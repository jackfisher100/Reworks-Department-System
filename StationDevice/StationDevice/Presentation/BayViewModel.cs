using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StationDevice.Presentation;

public partial class BayViewModel: MyObeservableObject
{
    private INavigator _navigator;

    [ObservableProperty]
    private string _title = "";

    int noOfBays = 0;

    [ObservableProperty]
    private List<string> _bays = [];

    [ObservableProperty]
    private string _selectedBay = "";

    [ObservableProperty]
    private string _name = "Null";

    [ObservableProperty]
    private string _message = "";

    private User details;
    //private static System.Timers.Timer _timer;
    private bool internetWorking;

    public BayViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        details = new User();
        _navigator = navigator;

        //_timer = new System.Timers.Timer(5000);
        //_timer.Elapsed += (sender, e) => NoInternetProtocol();
        //_timer.AutoReset = true;
        //_timer.Stop();

        Title = "Scanning Page";

        try
        {
            int noOfBays = httpClient.GetFromJsonAsync<int>("/settings/getsettings?Key=NoOfBays").Result;

            foreach (int i in Enumerable.Range(1, noOfBays))
            {
                Bays.Add(i.ToString());
            }
            internetWorking = true;
        }
        catch
        {
            Message = "Error with internet connection, please fix and restart application";
            //_timer.Start();
            internetWorking = false;
        }

        

    }

    //public void NoInternetProtocol()
    //{
    //    try
    //    {
    //        int noOfBays = httpClient.GetFromJsonAsync<int>("/settings/getsettings?Key=NoOfBays").Result;

    //        foreach (int i in Enumerable.Range(1, noOfBays))
    //        {
    //            Bays.Add(i.ToString());
    //        }
    //        clearMessage();
    //        _timer.Stop();
    //    }
    //    catch
    //    {
    //    }
    //}

    [RelayCommand]
    public void ContinuePressed()
    {
        if (!internetWorking) return;

        Console.WriteLine($"BayPage: Bay #{SelectedBay} chosen");
        if (SelectedBay != "")
        {
            details.BayNo = int.Parse(SelectedBay);
            _navigator.NavigateViewAsync<MainPage>(this, data: details);
        }
        else
        {
            Message = "Please select a bay number";
            _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => clearMessage());
        }

    }

    private void clearMessage()
    {
        Message = "";
    }   
}

