using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleControlsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static int count=0;
       
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (count%2==0)
            {
                VisualStateManager.GoToState(this, "UnPressed", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Pressed", false);
            }
            count++;
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
           Btn.IsEnabled = true;
           Ring1.IsActive = false;
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            SampleButton.IsLoading = false;
        }

        private void SampleButton_Toggled(object sender, RoutedEventArgs e)
        {
            if (count % 2 == 0)
            {
                VisualStateManager.GoToState(this, "UnPressed", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Pressed", false);
            }
            count++;
        }

        private void Status_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Status.Background = new SolidColorBrush(Colors.Red);
        }

        private void Status_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Status.Background = new SolidColorBrush(Colors.Green);
        }

        private void Status_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }
    }
}
