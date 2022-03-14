using Windows.UI.Xaml.Controls;


namespace EbaucheBN.Classes
{
    //This class' purpose is to define a button template that is used for the battleship grid to display either the column letter or the row index.

    #region IndexClassDefinition
    internal class Index
    {
        #region Variables
        public Button button = new Button(); //Button used as a container for the letter/index displayed.
        #endregion
        #region Constructor
        public Index(string name, Grid UI)
        {
            button.Width = UI.Width * 0.85 / (GameDesign.GridSizeX + 1);    //Sets the button's width which depends on the Grid sent into the constructor.
            button.Height = UI.Height * 0.85 / (GameDesign.GridSizeY + 1);  //Sets the button's height which depends on the Grid sent into the constructor.
            button.IsEnabled = false;                                       //Disables the button since its purpose isnt to allow user interaction.
            button.Content = name;                                          //Sets the button content to the string sent into the constructor (letter/index)
        }
        #endregion
    }
    #endregion
}