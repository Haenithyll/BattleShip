using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define a start button template to allow the user to launch the game upon the ship's positioning completion.

    #region StartGameButtonClassDefinition
    public class StartGameButton
    {
        #region Variables
        public Button button = new Button();            //Button to interact with
        public TextBlock textBlock = new TextBlock();   //Textblock that will contain the "Play Game" keywords and will further be set as the button's content.
        #endregion
        #region Constructor
        public StartGameButton(Grid UI)
        {
            textBlock.Text = "PLAY GAME";                                       //Sets the text of the textblock to indicate the button's function to the user.
            textBlock.Foreground = new SolidColorBrush(Colors.Black);           //Sets the font color to black to contrast with the button's color.

            button.Width = UI.Width / (GameDesign.MaxSize + 3);                 //Sets the button's width which depends on the Grid sent into the constructor.
            button.Height = UI.Height / (GameDesign.ShipCount + 2);             //Sets the button's height which depends on the Grid sent into the constructor.
            button.Background = new SolidColorBrush(GameDesign.StartGameColor); //Sets the button background color depending on the color presets in GameDesign.
            button.Content = textBlock;                                         //Sets the button's content to be the textblock to display the text contained.
            button.IsEnabled = false;                                           //Disables the button by default to prevent the user from launching the game
                                                                                                                    //while the ship's selection is not complete.
            button.Click += OnClick;                                            //Subscribes the Click event to the Onclick action. 

            UI.RowDefinitions.Add(new RowDefinition());         //Adds a new row to the Grid sent into the constructor.
            UI.Children.Add(button);                            //Adds the button as a new child to the Grid sent into the constructor.
            Grid.SetRow(button, GameDesign.ShipCount);          //Sets the button row position to be at the very bottom. 
            Grid.SetColumn(button, GameDesign.MaxSize + 2);     //Sets the button column position to be on the right edge of the row.
        }
        #endregion
        #region Click Event
        private void OnClick(object sender, RoutedEventArgs e) //Function used to respond to user interaction with the StartGame button.
        {
            MainPage.Instance.StartGame(); 
        }
        #endregion
    }
    #endregion
}