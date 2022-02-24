using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    public class ResetButton
    {
        public Button button = new Button();
        public TextBlock textBlock = new TextBlock();

        private Action Reset;

        public ResetButton(int ShipCount, Grid UI, Action ResetCoordinates)
        {
            Reset = ResetCoordinates;

            textBlock.Text = "RESET";
            textBlock.Foreground = new SolidColorBrush(Colors.Black);

            button.Width = UI.Width / (GameDesign.MaxSize + 3);
            button.Height = UI.Height / (GameDesign.ShipCount + 2);
            button.Background = new SolidColorBrush(GameDesign.DefaultColor);
            button.Content = textBlock;
            button.IsEnabled = false;
            button.Click += OnClick;

            UI.Children.Add(button);
            Grid.SetRow(button, ShipCount);
            Grid.SetColumn(button, GameDesign.MaxSize + 1);
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

    }
}
