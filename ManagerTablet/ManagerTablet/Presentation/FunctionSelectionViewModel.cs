using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.System;

namespace ManagerTablet.Presentation;
public partial class FunctionSelectionViewModel : MyObeservableObject
{

    private INavigator _navigator;

    private LoginDetails details;

    [ObservableProperty]
    private string _title = "";

    public FunctionSelectionViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        LoginDetails _details)
    {
        _navigator = navigator;
        details = _details;

        Title = details.NameOfUser;
    }



    [RelayCommand]
    public async Task ViewUsers()
    {
        Console.WriteLine("View Users");
        await _navigator.NavigateViewAsync<ViewUsersPage>(this, data: details);
    }

    [RelayCommand] 
    public async Task EnterPrediction()
    {
        Console.WriteLine("Enter Prediction");
        await _navigator.NavigateViewAsync<ScanningPage>(this, data: details);

    }

    [RelayCommand]
    public void LogoutPressed()
    {
        _navigator.NavigateBackAsync(this);
    }
}
