using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    public class Coordinate
    {
        public Button Button = new Button();
        public TextBlock TextBlock = new TextBlock();

        public Coordinate(string coordinate, Grid UI)
        {
            TextBlock.Text = coordinate;
            TextBlock.Foreground = new SolidColorBrush(Colors.White);
            TextBlock.FontSize = 20;

            Button.Width = UI.Width / (GameDesign.MaxSize + 3);
            Button.Height = UI.Height / (GameDesign.ShipCount + 2);

            Button.Content = TextBlock;
            Button.IsEnabled = false;
                            
        }
    }
}
