using System.Net.Http.Json;
using System.Text.Json;
using ManagerTablet.Models;
using Uno.Extensions.Navigation;
using Windows.System;

namespace ManagerTablet.Presentation;

public partial class MainViewModel : MyObeservableObject
{
    private INavigator _navigator;

    public string? Title { get; }

    [ObservableProperty]
    private string _userID = "";

    [ObservableProperty]
    private string _loginOutput = "";

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Login to Managers Tablet";
        Console.WriteLine(Title);


    }


    [RelayCommand]
    public async Task LoginPressed()
    {
        Console.Out.WriteLine("Pressed login: UserID = " + UserID);

        if (UserID.Length == 0)
        {
            return;
        }
        

        try
        {
            HttpResponseMessage resp = await httpClient.GetAsync("manager/login?managerID=" + UserID);
            string response = await resp.Content.ReadAsStringAsync();
            if (response.ToLower() == "\"login failed\"")
            {
                LoginOutput = "ID not recognised";
                _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => clearLoginOutput());
                UserID = "";
                return;
            }

            LoginDetails? user = await httpClient.GetFromJsonAsync<LoginDetails>("manager/login?managerID=" + UserID);

            if (user != null)
            {
                //await _navigator.NavigateViewAsync<ScanningPage>(this, data: new Entity(response));
                await _navigator.NavigateViewAsync<FunctionSelectionPage>(this, data: user);
            }

            else
            {
                LoginOutput = "ID not recognised";
                _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => clearLoginOutput());
            }
        }
        catch
        {

            LoginOutput = "Error with network";
            _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => clearLoginOutput());
        }
        UserID = "";

    }

    private void clearLoginOutput()
    {
        LoginOutput = "";
    }

    [RelayCommand]
    private void Pressed1() => UserID += "1";
    [RelayCommand]
    private void Pressed2() => UserID += "2";
    [RelayCommand]
    private void Pressed3() => UserID += "3";
    [RelayCommand]
    private void Pressed4() => UserID += "4";
    [RelayCommand]
    private void Pressed5() => UserID += "5";
    [RelayCommand]
    private void Pressed6() => UserID += "6";
    [RelayCommand]
    private void Pressed7() => UserID += "7";
    [RelayCommand]
    private void Pressed8() => UserID += "8";
    [RelayCommand]
    private void Pressed9() => UserID += "9";
    [RelayCommand]
    private void Pressed0() => UserID += "0";
    [RelayCommand]
    private void PressedX() => UserID = "";




}
