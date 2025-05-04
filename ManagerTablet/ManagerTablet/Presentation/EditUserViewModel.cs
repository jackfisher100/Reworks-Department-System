using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ManagerTablet.Presentation;

public partial class EditUserViewModel : MyObeservableObject
{

    private INavigator _navigator;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private User _currentUser;

    [ObservableProperty]
    private string _errorText;

    [ObservableProperty]
    private string _submitText;

    public EditUserViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        User _user)
    {
        _navigator = navigator;

        if (_user.BentleyID == null)
        {
            CurrentUser = new User();
            Title = "Making New User";
            SubmitText = "Create new user";
        }
        else
        {
            CurrentUser = _user;
            Title = "Editing " + _user.NameOfUser;
            SubmitText = "Update";
        }

    }

    [RelayCommand]
    public void BackPressed()
    {
        //Submit info

        _navigator.NavigateBackAsync(this);
    }

    [RelayCommand]
    public async Task Update()
    {
        if (CurrentUser.BentleyID == null || CurrentUser.BentleyID == "") ErrorText = "Please enter a Bentley ID";
        else if (CurrentUser.NameOfUser == null || CurrentUser.NameOfUser == "") ErrorText = "Plase enter a name for the user";
        else if (CurrentUser.Manager == null) ErrorText = "Please choose the users manager level";
        else
        {
            try
            {
                HttpResponseMessage resp = await httpClient.PostAsJsonAsync("manager/editUser", CurrentUser);

                if (resp.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ErrorText = "Bentley ID is already assigned to someone else";

                }
                else
                {
                    await _navigator.NavigateBackAsync(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorText = "Error with network";
            }
        }



        _ = Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o => ClearErrorMessage());
    }

    private void ClearErrorMessage()
    {
        ErrorText = "";
    }
}
