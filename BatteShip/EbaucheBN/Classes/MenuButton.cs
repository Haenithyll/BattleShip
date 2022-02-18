using EbaucheBN.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EbaucheBN.Classes
{
    internal class MenuButton
    {
        Button myButton = new Button();

        public MenuButton(Grid MenuUI, string Content, RoutedEventHandler Event, int GridIndex)
        {
            MenuUI.RowDefinitions.Add(new RowDefinition());
            myButton.Content = Content;
            MenuUI.Children.Add(myButton);
            Grid.SetRow(myButton, GridIndex);
            myButton.Width = MenuUI.Width;
            myButton.Height = MenuUI.Height / 2.5;
            myButton.Background = new SolidColorBrush(Colors.LightGray);
            myButton.Foreground = new SolidColorBrush(Colors.Black);
            myButton.FontSize = 32;
            myButton.FontWeight = FontWeights.Bold;
            myButton.Click += Event;
        }
    }
}
