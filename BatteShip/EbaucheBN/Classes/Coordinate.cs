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
    //Coordinate Objects are affiliated to one of the player ship and serve the purpose of displaying a cell coordinate on which the ship is placed.
    //Therefore, a ship will have as many 'Coordinates' as its size. 

    #region CoordinateClassDefinition
    public class Coordinate
    {
        #region Variables
        public Button Button = new Button();                //Button here used as a container for the TextBlock.
        public TextBlock TextBlock = new TextBlock();       //Textblock that will contain the Cell coordinate and will further be set as the button's content.
        #endregion
        #region Constructor
        public Coordinate(string coordinate, Grid UI)
        {
            TextBlock.Text = coordinate;                                //Sets the text of the textblock to be the Cell coordinate string sent in the constructor.
            TextBlock.Foreground = new SolidColorBrush(Colors.White);   //Sets the font color to white to contrast with the button's color.
            TextBlock.FontSize = 20;                                    //Sets the fontsize to match the button's proportions.

            Button.Width = UI.Width / (GameDesign.MaxSize + 3);         //Sets the button's width which depends on the Grid sent into the constructor.
            Button.Height = UI.Height / (GameDesign.ShipCount + 2);     //Sets the button's height which depends on the Grid sent into the constructor.

            Button.Content = TextBlock;     //Sets the button's content to be the textblock to display the text contained.
            Button.IsEnabled = false;       //Sets the button to be disabled since there is no purpose in interacting with the button.
        }
        #endregion
    }
    #endregion
}