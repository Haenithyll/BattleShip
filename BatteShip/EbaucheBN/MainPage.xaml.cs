using EbaucheBN.Classes;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EbaucheBN
{
    //This is the page script that links every other script with the xaml. 
    //Its purpose is to enable the other script to access the various grids to display the elements and serve as a 'hub' thanks to the singleton pattern.

    public sealed partial class MainPage : Page
    {
        #region Variables

        public static MainPage Instance;

        public BattleShipGrid AIBattleShipGrid = new BattleShipGrid();      //Definition of the AI's BattleShipGrid. On this grid :
                                                                                    // - the AI will setup its ships at the beginning of the game.
                                                                                    // - the player will hit cells to attempt to sink all AI's ships.

        public BattleShipGrid PlayerBattleShipGrid = new BattleShipGrid();  //Definition of the Player's BattleShipGrid. On this grid :
                                                                                    // - the player will set up its ship before the game starts.
                                                                                    // - the AI will hit cells following a pattern to sink all player's ships.

        public ShipSetupManager shipSetupManager = new ShipSetupManager();  //Definition of a ShipSetupManager that will :
                                                                                    // - Allow the Player to set up his ships.
                                                                                    // - Ensure the player's ships are rightfully positionned before game start.

        public GameManager myGameManager = new GameManager();               //Definition of a GameManager that will :
                                                                                    // - Manage Player Turns, Set up the AI upon game launch and manage ships' status.

        GridManager myGridManager = new GridManager();              //Definition of a GridManager that displays every cell of a BattleShipGrid.
        MenuManager myMenuManager = new MenuManager();              //Definition of a MenuManager that gives the player options upon launching the app.

        public List<Ship> PlayerShips = new List<Ship>();           //Creation of a List of Ships that stores every ship the Player has.

        public bool GameStarted = false;                            //Definition of a Bool that keeps track whether the game has started or not.
                                                                            //This bool is useful to determine elements to display depending on the game status.

        public bool GameOver = false;                               //Definition of a Bool that keeps track whether the game is over or not.
                                                                            //This bool is useful to determine elements to display depending on the game status.

        public bool Selection = false;                              //Definition of a Bool that keeps track whether the ship positioning is in progress or not.
                                                                            //This bool is useful to determine the reponse to the player's actions on the grid.
        #endregion
        #region Constructor
        public MainPage()
        {
            Instance = this;

            this.InitializeComponent();         //Initialization of every component necessary to operate scripts and xaml.
            myMenuManager.Initialize(MenuUI);   //Initialization of the Menu.
        }
        #endregion
        #region Initialization of Grids
        public void GridInitialization(BattleShipGrid BSGridToInitialize, bool Ally)    //Proceeds to Initialize grids with cells from a BattleShipGrid :
                                                                                                //The purpose being to display every clickable cell to interact with.
        {
            BSGridToInitialize.initGrid(Ally);                                                          //Initializes the BattleShipGrid that will be displayed.
            myGridManager.Setup(Ally ? allyGrid : enemyGrid, BSGridToInitialize, Ally);                 //Sets up the Grid to display every cell of the BSG.
        }
        #endregion
        #region Game Start
        public void StartGame()                         //Proceeds to Start the game. 
        {
            shipSetupManager.GetAllyShipCoordinates();      //Retrieves the player's selected ships' positions and changes their color.
            shipSetupManager.ChangeAllyShipsColor();        //Changes the color of the cells upon which ships are positionned.

            ShipSetupUI.Children.Clear();                   //Clears the unnecessary displayed elements.
            ShipSetupSelectionUI.Children.Clear();          //Clears the unnecessary displayed elements.

            GridInitialization(AIBattleShipGrid, false);    //Initialization of the AI Grid.

            myGameManager.Initialize();                     //Launches the game by initializing the gameManager.
        }
        public void StartNewGame()                      //Proceeds to Start a new game. 
                                                            //Important note : There is currently an issue with references upon starting a new game.
                                                                            //Therefore, this function is not called yet.
        {
            ResetGrid(InGameUI);                            //Proceeds to Reset every grid.
            ResetGrid(allyGrid);
            ResetGrid(enemyGrid);
            ResetGrid(MenuUI);
            ResetGrid(ShipSetupSelectionUI);
            ResetGrid(ShipSetupUI);

            GameStarted = false;                            //Resets every bool to their default false status.
            Selection = false;
            GameOver = false;

            PlayerShips.Clear();                            //Clears the player's ship list.

            AIBattleShipGrid = new BattleShipGrid();        //Sets new definitions for every element.
            PlayerBattleShipGrid = new BattleShipGrid();
            shipSetupManager = new ShipSetupManager();
            myGameManager = new GameManager();
            myGridManager = new GridManager();
            myMenuManager = new MenuManager();

            myMenuManager.Initialize(MenuUI);
        }
        private void ResetGrid(Grid grid)               //Proceeds to Reset the grid. 
        {
            grid.Children.Clear();                          //Clears every children of the grid.
            grid.RowDefinitions.Clear();                    //Clears the rows of the grid.
            grid.ColumnDefinitions.Clear();                 //Clears the columns of the grid.
        }
        #endregion
        #region Grids Retrieval
        public Grid GetShipSetupUI()                //Retrieves the corresponding grid. 
        {
            return ShipSetupUI;
        }
        public Grid GetShipSelectionSetupUI()       //Retrieves the corresponding grid. 
        {
            return ShipSetupSelectionUI;
        }
        public Grid GetPlayerGrid()                 //Retrieves the corresponding grid. 
        {
            return allyGrid;
        }
        public Grid GetAIGrid()                     //Retrieves the corresponding grid. 
        {
            return enemyGrid;
        }
        public Grid GetTestGrid()                   //Retrieves the corresponding grid. 
                                                            //This one allows testing during development.
        {
            return TestGrid;
        }
        public Grid GetInGameUI()                   //Retrieves the corresponding grid. 
        {
            return InGameUI;
        }
        public BattleShipGrid GetAllyBSGrid()       //Retrieves the corresponding battleShipGrid. 
        {
            return PlayerBattleShipGrid;
        }
        public BattleShipGrid GetEnemyBSGrid()      //Retrieves the corresponding battleShipGrid. 
        {
            return AIBattleShipGrid;
        }
        #endregion
        #region Coordinates Reset
        public void CoordReset()            //Proceeds to change the instruction text upon coordinates being reset.
        {
            shipSetupManager.ShipSelectionText.Text = "Select a ship to set its coordinates";           //Sets the instruction text to default instruction.
            shipSetupManager.startGameButton.button.IsEnabled = false;                                  //Disables the start game.
            shipSetupManager.startGameButton.textBlock.Foreground = new SolidColorBrush(Colors.Black);  //Sets the fontcolor to black in contrast with the button color.
        }
        #endregion
        #region Click Event
        public void Click(Cell CellClicked)                  //Proceeds to respond to user interaction with the grids. 
        {
            if (Selection && CellClicked.Ally)                                  //If the selection process is in progress :
                shipSetupManager.ProceedShipSetup(CellClicked);                         //Proceeds to setup the ship.
            else if (!CellClicked.Ally &&                                       //Else if the player clicks on the enemy grid when the game is in progress :
                !myGameManager.AItoPlay && !CellClicked.HitYet && !GameOver)            
                myGameManager.Hit(CellClicked, false);                                  //Strikes the cell.
        }
        #endregion
    }
}