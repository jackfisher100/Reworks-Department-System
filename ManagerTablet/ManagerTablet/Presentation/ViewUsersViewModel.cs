using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ManagerTablet.Presentation;
public partial class ViewUsersViewModel : MyObeservableObject
{
    private INavigator _navigator;

    private LoginDetails details;

    [ObservableProperty]
    private List<User>? _allUsers = new List<User>();

    [ObservableProperty]
    private string _errorMessage;

    [ObservableProperty]
    private Visibility _errorVisibility = Visibility.Visible;


    [ObservableProperty]
    private string _title = "";


    public ViewUsersViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        LoginDetails _details)
    {
        _navigator = navigator;
        details = _details;

        Title = details.NameOfUser;

        updateUsers();
    }

    public async void updateUsers()
    {
        ErrorMessage = "Error with network";
        ErrorVisibility = Visibility.Visible;

        try
        {
            AllUsers = await httpClient.GetFromJsonAsync<List<User>>("manager/allUsers");
            ErrorMessage = "";
            ErrorVisibility = Visibility.Collapsed;
        }
        catch { }
    }


    [RelayCommand]
    public void LogoutPressed()
    {
        _navigator.NavigateViewAsync<MainPage>(this, data: details, qualifier: Qualifiers.ClearBackStack);
    }

    [RelayCommand]
    public void AddNewUser()
    {
        User newUser = new User();
        _navigator.NavigateViewAsync<EditUserPage>(this, data: newUser);
    }

    public void UserClicked(User userClicked)
    {
        Console.WriteLine(userClicked.NameOfUser);
        _navigator.NavigateViewAsync<EditUserPage>(this, data: userClicked);
    }

}
