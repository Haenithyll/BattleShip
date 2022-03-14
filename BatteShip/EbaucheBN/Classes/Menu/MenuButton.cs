using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define a template for the buttons displayed on the menu at the very beginning of the game.

    #region MenuButtonClassDefinition
    internal class MenuButton
    {
        #region Variables
        Button myButton = new Button(); //Button to interact with
        #endregion
        #region Constructor
        public MenuButton(Grid MenuUI, string Content, RoutedEventHandler Event, int GridIndex)
        {
            myButton.Width = MenuUI.Width;                                      //Sets the button's width which depends on the Grid sent into the constructor.
            myButton.Height = MenuUI.Height / 2.5;                              //Sets the button's height which depends on the Grid sent into the constructor.
            myButton.Background = new SolidColorBrush(GameDesign.DefaultColor); //Sets the button background color depending on the color presets in GameDesign.
            myButton.Foreground = new SolidColorBrush(Colors.Black);            //Sets the font color to black to contrast with the button's color.
            myButton.FontSize = 32;                                             //Sets the fontsize to match the button's proportions.
            myButton.FontWeight = FontWeights.Bold;                             //Sets the text to be bold 

            myButton.Content = Content;                                         //Sets the button's text to the input sent into constructor.
            myButton.Click += Event;                                            //Subscribes the Click event to the Event sent into the constructor.

            MenuUI.Children.Add(myButton);                                      //Adds the button as a new child to the Grid sent into the constructor.
            MenuUI.RowDefinitions.Add(new RowDefinition());                     //Adds a new row to the Grid sent into the constructor.
            Grid.SetRow(myButton, GridIndex);                                   //Sets the button row position according to the index sent into the constructor.
        }
        #endregion
    }
    #endregion
}