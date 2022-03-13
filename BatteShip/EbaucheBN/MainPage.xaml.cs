using EbaucheBN.Classes;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EbaucheBN
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        public static MainPage Instance;

        public BattleShipGrid EnemyBattleShipGrid = new BattleShipGrid();
        public BattleShipGrid AllyBattleShipGrid = new BattleShipGrid();
        public ShipSetupManager shipSetupManager = new ShipSetupManager();

        public GameManager myGameManager = new GameManager();

        GridManager myGridManager = new GridManager();
        MenuManager myMenuManager = new MenuManager();

        public List<Ship> allyShips = new List<Ship>();

        public bool GameStarted = false;
        public bool GameOver = false;

        public bool Selection = false;

        public MainPage()
        {
            Instance = this;

            this.InitializeComponent();
            myMenuManager.Initialize(MenuUI);
        }
        public void GridInitialization(BattleShipGrid BSGridToInitialize, bool Ally)
        {
            BSGridToInitialize.initGrid(Ally);

            myGridManager.Setup(Ally ? allyGrid : enemyGrid, BSGridToInitialize, Ally);
        }
        public void Click(Cell CellClicked)
        {
            if (Selection && CellClicked.Ally)
                shipSetupManager.ProceedShipSetup(CellClicked);
            else if (!CellClicked.Ally && !myGameManager.AItoPlay && !CellClicked.HitYet && !GameOver)
                myGameManager.Hit(CellClicked, false);
        }
        public void StartGame()
        {
            shipSetupManager.GetAllyShipCoordinates();
            shipSetupManager.ChangeAllyShipsColor();

            ShipSetupUI.Children.Clear();
            ShipSetupSelectionUI.Children.Clear();

            GridInitialization(EnemyBattleShipGrid, false);

            myGameManager.Initialize();
        }
        public void StartNewGame()
        {
            ResetGrid(InGameUI);
            ResetGrid(allyGrid);
            ResetGrid(enemyGrid);
            ResetGrid(MenuUI);
            ResetGrid(ShipSetupSelectionUI);
            ResetGrid(ShipSetupUI);

            GameStarted = false;
            Selection = false;
            GameOver = false;

            allyShips.Clear();

            EnemyBattleShipGrid = new BattleShipGrid();
            AllyBattleShipGrid = new BattleShipGrid();
            shipSetupManager = new ShipSetupManager();
            myGameManager = new GameManager();
            myGridManager = new GridManager();
            myMenuManager = new MenuManager();

            myMenuManager.Initialize(MenuUI);
        }
        private void ResetGrid(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
        }
        public Grid GetShipSetupUI()
        {
            return ShipSetupUI;
        }
        public Grid GetShipSelectionSetupUI()
        {
            return ShipSetupSelectionUI;
        }
        public Grid GetPlayerGrid()
        {
            return allyGrid;
        }
        public Grid GetTestGrid()
        {
            return TestGrid;
        }
        public Grid GetInGameUI()
        {
            return InGameUI;
        }
        public BattleShipGrid GetAllyBSGrid()
        {
            return AllyBattleShipGrid;
        }
        public BattleShipGrid GetEnemyBSGrid()
        {
            return EnemyBattleShipGrid;
        }
        public Grid GetEnemyGrid()
        {
            return enemyGrid;
        }
        public void CoordReset()
        {
            shipSetupManager.ShipSelectionText.Text = "Select a ship to set its coordinates";
            shipSetupManager.startGameButton.button.IsEnabled = false;
            shipSetupManager.startGameButton.textBlock.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
