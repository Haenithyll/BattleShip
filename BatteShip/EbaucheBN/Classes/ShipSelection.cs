using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to have an element that contains both :
                // - A ship button to enable the player to select which ship to setup.
                // - A reset button to enable the player to reset the cell selection.
                // - A list of Coordinates that corresponds to the size of the ship and will display the ships positions.
                // - A list of Cells corresponding to the Cells selected.
                // - A ship that determines the amount of coordinates necessary.

    #region ShipSelectionClassDefinition
    public class ShipSelection
    {
        #region Variables
        public Button ShipButton = new Button();                        //Definition of a button that enables the player to select which ship to setup.

        public Ship ship;                                               //Definition of a ship associated that determines the amount of Coordinates necessary.
        public List<Coordinate> Coordinates = new List<Coordinate>();   //Creation of a list of coordinates that corresponds to the position of the ship.
        public List<Cell> ShipCells = new List<Cell>();                 //Creation of a list of cells to store the currently selected cells during setup.

        public int ShipSize;                                            //Definition of an integer which stores the length of the ship.
        public bool Complete = false;                                   //Definition of a bool that informs whether every ship position has been set or not.
        public ResetButton reset;                                       //Definition of a resetbutton to reset the current selection.
        public Action ResetCoordinatesAction;                           //Definition of an action to send as an input in the constructor of the reset button.
        public TextBlock ShipName = new TextBlock();                    //Definition of a TextBlock to store the shipname.

        public Color CurrentColor = GameDesign.DefaultColor;            //Definition of a Color variable to store the currentcolor of the button. 
        #endregion
        #region Constructor
        public ShipSelection(int ShipCount, Grid UI, Ship inputShip)
        {
            ResetCoordinatesAction += ResetCoordinates;                                 //Subscribes the ResetCoordinateAction event to the ResetCoordinate action.
                                                                                        
            reset = new ResetButton(ShipCount, UI, ResetCoordinatesAction);             //Creates a new Reset button.
                                                                                        
            ShipSize = GameDesign.ShipArray[ShipCount].Size;                            //Sets the shipsize to the inputship length.
                                                                                        
            ShipName.Text = GameDesign.ShipArray[ShipCount].Name;                       //Sets the shipname to the inputship name.
            ShipName.FontSize = 20;                                                     //Sets the fontsize to match the button size.
            ShipName.Foreground = new SolidColorBrush(Colors.Black);                    //Sets the font color to black to contrast with the button's color.

            ShipButton.Width = UI.Width / (GameDesign.MaxSize + 3);                     //Sets the button's width which depends on the Grid sent into the constructor.
            ShipButton.Height = UI.Height / (GameDesign.ShipCount + 2);                 //Sets the button's height which depends on the Grid sent into the constructor.
            ShipButton.Background = new SolidColorBrush(GameDesign.DefaultColor);       //Sets the button background color depending on the color presets in GameDesign.
            ShipButton.Content = ShipName;                                              //Sets the shipname to be the button's content.
            ShipButton.Click += OnClick;                                                //Subscribes the Click Event to the OnClick action.

            UI.Children.Add(ShipButton);                                                //Adds the shipbutton to the children of the grid.
            Grid.SetRow(ShipButton, ShipCount);                                         //Sets the row position of the shipbutton depending on the currentshipcount.
            Grid.SetColumn(ShipButton, 0);                                              //Sets the column position of the shipbutton to be at the far left of the grid.

            for(int CoordAmount = 0; CoordAmount < ShipSize; CoordAmount++)  //For every coordinate necessary :
            {
                Coordinate newCoord = new Coordinate("", UI);                       //Creates a new Coordinate.

                UI.Children.Add(newCoord.Button);                                   //Adds the Coordinate to the children of the grid.

                Grid.SetRow(newCoord.Button, ShipCount);                            //Sets the row position of the Coordinate depending on the currentshipcount.
                Grid.SetColumn(newCoord.Button, CoordAmount+1);                     //Sets the column position of the Coordinate depending on the last Coord position.
                Coordinates.Add(newCoord);                                          //Adds the Coordinate to the list.
            }

            ship = inputShip;                                               //Sets the ship to the one sent as an input in the constructor.
            MainPage.Instance.allyShips.Add(ship);                          //Adds the ship to the list of Player's ships on MainPage.
        }
        #endregion
        #region Click Event
        void OnClick(object sender, RoutedEventArgs e)          //If the selection is not complete, launch the ship position selection.
        {
            if(!Complete)
                MainPage.Instance.shipSetupManager.Click(this);
        }
        #endregion
        public void ResetCoordinates()
        {
            ShipName.Foreground = new SolidColorBrush(Colors.Black);
            ShipButton.Background = new SolidColorBrush(GameDesign.DefaultColor);

            foreach (Cell cell in ShipCells)
            {
                Complete = false;

                cell.cellButton.Background = new SolidColorBrush(GameDesign.DefaultColor);
                MainPage.Instance.shipSetupManager.ShipCells.Remove(cell);
            }

            foreach(Coordinate coord in Coordinates)
            {
                coord.TextBlock.Text = string.Empty;
                coord.Button.ClearValue(Button.BackgroundProperty);
            }
            CurrentColor = GameDesign.DefaultColor;

            reset.button.IsEnabled = false;
            reset.button.Background = new SolidColorBrush(GameDesign.DefaultColor);
            reset.textBlock.Foreground = new SolidColorBrush(Colors.Black);

            ShipCells.Clear();
            MainPage.Instance.CoordReset();
        }
    }
    #endregion
}