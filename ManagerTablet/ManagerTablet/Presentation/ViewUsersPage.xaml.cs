using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ManagerTablet.Presentation;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ViewUsersPage : Page
{
    public ViewUsersPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        try
        {
            ViewUsersViewModel vm = (ViewUsersViewModel)this.DataContext;
            Console.WriteLine("Success");
            vm.updateUsers();
        }
        catch { }
    }

    private void UserItemClicked(object sender, ItemClickEventArgs e)
    {
        var vm = (ViewUsersViewModel)this.DataContext;
        var clickedUser = (User)e.ClickedItem;

        vm.UserClicked(clickedUser);
        
    }


}
