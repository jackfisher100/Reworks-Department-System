
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace SummaryScreen.Presentation;


public partial class MainViewModel : MyObeservableObject
{
    //private INavigator _navigator;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    public int _completedCars;


    public string? Title { get; }

    public SolidColorBrush defaultBackgroundColour = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 119, 119, 119));
    private int noOfBays;

    private List<BayClass> BayDetailed = new List<BayClass>();


    public MainViewModel()
    {

        Title = "Main";
        //noOfBays = int.Parse(httpClient.GetAsync("/settings?Key=NoOfBays").Result.Content.ReadAsStringAsync().Result);


    }

    [RelayCommand]
    public void ButtonPressed()
    {
        Console.WriteLine("Button Pressed");
        //_ = updateInfo();
    }

}
