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
    public class StartGameButton
    {
        public Button button = new Button();
        public TextBlock textBlock = new TextBlock();

        public StartGameButton(Grid UI)
        {
            textBlock.Text = "PLAY GAME";
            textBlock.Foreground = new SolidColorBrush(Colors.Black);

            button.Width = UI.Width / (GameDesign.MaxSize + 3);
            button.Height = UI.Height / (GameDesign.ShipCount + 2);
            button.Background = new SolidColorBrush(GameDesign.StartGameColor);
            button.Content = textBlock;
            button.IsEnabled = false;
            button.Click += OnClick;

            UI.RowDefinitions.Add(new RowDefinition());
            UI.Children.Add(button);
            Grid.SetRow(button, GameDesign.ShipCount);
            Grid.SetColumn(button, GameDesign.MaxSize + 2);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.StartGame();
        }
    }
}
