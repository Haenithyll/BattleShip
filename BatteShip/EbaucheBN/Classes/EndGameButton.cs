using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    internal class EndGameButton
    {
        Button button = new Button();
        TextBlock textBlock = new TextBlock();

        public EndGameButton(Grid UI, int RowIndex, string Text, bool Enabled)
        {
            button.Background = new SolidColorBrush(GameDesign.DefaultColor);
            button.Width = 2 * UI.Width / 3;
            button.Height = UI.Height / 6;
            button.IsEnabled = Enabled;

            textBlock.Text = Text;
            textBlock.FontSize = 28;
            textBlock.Foreground = new SolidColorBrush(Enabled ? Colors.Black : Colors.White);

            button.Content = textBlock;
            button.HorizontalAlignment = HorizontalAlignment.Center;

            UI.Children.Add(button);
            Grid.SetRow(button, RowIndex);

            if (Enabled)
                button.Click += OnClick;
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
