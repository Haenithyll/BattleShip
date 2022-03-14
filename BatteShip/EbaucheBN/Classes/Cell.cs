using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define an element "cell" used for every tile of the Battleship's grid.

    #region CellTypeEnum
    //Enumeration used to distinguish ships from water in game
    public enum cellType
    {
        Water,
        Ship
    }
    #endregion
    #region CellClassDefinition
    public class Cell
    {
        #region Variables
        public Button cellButton = new Button();        //Button to interact with the cell
        public cellType typeOfCell = new cellType();    //Defines whether or not there is a ship on this cell

        public string cellCoordinate;                   //Corresponds to the usual definition of battleship's cells. For instance 'A4', 'B3', 'J10'...

        public bool Ally;                               //Defines whether or not the cell is part of the player's or AI's grid.
                                                        //Set to true if part of the player's grid.
        public bool HitYet;                             //Defines whether or not the cell has been hit yet.

        public int cellIndex;                           //Corresponds to the index of the cell with respect to the grid.
        public int X;                                   //Corresponds to the Row of the cell amongst the grid.
        public int Y;                                   //Corresponds to the Column of the cell amongst the grid.
        #endregion
        #region Constructor
        public Cell(cellType SetCellType, bool SetAlly, int x, int y, Grid UI) 
        {
            typeOfCell = SetCellType;       //Sets the type of cell according to the input send into the constructor.

            Ally = SetAlly;                 //Sets whether the cell is part of the player's grid according to the input send into the constructor.
            HitYet = false;                 //By definition sets the cell status to "not hit yet".
            X = x;                          //Sets the Row of the cell according to the input send into the constructor.
            Y = y;                          //Sets the Column of the cell according to the input send into the constructor.

            cellCoordinate = Convert.ToChar(y + 65) + Convert.ToString(x + 1);      //Assigns the cell coordinate depending on the row and column.
            cellIndex = x * GameDesign.GridSizeY + y + 1;                           //Sets the index of the cell depending on the row and column.

            cellButton.Width = UI.Width * 0.85 / (GameDesign.GridSizeX + 1);        //Sets the button's width which depends on the Grid sent into the constructor.
            cellButton.Height = UI.Height * 0.85 / (GameDesign.GridSizeY + 1);      //Sets the button's height which depends on the Grid sent into the constructor.
            cellButton.Background = new SolidColorBrush(GameDesign.DefaultColor);   //Sets the button background color depending on the color presets in GameDesign.

            cellButton.Click += OnClick; //Subscribes the Click event to the Onclick action.
        }
        #endregion
        #region Click Event
        void OnClick(object sender, RoutedEventArgs e) //Function used to respond to user interaction with the cell's button.
        {
            MainPage.Instance.Click(this);
        }
        #endregion
    }
#endregion
}