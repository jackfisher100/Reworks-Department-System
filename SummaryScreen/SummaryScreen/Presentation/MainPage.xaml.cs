using System.Net.Http.Json;
using System.Text.Json;

namespace SummaryScreen.Presentation;

public sealed partial class MainPage : Page
{
    private static System.Timers.Timer _timer;

    public int CompletedCars { get; set; }
    private List<BayClass> BayDetailed = new List<BayClass>();

    MyObeservableObject THEWHOLEENTIREINTERNET = new MyObeservableObject();
    public int? noOfBays { get; set; }

    private bool currentlyRed = false;

    public MainPage()
    {
        this.InitializeComponent();

        getNoOfBays();

        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed +=  (sender, e) =>  getBayInfo();
        _timer.Start();

    }

    private void getNoOfBays()
    {
        try
        {
            HttpResponseMessage resp = THEWHOLEENTIREINTERNET.httpClient.GetAsync("/settings/getSettings?Key=NoOfBays").Result;

            string noOfBaysJson = resp.Content.ReadAsStringAsync().Result;
            noOfBaysJson = noOfBaysJson.Replace("\"", "");

            noOfBays = JsonSerializer.Deserialize<int>(noOfBaysJson);

            MessageTextBlock.Text = "";
        }
        catch
        {
            MessageTextBlock.Text = "Error with network";
            noOfBays = null;

        }
    }


    private void getBayInfo()
    {

        _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
        {
            await updateInfo();

        });

    }

    public async Task updateInfo()
    {
        try
        {
            List<BayClass>? bayInfo = await THEWHOLEENTIREINTERNET.httpClient.GetFromJsonAsync<List<BayClass>?>("Bays/allBays");

            CompletedCars = await THEWHOLEENTIREINTERNET.httpClient.GetFromJsonAsync<int>("Bays/todaysStats");

            MessageTextBlock.Text = "";

            TextBlock completedCarsText = (TextBlock)this.FindName("CompletedCarsText");
            completedCarsText.Text = CompletedCars.ToString();

            resetBayList();
            if (noOfBays == null) return;


            if (bayInfo != null && bayInfo.Count > 0)
            {
                foreach (BayClass c in bayInfo)
                {
                    if (!c.andonOn) c.Background = BayClass.BackgroundAndonOff;
                    else
                    {
                        if (currentlyRed) c.Background = BayClass.Yellow;
                        else c.Background = BayClass.Red;
                    }

                    try
                    {
                        BayDetailed[c.bayNo - 1] = c;
                    }
                    catch
                    {
                        // Can happen if it has been set up for a large number of bays, a car is left checked in before switching back down to a smaller amount of bays
                        // This will then try and add the car to a position in BayDetailed that doesn't exist
                    }
                }
            }

            currentlyRed = !currentlyRed;

            MainViewModel vm = (MainViewModel)this.DataContext;
            GridView gv = (GridView)this.FindName("BayGridView");


            gv.Items.Clear();

            foreach (BayClass bay in BayDetailed)
            {
                gv.Items.Add(bay);
            }

        }
        catch
        {
            MessageTextBlock.Text = "Error with network";
        }

    }


    private void resetBayList()
    {
        getNoOfBays();
        if (noOfBays == null) return;

        BayDetailed.Clear();
        for (int i = 0; i < noOfBays; i++)
        {
            BayClass tempBay = new BayClass();
            tempBay.bayNo = i + 1;
            tempBay.Background = BayClass.BackgroundAndonOff;

            BayDetailed.Add(tempBay);
        }
    }
}
