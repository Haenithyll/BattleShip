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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EbaucheBN.Classes
{
    public class ShipSelectionButton
    {
        public Button Button = new Button();
        public TextBlock Coordinates = new TextBlock();
        private TextBlock ShipSelectionText = new TextBlock();

        public ShipSelectionButton(int ShipCount, Grid UI, Grid SelectionUI)
        {
            ShipSelectionText.Text = GameDesign.ShipArray[ShipCount].Name;
            ShipSelectionText.FontSize = 20;
            ShipSelectionText.Foreground = new SolidColorBrush(Colors.Black);

            Button.Width = UI.Width / (GameDesign.ShipCount + 1);
            Button.Height = 50;
            Button.Background = new SolidColorBrush(GameDesign.DefaultColor);
            Button.Content = ShipSelectionText;
            Button.Click += OnClick;

            SelectionUI.Children.Add(Button);
            Grid.SetRow(Button, 0);
            Grid.SetColumn(Button, ShipCount);

            Coordinates.HorizontalAlignment = HorizontalAlignment.Center;
            Coordinates.Foreground = new SolidColorBrush(Colors.White);
            Coordinates.FontSize = 20;

            SelectionUI.Children.Add(Coordinates);
            Grid.SetRow(Coordinates, 1);
            Grid.SetColumn(Coordinates, ShipCount);
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.shipSetupManager.Click(this);
        }
    }
}