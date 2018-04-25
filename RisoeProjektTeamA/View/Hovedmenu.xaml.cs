using RisoeProjektTeamA.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RisoeProjektTeamA.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Hovedmenu : Page
    {
        public Hovedmenu()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Hovedmenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Hovedmenu));
        }

        private void Button_Click_Oversigt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Oversigt));
        }

        private void Button_Click_Opgaver(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Opgaver));
        }

        private void Button_Click_Udstyr(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
