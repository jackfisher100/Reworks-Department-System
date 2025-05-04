using System.Text;
using Microsoft.UI;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.UI.Xaml.Controls;

namespace StationDevice.Presentation;

public partial class ScanningViewModel : MyObeservableObject
{
    private INavigator _navigator;

    private User details;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _breakButtonText = "Start Break";

    [ObservableProperty]
    private string _title = "";

    [ObservableProperty]
    private Visibility _buttonsVisible = Visibility.Collapsed;

    [ObservableProperty]
    private Visibility _inputBoxVisible = Visibility.Visible;

    [ObservableProperty]
    private Visibility _endBreakButtonVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private Visibility _finishButtonVisible = Visibility.Collapsed;

    [ObservableProperty]
    private string _scannerInput;

    [ObservableProperty]
    private SolidColorBrush _managerColour = new SolidColorBrush(Colors.DarkGreen);

    [ObservableProperty]
    private Visibility _logoutVisible = Visibility.Visible;

    [ObservableProperty]
    private string _message;

    [ObservableProperty]
    private Visibility _messageVisibility = Visibility.Visible;

    [ObservableProperty]
    private Visibility _progressVisible = Visibility.Collapsed;

    [ObservableProperty]
    private int? _predicted;

    [ObservableProperty]
    private int _currentMins;

    private static System.Timers.Timer managerButtonTimer;
    public SolidColorBrush defaultButtonColour;

    private bool onBreak = false;

    Car currentCar;
    DateTime breakStarted;

    public ScanningViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        User _details)
    {
        _navigator = navigator;
        details = _details;
        Name = details.NameOfUser;
        
        Title = details.NameOfUser + ": Bay #" + details.BayNo.ToString();

        managerButtonTimer = new System.Timers.Timer(1000);
        managerButtonTimer.Elapsed += (sender, e) => managerButtonChangeColour();
        managerButtonTimer.AutoReset = true;
        managerButtonTimer.Stop();


    }


    public async void BarcodeEntered(string barcode)
    {
        currentCar = new Car();
        currentCar.Barcode = barcode;
        currentCar.BayNo = details.BayNo;
        currentCar.WorkerID = details.BentleyID.ToString();
        currentCar.Dwell = 0;

        try
        {
            HttpResponseMessage resp =  await httpClient.PostAsJsonAsync("worker/checkinCar", currentCar);
            string? response = await resp.Content.ReadAsStringAsync();
            Console.WriteLine(response);

            ButtonsVisible = Visibility.Visible;
            InputBoxVisible = Visibility.Collapsed;
            LogoutVisible = Visibility.Collapsed;
            FinishButtonVisible = Visibility.Visible;
            Message = "";

        }
        catch
        {
            Message = "Error with network";            
        }

        try
        {
            int predictedTime = await httpClient.GetFromJsonAsync<int>("worker/getPredicted?Barcode=" + barcode);
            Predicted = predictedTime*60; // Predicted is in seconds
            ProgressVisible = Visibility.Visible;

            if (predictedTime < 60) Message = "Predicted Time: " + predictedTime + " mins";
            else
            {
                int hours = predictedTime / 60;
                int mins = predictedTime % 60;
                Message = "Predicted Time: " + hours + "h " + mins + "mins";
            }

            Progress<int> progress = new Progress<int>(value => CurrentMins = value);
            _ = Task.Run(() =>
            {
                for (int i = 0; i <= Predicted; i++)
                {
                    if (onBreak) { i--; }
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(1000);
                }
            });

        }
        catch { }


    }

    private void showPredictedTime()
    {
        // Predicted is in seconds not minutes
        if (Predicted != null)
        {
            if (Predicted < 3600) Message = "Predicted Time: " + Predicted/60 + " mins";
            else
            {
                int hours = Predicted.Value / 3600;
                int mins = Predicted.Value % 3600;
                Message = "Predicted Time: " + hours + "h " + mins + "mins";
            }
        }
        else
        {
            Message = "";
        }
    }

    [RelayCommand]
    public async Task FinishCarPressed()
    {
        try
        {
            HttpResponseMessage resp = await httpClient.PostAsJsonAsync("worker/checkoutCar", currentCar);

            string? response = await resp.Content.ReadAsStringAsync();

            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ButtonsVisible = Visibility.Collapsed;
                InputBoxVisible = Visibility.Visible;
                LogoutVisible = Visibility.Visible;
                FinishButtonVisible = Visibility.Collapsed;
                ProgressVisible = Visibility.Collapsed;
                Message = "";
            }
            else
            {
                Console.WriteLine(response);
                Message = "Error with network";
            }
        }
        catch
        {
            Message = "Error with network";
        }
    }


    [RelayCommand]
    public void LogoutPressed()
    {
        Console.WriteLine("Pressed Logout");
        _navigator.NavigateBackAsync(this);
    }

    [RelayCommand]
    public async Task BreakPressed()
    {
        try
        {
            HttpResponseMessage resp = await httpClient.PostAsJsonAsync("Bays/breakOn", currentCar);
            breakStarted = DateTime.Now;
            onBreak = !onBreak;
            EndBreakButtonVisibility = Visibility.Visible;
            showPredictedTime();

        }
        catch
        {
            Message = "Error with network";
        }


    }

    [RelayCommand]
    public async Task EndBreak()
    {
        try { 
            HttpResponseMessage resp = await httpClient.PostAsJsonAsync("Bays/breakOff", currentCar);
            EndBreakButtonVisibility = Visibility.Collapsed;
            currentCar.Dwell += (DateTime.Now - breakStarted).Minutes;
            onBreak = !onBreak;
            showPredictedTime();
        }
        catch
        {
            Message = "Error with network";
        }
    }


    private bool isRed = true;
    private void managerButtonChangeColour()
    {
        if (isRed)
        {
            ManagerColour = new SolidColorBrush(Colors.Yellow);
        }
        else
        {
            ManagerColour = new SolidColorBrush(Colors.Red);
        }
        isRed = !isRed;
    }

    private bool managerPressed = false;
    [RelayCommand]
    public async Task CallManagerPressed()
    {
        try
        {
            Console.WriteLine("Andon Pressed");
            managerPressed = !managerPressed;
            if (managerPressed)
            {
                managerButtonChangeColour();
                managerButtonTimer.Start();
                FinishButtonVisible = Visibility.Collapsed;

                Car tempCar = new Car { WorkerID = details.BentleyID, BayNo = details.BayNo };
                HttpResponseMessage resp = await httpClient.PostAsJsonAsync("Bays/andonon", tempCar);


            }
            else
            {
                managerButtonTimer.Stop();
                ManagerColour = defaultButtonColour;

                Car tempCar = new Car { WorkerID = details.BentleyID, BayNo = details.BayNo };
                HttpResponseMessage resp = await httpClient.PostAsJsonAsync("Bays/andonoff", tempCar);

                FinishButtonVisible = Visibility.Visible;
            }
            Message = "";
        }
        catch
        {
            managerButtonTimer.Stop();
            ManagerColour = defaultButtonColour;
            FinishButtonVisible = Visibility.Visible;
            managerPressed = !managerPressed;
            Message = "Error with network";
        }
    }

    

}

