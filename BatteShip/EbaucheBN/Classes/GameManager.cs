using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI;

namespace EbaucheBN.Classes
{
    //This class' purpose is to manage the game which entails :
            // - Proceeding to the AI's initialization.
            // - Updating Player/AI turn to hit the opponent's grid.
            // - Striking the grid.
            // - Testing whether or not a ship is sunk.
            // - Updating the shipCount whenver a ship is sunk.
            // - Stopping the game when the Player/AI has sunk every opponent's ships.

    #region GameManagerClassDefinition
    public class GameManager
    {
        #region Variables
        private AI enemyAI = new AI();                              //Definition of a new AI.

        private Grid UI = new Grid();                               //Definition of a grid that will display the current shipcount of each side.

        public List<Ship> PlayerShips = new List<Ship>();           //Creation of a list of ships which stores the player's ships.
        public List<Ship> AIShips = new List<Ship>();               //Creation of a list of ships which stores the AI's ships.

        public bool AItoPlay = new bool();                          //Definition of a bool that indicates whether or not the AI is playing.

        private int currentHitCounter = new int();                  //This int will be used to determine whose turn it is to play.

        public List<Cell> AIShipCellHits = new List<Cell>();        //Creation of a list of cells which stores every AI ship cells hit. 
        public List<Cell> PlayerShipCellHits = new List<Cell>();    //Creation of a list of cells which stores every Player ship cells hit. 

        private List<TextBlock> ShipCount = new List<TextBlock>();  //Creation of a list of TextBlocks which stores the player's and AI's ship count.
        #endregion
        #region Initialization
        public void Initialize()                        //Initialization of the turn bool, AI & UI. Retrieval of the ships and update of celltypes.
        {
            AItoPlay = false;                                   //The player is the first to play.
            enemyAI.Initialize();                               //Proceed to initialize the AI.

            PlayerShips = MainPage.Instance.PlayerShips;          //Retrieve the Player's Ships.
            AIShips.AddRange(enemyAI.AIShips);                  //Retrieve the AI's Ships.

            UpdateAIShipCellType();                             //Update the AI's Ships CellType (=> every cellType is set to Ship)
            UpdatePlayerShipCellType();                         //Update the Player's Ships CellType (=> every cellType is set to Ship)

            InitializeUI();                                     //Initialize the UI : Display the player's and AI's shipCount.
        }
        #endregion
        #region UIManagement
        private void InitializeUI()                                                 //Proceeds to initialize the UI. 
        {
            UI = MainPage.Instance.GetInGameUI();                                       //Retrieves the Grid from MainPage.
            UI.RowDefinitions.Add(new RowDefinition());                                 //Addition of a new row.

            for (int rowIndex = 0; rowIndex < 4; rowIndex++)                            //For each of the 4 rows (2 * (1 counter + 1 message)) :
            {
                UI.RowDefinitions.Add(new RowDefinition());                                //Addition of a new row.
                Button UIButton = new Button();                                            //Creation of a new button to add on the grid.
                UIButton.Background = new SolidColorBrush(GameDesign.DefaultColor);        //Sets the color of the button according to Game Design choices.
                UIButton.Width = 2 * UI.Width / 3;                                         //Sets the width of the button depending on the UI sent into the constructor.
                UIButton.Height = UI.Height / 6;                                           //Sets the height of the button depending on the UI sent into the constructor.

                TextBlock UITextBlock = new TextBlock();                                    //Creation of a new textblock that will contain the message or the counter.
                UITextBlock.FontSize = rowIndex % 2 == 0 ? 26 : 32;                         //Sets the fontsize of the textblock depending on the content.
                UITextBlock.Foreground = new SolidColorBrush(Colors.Black);                 //Sets the color of the text to black to have contrast with the button.

                if (rowIndex % 2 == 1)                                                      //If index is even :
                    ShipCount.Add(UITextBlock);                                                 //Adds the current textblock to the list.

                switch (rowIndex)                                                           //Depending on the row index :
                {
                    case 0:                                                                     //If it equals 0 :
                        UITextBlock.Text = "Player Ship Count";                                     //Displays the message 'Player ship count'.
                        break;
                    case 1:                                                                     //If it equals 1 :
                        UITextBlock.Text = PlayerShips.Count.ToString();                            //Displays the counter of the Player's ships.
                        break;
                    case 2:                                                                     //If it equals 2 :
                        UITextBlock.Text = "AI Ship Count";                                         //Displays the message 'AI ship count'.
                        break;
                    case 3:                                                                     //If it equals 3 :
                        UITextBlock.Text = AIShips.Count.ToString();                                //Displays the counter of the AI's ships.
                        break;
                }

                UIButton.Content = UITextBlock;                                             //Sets the textblock as the button's content.
                UI.Children.Add(UIButton);                                                  //Adds the textblock to the children of the grid.
                UIButton.HorizontalAlignment = HorizontalAlignment.Center;                  //Sets the button alignment to center.
                Grid.SetRow(UIButton, rowIndex + (rowIndex < 2 ? 0 : 1));                   //Sets the row position of the button depending on the row index.
            }
        }
        private void UpdateUI(bool AI)                                              //Updates the player/AI shipCount upon a ship being sunk. 
        {
            TextBlock newTextBlock = new TextBlock();                                             //Creation of a new TextBlock to replace the ones currently displayed.
            newTextBlock.FontSize = 32;                                                           //Sets the fontsize to match the ones of the textblock currently displayed.
            newTextBlock.Foreground = new SolidColorBrush(Colors.Black);                          //Sets the font color to black to contrast with the button's color.

            newTextBlock.Text = AI ? PlayerShips.Count.ToString() : AIShips.Count.ToString();     //Updates the player/AI count depending on whose ship just got sunk.

            ShipCount[AI ? 0 : 1].Text = newTextBlock.Text;                                       //Replacement of the current textblock by the new one.
        }
        #endregion
        #region Hit
        public void Hit(Cell cellHit, bool AIHit)   //Strikes the cell sent as input, verifies if a ship is hit or sunk, updates UI if necessary, switch turns.  
        {
            cellHit.HitYet = true;                                  //Change of status to 'hityet' for the cell sent as an input. 

            if (AIHit)                                              //If the AI is the one to strike :
            {
                if (cellHit.typeOfCell == cellType.Ship)                        //If the cell hit is a ship :
                {
                    AppendPlayerCellHit(cellHit);                                       //Add the cell to the list of the player's cell hits.

                    if (!TestShipSunk(AIHit))                                                   //If the Ship is not sunk :
                    {
                        cellHit.cellButton.Background = new SolidColorBrush(GameDesign.ShipHitColor);       //Sets the background to the selected GameDesign color.
                        enemyAI.AddPotentialShipPosition(cellHit);                                          //Add surrounding cells to the list for future hit.
                    }
                    else                                                                        //else, meaning the ship got sunk :
                        enemyAI.UpdatePotentialShipPosition(PlayerShipCellHits);                            //Update the cells to hit to avoid striking unnecessary cells.
                }
                                                                              //Else :
                else                                                                    //Sets the background to the selected GameDesign Color.                                   
                    cellHit.cellButton.Background = new SolidColorBrush(GameDesign.WaterColor);
            }                                                       
            else                                                   //Else :
            {                                                                           
                if (cellHit.typeOfCell == cellType.Ship)                        //If the cell hit is a ship :
                {
                    AppendAICellHit(cellHit);                                           //Add the cell to the list of the AI's cell hits.

                    if (!TestShipSunk(false))                                           //If the Ship is not sunk :
                        cellHit.cellButton.Background = new SolidColorBrush(GameDesign.ShipHitColor);       //Sets the background to the selected GameDesign Color.
                }
                                                                                //Else :
                else                                                                    //Sets the background to the selected GameDesign Color.
                    cellHit.cellButton.Background = new SolidColorBrush(GameDesign.WaterColor);             
            }

            ManagePlayerTurn();                                     //Switches Players turns.

            if (AItoPlay)                                           //If AI is the one to play :
                enemyAI.AIHit(PlayerShipCellHits);                              //Use the AI hit function to hit one of the Player's cells.
        }
        #endregion
        #region Ship Sink
        public bool TestShipSunk(bool AI)                           //Returns whether or not a ship is sunk.
        {
            int hitCount;                                                       //Definition of an int which keeps track of the amount of positions hit for a ship.
            List<Cell> CellsToTest = AI ? PlayerShipCellHits : AIShipCellHits;  //Creation of a list of cells that corresponds to the opponent's ships hits.

            foreach (Ship ship in AI ? PlayerShips : AIShips)                   //For each ship in the opponent ship list :
            {                       
                hitCount = 0;                                                       //Is set to 0.

                foreach (Cell position in ship.Position)                            //For each position for the current ship :
                {
                    if (CellsToTest.Contains(position))                                 //If the list of cells hit contains the current ship position :
                        hitCount++;                                                             //Increments hitCount.
                    else                                                                //Else : 
                        break;                                                                  //Exits the loop.
                }
                if (hitCount == ship.Size)                                          //If the amount of cells hit for this ship equals its size :
                {
                    SinkShip(ship, AI);                                                 //Sink the corresponding ship.
                    return true;                                                        //return true;
                }
            }
            return false;                                                       //return false;
        }
        private void SinkShip(Ship shipToSink, bool AI)             //Proceeds to sink a ship. 
        {
            List<Ship> shipToRemove = AI ? PlayerShips : AIShips;                                   //Retrieves the correct Ship list.
            List<Cell> hitCellsToRemove = AI ? PlayerShipCellHits : AIShipCellHits;                 //Retrieves the correct hit cells list.

            foreach (Cell position in shipToSink.Position)                                          //For each position of the ship :
            {
                hitCellsToRemove.Remove(position);                                                          //Removes this position from the hit cells.
                position.cellButton.Background = new SolidColorBrush(GameDesign.ShipSunkColor);             //Changes the button background to the GameDesign Color.
            }

            shipToRemove.Remove(shipToSink);                                                        //Removes the ship from the corresponding list.
            UpdateUI(AI);                                                                           //Updates the shipCount.
            TestGameOver();                                                                         //Verifies whether the game is over or not.
        }
        private void TestGameOver()                                 //Verifies whether the game is over or not. 
        {
            if(AIShips.Count == 0 || PlayerShips.Count == 0)        //If one of the players does not have any ship left :
            {
                UI.Children.Clear();                                      //Removal of every children of the grid.
                                                                          //Creation of 3 EngameButtons, 2 to display messages and 1 to quit the game.                                                                        
                
                EndGameButton GameOver = new EndGameButton(UI, 0, "Game Over", false);          
                EndGameButton YouWinLose = new EndGameButton(UI, 1, AIShips.Count == 0 ? "You Win !" : "You Lose !", false);
                EndGameButton BackToMenu = new EndGameButton(UI, 3, "Exit Game", true);

                MainPage.Instance.GameOver = true;                        //Set the GameOver variable to true.
            }
        }
        #endregion
        #region CellType Update
        private void UpdatePlayerShipCellType()         //Updates the celltype of every player ship => sets to Ship. 
        {
            foreach (Ship ship in MainPage.Instance.PlayerShips)  //For each player ship :
            {
                foreach (Cell cell in ship.Position)                    //For each cell position of this ship :
                    cell.typeOfCell = cellType.Ship;                            //The type of this cell is set to ship.
            }
        }
        public void UpdateAIShipCellType()              //Updates the celltype of every player ship => sets to Ship. 
        {
            foreach (Ship ship in AIShips)                      //For each AI ship :
            {
                foreach (Cell cell in ship.Position)                    //For each cell position of this ship :
                    cell.typeOfCell = cellType.Ship;                            //The type of this cell is set to ship.
            }
        }
        #endregion
        #region Turn Management
        private void ManagePlayerTurn()         //Alternates the turns between Player and AI. 
        {
            currentHitCounter++;                                //Increments the currenthitcounter.
            if (currentHitCounter == GameDesign.HitPerTurn)     //If the currenthitcounter equals the amount of strike per turn set in GameDesign :
            {
                currentHitCounter = 0;                                  //Resets the currentHitCounter.
                AItoPlay = AItoPlay ? false : true;                     //Changes whose turn it is to play
            }
        }
        #endregion
        #region CellHitList Addition
        private void AppendPlayerCellHit(Cell cellHit)      //Adds the cell to the Player ship hit list. 
        {
            PlayerShipCellHits.Add(cellHit);
        }
        private void AppendAICellHit(Cell cellHit)          //Adds the cell to the AI ship hit list. 
        {
            AIShipCellHits.Add(cellHit);
        }
        #endregion
    }
    #endregion
}