using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StationDevice.Presentation
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ScanningPage : Page
	{
		private static TextBox inputBox;
		private static Grid buttonsGrid;
        private static System.Timers.Timer _lostFocusTimer;
		private bool isVisible = true;

		private static System.Timers.Timer _timer;
		private static object _lock = new object();


        public ScanningPage()
		{
			this.InitializeComponent();
			inputBox = (TextBox)this.FindName("InputBox");
			buttonsGrid = (Grid)this.FindName("ButtonsGrid");
            _ = inputBox.Focus(FocusState.Programmatic);



            _timer = new System.Timers.Timer(750); // 2 seconds
			_timer.Elapsed +=(sender, e) => BarcodeComplete();
			_timer.AutoReset = false;
            
            // This timer is because the input box loses focus when checking in a car and not making it visible
            _lostFocusTimer = new System.Timers.Timer(1000);
            _lostFocusTimer.Elapsed += (sender, e) => InputBox_LostFocus(null, null);
            _lostFocusTimer.AutoReset = true;
            _lostFocusTimer.Start();

		}

        private async void InputBox_LostFocus(object? sender, RoutedEventArgs? e)
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
			Console.WriteLine("Is not visible");
			isVisible = false;
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



        bool firstRun = false;
        private void BarcodeComplete()
		{
			ScanningViewModel vm = (ScanningViewModel)DataContext;
			if (inputBox.Text == "") return; // Happens when the text box is cleared at the end of the function

            if (!firstRun)
            {
                vm.defaultButtonColour = (SolidColorBrush)((Button)this.FindName("BreakButton")).Background;
                ((Button)this.FindName("ManagerButton")).Background = vm.defaultButtonColour;
                firstRun = true;
            }

            // Runs this on the UI thread so that it has access to the inputBox
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
			{
				vm.BarcodeEntered(inputBox.Text); 
				inputBox.Text = string.Empty;
				//buttonsGrid.Visibility = Visibility.Visible;
			});



		}


        private void ManagerButton_GotFocus(object sender, RoutedEventArgs e)
        {
            ScanningViewModel vm = (ScanningViewModel)this.DataContext;
            
            this.Focus(FocusState.Programmatic);
            vm.CallManagerPressed();
        }
    }
}
