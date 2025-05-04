using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Windows.Media.Protection.PlayReady;

namespace StationDevice.Presentation;

public partial class MainViewModel : MyObeservableObject
{
	private INavigator _navigator;


    public string? Title { get; }

    private User CurrentUser;

    [ObservableProperty]
    private string _userID = "";

    [ObservableProperty]
    private string _loginOutput = "";

    public MainViewModel(
		IStringLocalizer localizer,
		IOptions<AppConfig> appInfo,
		INavigator navigator,
        User _user)
	{
		_navigator = navigator;
		Title = "Login for Bay "+_user.BayNo;
        Console.WriteLine(Title);

        CurrentUser = _user;
    }


    [RelayCommand]
    public async Task LoginPressed()
    {
        Console.Out.WriteLine("Pressed login: UserID = "+ UserID);

        if (UserID.Length == 0)
        {
            return;
        }
        try
        {
            HttpResponseMessage resp = await httpClient.GetAsync("worker/login?WorkerID=" + UserID);
            string response = await resp.Content.ReadAsStringAsync();
            if (response.ToLower() == "\"login failed\"")
            {
                LoginOutput = "ID not recognised";
                _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => clearLoginOutput());
                UserID = "";
                return;
            }

            User? user = await httpClient.GetFromJsonAsync<User>("worker/login?WorkerID=" + UserID);

            if (user != null)
            {
                user.BayNo = CurrentUser.BayNo;
                await _navigator.NavigateViewAsync<ScanningPage>(this, data: user);
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
