using System.Text;
using Microsoft.UI;
using System.Text.Json;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace ManagerTablet.Presentation;

public partial class ScanningViewModel : MyObeservableObject
{
    private INavigator _navigator;

    private LoginDetails details;

    [ObservableProperty]
    private string _title = "";

    [ObservableProperty]
    private Visibility _inputBoxVisible = Visibility.Visible;

    [ObservableProperty]
    private Visibility _updatePredecitedVisible = Visibility.Collapsed;

    [ObservableProperty]
    private string _scannerInput;

    [ObservableProperty]
    private List<int> _possibleHours = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    [ObservableProperty]
    private List<string> _possibleMins = new List<string>();

    [ObservableProperty]
    private int _selectedHour;

    [ObservableProperty]
    private string _selectedMin;

    [ObservableProperty]
    private string _errorMessage = "";

    Car currentCar;
    DateTime breakStarted;

    public ScanningViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        LoginDetails _details)
    {
        _navigator = navigator;
        details = _details;

        Title = details.NameOfUser;


        for (int i = 0; i < 60; i++)
        {
            if (i < 10) PossibleMins.Add("0" + i);
            else PossibleMins.Add(i.ToString());
        }

        SelectedHour = PossibleHours[0];
        SelectedMin = PossibleMins[0];
    }


    public void BarcodeEntered(string barcode)
    {
        currentCar = new Car();
        currentCar.Barcode = barcode;
        InputBoxVisible = Visibility.Collapsed;
        UpdatePredecitedVisible = Visibility.Visible;

    }

    [RelayCommand]
    public async Task FinishCarPressed()
    {
        currentCar.Prediction = (SelectedHour * 60) + int.Parse(SelectedMin);
        currentCar.ManagerID = details.BentleyID;

        try
        {
            HttpResponseMessage resp = await httpClient.PostAsJsonAsync("manager/updatePredicted", currentCar);

            string? response = await resp.Content.ReadAsStringAsync();

            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UpdatePredecitedVisible = Visibility.Collapsed;
                InputBoxVisible = Visibility.Visible;
            }
            else
            {
                Console.WriteLine(response);
            }
            ErrorMessage = "";
        }
        catch
        {
            ErrorMessage = "Error with network";
        }
    }


    [RelayCommand]
    public void LogoutPressed()
    {
        Console.WriteLine("Pressed Logout");
        _navigator.NavigateViewAsync<MainPage>(this, data: details, qualifier: Qualifiers.ClearBackStack);
    }


}

