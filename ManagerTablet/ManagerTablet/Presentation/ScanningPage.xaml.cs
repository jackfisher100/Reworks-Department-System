using Microsoft.UI.Xaml.Input;

namespace ManagerTablet.Presentation;

public sealed partial class ScanningPage : Page
{
    private static TextBox inputBox;
    private static System.Timers.Timer _lostFocusTimer;
    private bool isVisible = true;

    private static System.Timers.Timer _timer;
    private static object _lock = new object();

    


    public ScanningPage()
    {
        this.InitializeComponent();
        inputBox = (TextBox)this.FindName("InputBox");
        _ = inputBox.Focus(FocusState.Programmatic);


        _timer = new System.Timers.Timer(750); // 2 seconds
        _timer.Elapsed += (sender, e) => BarcodeComplete();
        _timer.AutoReset = false;

        // This timer is because the input box loses focus when checking in a car and not making it visible
        _lostFocusTimer = new System.Timers.Timer(1000);
        _lostFocusTimer.Elapsed += (sender, e) => EnableInputFocus();
        _lostFocusTimer.AutoReset = true;
        _lostFocusTimer.Start();

    }

    private async void EnableInputFocus()
    {
        if (isVisible)
        {
            await Task.Delay(200);
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                _ = await FocusManager.TryFocusAsync(inputBox, FocusState.Programmatic);
            });
        }
        
    }


    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        //Console.WriteLine("Is not visible");
        isVisible = false;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        isVisible = true;
    }

    ~ScanningPage()
    {
        Console.WriteLine("Killing page");
    }

    private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        lock (_lock)
        {
            _timer.Stop();
            _timer.Start();
        }

    }



    private void BarcodeComplete()
    {
        ScanningViewModel vm = (ScanningViewModel)DataContext;
        if (inputBox.Text == "") return; // Happens when the text box is cleared at the end of the function

        // Runs this on the UI thread so that it has access to the inputBox
        _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        {
            try { vm.BarcodeEntered(inputBox.Text); } catch { }
            inputBox.Text = string.Empty;
        });



    }

}


