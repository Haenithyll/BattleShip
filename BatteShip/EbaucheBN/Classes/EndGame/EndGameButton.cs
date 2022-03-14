using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define a button template used to display messages and enable to player to quit the game upon game over.

    #region EndGameButtonClassDefinition
    internal class EndGameButton
    {
        #region Variables
        private Button button = new Button();               //Button to interact with 
        private TextBlock textBlock = new TextBlock();      //Textblock that will contain either messages or instruction and will later be set as the button's content.
        #endregion
        #region Constructor
        public EndGameButton(Grid UI, int RowIndex, string Text, bool Enabled)
        {
            button.Background = new SolidColorBrush(GameDesign.DefaultColor);   //Sets the button background color depending on the color presets in GameDesign.
            button.Width = 2 * UI.Width / 3;                                    //Sets the button's width which depends on the Grid sent into the constructor.
            button.Height = UI.Height / 6;                                      //Sets the button's height which depends on the Grid sent into the constructor.
            button.IsEnabled = Enabled;                                         //Sets the button status according to the bool sent as an input into the constructor.

            textBlock.Text = Text;                                              //Sets the Textblock text to the string sent as an input into the constructor.
            textBlock.FontSize = 28;                                            //Sets the fontsize to match the button's proportions.
            textBlock.Foreground = new SolidColorBrush(Enabled ? Colors.Black : Colors.White); //Sets the button's color depending on its status due to the contrast.

            button.Content = textBlock;                                         //Sets the button's content to be the textblock to display the text contained.
            button.HorizontalAlignment = HorizontalAlignment.Center;            //Sets the button's alignment to be at the center of its Grid position.  

            UI.Children.Add(button);                    //Adds the button as a new child to the Grid sent as an input into the constructor.
            Grid.SetRow(button, RowIndex);              //Sets to button Row position according to the index sent as an input into the constructor.

            if (Enabled) //If the button is set to be clickable, subscribes the Click event to the OnClick action.
                button.Click += OnClick;
        }
        #endregion
        #region Click Event
        void OnClick(object sender, RoutedEventArgs e) //Upon being pressed, the button will close the application.
        {
            CoreApplication.Exit();
        }
        #endregion
    }
    #endregion
}