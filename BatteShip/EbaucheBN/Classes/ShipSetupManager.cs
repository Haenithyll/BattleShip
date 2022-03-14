using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define a list of 'ShipSelection' elements according to the ships in GameDesign manage the setup process.

    #region ShipSetupManagerClassDefinition
    public class ShipSetupManager
    {
        #region Variables
        public HashSet<ShipSelection> ShipSelectionList = new HashSet<ShipSelection>();   //Creates a hashset of ShipSelection to setup every ship in GameDesign. 
        public TextBlock ShipSelectionText = new TextBlock();                   //Defines a new TextBlock to give the user instructions during the selection process.
        public StartGameButton startGameButton;                                 //Defines a single startGameButton whose purpose will be to launch the game. 
        public ShipSelection CurrentShip;                                       //Defines a ShipSelection buffer to store the current shipselection for testing.

        private Grid UI, SelectionUI;                                           //Defines 2 Grid elements necessary to display the various buttons necessary.
            
        private List<Cell> CurrentShipCells = new List<Cell>();                 //Creates a list of cells that stores the user's selection for the current ship.
        public List<Cell> ShipCells = new List<Cell>();                         //Creates a list of cells that stores every cell already selected to position ships.
        #endregion
        #region Initialization
        public void Initialize()                    //Proceeds to retrieve the display grids from mainpage and initializes the instruction text and shipselections.
        {
            UI = MainPage.Instance.GetShipSetupUI();                    //Retrieves the grid that will display the instruction text.
            SelectionUI = MainPage.Instance.GetShipSelectionSetupUI();  //Retrieves the grid that will display the 'shipselection' elements.
            InitializeInstructionText();                                //Initializes the instruction text.
            InitializeShipSelection();                                  //Initializes the ship selections.
        }
        private void InitializeInstructionText()    //Proceeds to setup and display the instruction text on the corresponding Grid. 
        {
            Button ShipSelectionButton = new Button();                                      //Definition of a new button to hold the textblock.

            ShipSelectionText.Text = "Select a ship to set its coordinates";                //Sets the text to the default instruction.
            ShipSelectionText.Foreground = new SolidColorBrush(Colors.White);               //Sets the font color to white to contrast with the button's color.
            ShipSelectionText.FontSize = UI.Width / ShipSelectionText.ToString().Length;    //Sets the fontsize to match the button's size.

            ShipSelectionButton.Width = UI.Width;                                   //Sets the button width depending on the grid sent as input in the constructor.
            ShipSelectionButton.Height = UI.Height;                                 //Sets the button length depending on the grid sent as input in the constructor.
            ShipSelectionButton.Content = ShipSelectionText;                        //Sets the content of the button to be the instructiontext.
            ShipSelectionButton.IsEnabled = false;                                  //Disables the button since no user interaction is necessary.

            UI.Children.Add(ShipSelectionButton);                                   //Adds the button to children of the corresponding Grid for display.
        }
        private void InitializeShipSelection()      //Proceeds to setup and display every ShipSelection element on the corresponding Grid. 
        {
            for (int ColumnAmount = 0; ColumnAmount < GameDesign.MaxSize + 2; ColumnAmount++)     //For every column necessary :                                                                                       
                SelectionUI.ColumnDefinitions.Add(new ColumnDefinition());                              //Adds a new column definition.

            for (int ShipCount = 0; ShipCount < GameDesign.ShipCount; ShipCount++)                //For every ship that has to be setup :
            {
                SelectionUI.RowDefinitions.Add(new RowDefinition());                                    //Adds a new row definition.

                ShipSelection newShipSelectionButton =                                                  //Creates a new ShipSelection to setup the current ship.
                            new ShipSelection(ShipCount, SelectionUI, GameDesign.ShipArray[ShipCount]); 
                ShipSelectionList.Add(newShipSelectionButton);                                          //Adds the newly created ShipSelection to the list.
            }

            startGameButton = new StartGameButton(SelectionUI);                    //Creates a unique startgame button for display at the bottom of the Grid.
        }
        #endregion
        #region Setup Process
        #region Setup Beginning 
        public void ResetCurrentBoatSelection()                            //Proceeds to reset any incomplete selection.
        {
            if (CurrentShipCells.Count > 0)                                         //If cells have been selected and the selection is not complete :         
            {
                foreach (Cell cell in CurrentShipCells)                                     //For each cell in that case :
                {
                    ShipCells.Remove(cell);                                                         //Removes the cells from the general list.
                    cell.cellButton.Background =                                                    //Sets the background color back to default.
                            new SolidColorBrush(GameDesign.DefaultColor);
                }
                foreach (Coordinate coord in CurrentShip.Coordinates)                       //For each coordinate of the current ship :
                    coord.TextBlock.Text = string.Empty;                                            //Resets the cell position displayed.

                CurrentShipCells.Clear();                                                   //Clears the list of currently selected cells.
            }
        }
        public void StartCoordinateSelection(ShipSelection ButtonClicked)  //Proceeds to change the selection status in MainGame and updates the currentship value.
        {
            MainPage.Instance.Selection = true;                                 //Sets the selection status to true in MainGame.
            CurrentShip = ButtonClicked;                                        //Sets the current ShipSelection value to be the one that was clicked.
        }
        public void UpdateButtonColor(ShipSelection ButtonClicked)         //Updates the ships' button color to inform the user which ship is being setup. 
        {
            foreach (ShipSelection Button in ShipSelectionList)                 //For each ShipSelection in the list :
            {
                if (Button != ButtonClicked)                                                    //If the current shipselection and and the one selected dont match.
                    Button.ShipButton.Background = new SolidColorBrush(Button.CurrentColor);            //Sets the background color of the ship button to its
                                                                                                        //current color.
                else                                                                            //Else :
                    Button.ShipButton.Background = new SolidColorBrush(GameDesign.ShipSelectColor);     //Sets the background color of the ship button to the 
            }                                                                                           //preset in the GameDesign.
        }
        #endregion
        #region Setup in progress
        public void ProceedShipSetup(Cell CellClicked)      //Proceeds to setup the ships :
                                                                    // - ensures ships are not overlapping.
                                                                    // - ensures ships are legally positioned.
                                                                    // - updates the displayed elements for the user.
        {
            if (!ShipCells.Contains(CellClicked))                       //If the cell selected to position the ship has not been already selected for another ship :
            {
                CellClicked.cellButton.Background =                            //Sets the background color to the preset in GameDesign.
                    new SolidColorBrush(GameDesign.CellSelectColor);          
                                                                              
                ShipCells.Add(CellClicked);                                    //Adds the clicked cell to the list of cells already selected.
                                                                              
                CurrentShipCells.Add(CellClicked);                             //Adds the clicked cell to the list of cells selected for the ship.
                CurrentShipCells.Sort(CompareCellsByCoordinate);               //Sorts the Cells to be displayed in the right order in the coordinates.

                for (int count = 0; count < CurrentShipCells.Count; count++)   //For every cell selected for the ship currently being setup :
                    CurrentShip.Coordinates[count].TextBlock.Text =                     //Sets the coordinate text to display the cell position.
                        CurrentShipCells[count].cellCoordinate;
                    
                if (CurrentShipCells.Count == CurrentShip.ShipSize)            //If every ship cell has been positioned :
                {
                    MainPage.Instance.Selection = false;                                //Disables the bool in MainPage until another ship is being positioned. 
                    CurrentShip.ShipButton.Background =                                 //Sets the background of the button to the preset in GameDesign.
                        new SolidColorBrush(GameDesign.DefaultColor);

                    if (VerifyCoordinates())                                            //If the coordinates are legally positioned :
                    {
                        for (int count = 0; count < CurrentShip.ShipSize; count++)          //For every coordinate of the shipselection :
                        {
                            CurrentShip.Coordinates[count].Button.Background =                  //Sets the background color to the preset in GameDesign.
                                new SolidColorBrush(GameDesign.ShipCoordColor);                     //Here indicates the coordinate is set.
                            CurrentShipCells[count].cellButton.Background =                     //Sets the background color of the cells being selected to the
                                new SolidColorBrush(GameDesign.ShipColor);                          //preset in GameDesign. Here indicates a ship is positioned.
                        }

                        CurrentShip.ShipCells.AddRange(CurrentShipCells);                   //Sets the ShipSelection cell list to match the current ship position.
                        CurrentShip.Complete = true;                                        //Sets the bool to true to indicate the ship's positioning is complete.

                        CurrentShip.ShipName.Foreground =                                   //Sets the textcolor to white to contrast with the button's background.
                            new SolidColorBrush(Colors.White);
                        CurrentShip.ShipButton.Background =                                 //Sets the background color to the preset in GameDesign.
                            new SolidColorBrush(GameDesign.ShipColor);                              //Here indicates the ship has been positioned.
                        CurrentShip.CurrentColor = GameDesign.ShipColor;                    //Updates the currentcolor value to match the current color.

                        CurrentShip.reset.button.IsEnabled = true;                          //Enables the Reset button to allow the user to reset his selection.
                        CurrentShip.reset.button.Background =                               //Sets the background of the button to the preset in GameDesign.
                            new SolidColorBrush(GameDesign.ResetButtonColor);                       //Here lights up the reset button to indicate it can be pressed.
                        CurrentShip.reset.textBlock.Foreground =                            //Sets the fontcolor to White to contrast with the button's background.
                            new SolidColorBrush(Colors.White);
                    }
                    else                                                               //Else (meaning the selection is not legal) :      
                    {
                        for (int count = 0; count < CurrentShipCells.Count; count++)        //For every cell used for the positioning of the ship :
                        {
                            ShipCells.Remove(CurrentShipCells[count]);                              //Removes this cell from the general cell list.
                            CurrentShip.Coordinates[count].TextBlock.Text = string.Empty;           //Resets the coordinates text.
                            CurrentShipCells[count].cellButton.Background =                         //Sets the background of the button to the preset in GameDesign.
                                new SolidColorBrush(GameDesign.DefaultColor);                               //Here indicates the cell does not host a ship.
                        }
                    }

                    ShipSelectionText.Text = "Select a ship to set its coordinates";   //Updates the instruction text to the next instruction.
                    CurrentShipCells.Clear();                                          //Clears the list of the current cells selected.
                    TestSelectionComplete();                                           //Tests whether every ship has been positioned.
                }
            }
        }
        private void TestSelectionComplete()                //Proceeds to verify whether every ship has been set up. 
        {
            if (ShipCells.Count == GameDesign.TotalSize)                //If every ship cell has been positioned.    
            {
                ShipSelectionText.Text = "Press Play Game to play !";       //Updates the instruction text to the next instruction.
                startGameButton.button.IsEnabled = true;                    //Enables the start game button since every ship have been rightfully setup.
                startGameButton.textBlock.Foreground =                      //Sets the fontcolor to White to contrast with the button's color.
                    new SolidColorBrush(Colors.White);
            }
        }
        private bool VerifyCoordinates()                    //Returns whether the selected coordinates for the current ship are legally positioned. 
        {
            int difference = CurrentShipCells[1].cellIndex                                      //Computes the alignment between 2 cells of the ship.
                                    - CurrentShipCells[0].cellIndex;

            if (Math.Abs(difference) != 1 && Math.Abs(difference) != GameDesign.GridSizeX)      //If the 2 cells position are not next to each other :    
                return false;                                                                           //Returns false.

            for (int coordIndex = 0; coordIndex < CurrentShip.ShipSize - 1; coordIndex++)       //For every cell ship position :
            {
                if (CurrentShipCells[coordIndex].cellIndex % GameDesign.GridSizeX == 0                  //If the ship is positioned such that one part of the ship 
                                                                    && Math.Abs(difference) == 1)       //is on the right end of the grid and the other on the left
                                                                                                        //end of the grid :
                    return false;                                                                           //Returns false.

                if (CurrentShipCells[coordIndex + 1].cellIndex                                          //If the ship's alignment does not match the one computed :
                                            - CurrentShipCells[coordIndex].cellIndex != difference)
                    return false;                                                                           //Returns false.
            }

            return true;                                                                        //Returns true if every condition is valid.
        }
        private static int CompareCellsByCoordinate(Cell cell1, Cell cell2) //Orders the Cell list by index and letter. 
        {
            if (cell1 == null)                                          //If the first cell is null :
            {
                if (cell2 == null)                                              //If the second cell is null :
                    return 0;                                                       //Returns no modification to the order.
                else                                                            //Else :
                    return -1;                                                      //Returns the second cell to be positioned first in the list.
            }
            else                                                        //Else :
            {
                if (cell2 == null)                                              //If the second cell is null :
                    return 1;                                                       //Returns the first cell to be positioned first in the list.
                else                                                            //Else :
                    return cell1.cellIndex.CompareTo(cell2.cellIndex);              //Returns the comparison of the cells' index.
            }
        } 
        #endregion
        #region Setup Complete
        public void GetAllyShipCoordinates()        //Proceeds to set the ships' position to match the currently selected cells in the shipselection.
        {
            foreach (ShipSelection shipSelection in ShipSelectionList)              //For each shipselection in the list :
                shipSelection.ship.Position.AddRange(shipSelection.ShipCells);              //Sets the ships' position to match the final selection.
        }
        public void ChangeAllyShipsColor()          //Proceeds to change the player's ships cells' color when the setup is finished.  
        {
            foreach (Ship allyShip in MainPage.Instance.PlayerShips)                      //For each ship in the player ship list :
            {
                foreach (Cell cell in allyShip.Position)                                        //For each cell upon which the ship is positioned :
                    cell.cellButton.Background = new SolidColorBrush(GameDesign.InGameShipColor);       //Sets the background to the preset in GameDesign.
            }
        }
        #endregion
        #endregion
        #region Click Event
        public void Click(ShipSelection ButtonClicked)      //Proceeds to updates the instruction text when a ship has been selected for positioning.
        {
            ShipSelectionText.Text =                                            //Sets the instruction text to the corresponding instruction.
                "Now Click on the grid to select the ship's coordinates";  
            ResetCurrentBoatSelection();                                        //Proceeds to reset any undergoing selection that has not been completed.
            UpdateButtonColor(ButtonClicked);                                   //Change the button color to the preset in GameDesign.
            StartCoordinateSelection(ButtonClicked);                            //Launches the selection process for the selected shipselection.
        }
        #endregion
    }
    #endregion
}