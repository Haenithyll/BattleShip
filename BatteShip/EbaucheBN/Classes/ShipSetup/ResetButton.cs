using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define a reset button to allow the user to change a ship's coordinates after they've been set.

    #region ResetButtonClassDefinition
    public class ResetButton
    {
        #region Variables
        public Button button = new Button();            //Button to interact with
        public TextBlock textBlock = new TextBlock();   //Textblock that will contain the "reset" keyword and will further be set as the button's content.

        private Action Reset;                           //Action that will allow the player's interaction to call the function sent as an input in the constructor.
        #endregion
        #region Constructor
        public ResetButton(int ShipCount, Grid UI, Action ResetCoordinates)
        {
            Reset = ResetCoordinates;                                           //Sets the action to the one sent as an input into constructor.

            textBlock.Text = "RESET";                                           //Sets the text of the textblock to indicate the button's function to the user.
            textBlock.Foreground = new SolidColorBrush(Colors.Black);           //Sets the font color to black to contrast with the button's color.

            button.Width = UI.Width / (GameDesign.MaxSize + 3);                 //Sets the button's width which depends on the Grid sent into the constructor.
            button.Height = UI.Height / (GameDesign.ShipCount + 2);             //Sets the button's height which depends on the Grid sent into the constructor.
            button.Background = new SolidColorBrush(GameDesign.DefaultColor);   //Sets the button background color depending on the color presets in GameDesign.
            button.Content = textBlock;                                         //Sets the button's content to be the textblock to display the text contained.
            button.IsEnabled = false;                                           //Disables the button to prevent the user from reseting
                                                                                                                           //a ship's positioning that's not been set yet.
            button.Click += OnClick;                                            //Subscribes the Click event to the Onclick action. 

            UI.Children.Add(button);                            //Adds the button as a new child to the Grid sent as an input into the constructor.
            Grid.SetRow(button, ShipCount);                     //Sets the button row position to match the ship whose coordinates are reset upon this button being pressed.
            Grid.SetColumn(button, GameDesign.MaxSize + 1);     //Sets the button column position to be on the right edge of the ship selection row.
        }
        #endregion
        #region Click Event
        void OnClick(object sender, RoutedEventArgs e) //Function used to respond to user interaction with the reset button.
        {
            Reset(); //Calls the function that was sent as an action as an input into the constructor.
        }
        #endregion
    }
    #endregion
}