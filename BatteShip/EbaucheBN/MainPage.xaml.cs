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

        public BattleShipGrid AllyBattleShipGrid = new BattleShipGrid();
        public BattleShipGrid EnemyBattleShipGrid = new BattleShipGrid();
        public ShipSetupManager shipSetupManager = new ShipSetupManager();

        public GameManager myGameManager = new GameManager();
        
        GridManager myGridManager = new GridManager();
        MenuManager myMenuManager = new MenuManager();

        public HashSet<Ship> allyShips = new HashSet<Ship>();

        public bool GameStarted = false;

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
            else if (!CellClicked.Ally && !myGameManager.AItoPlay && !CellClicked.HitYet)
            {
                myGameManager.Hit(CellClicked, false);
            }
            else
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = CellClicked.cellIndex.ToString();
                CellClicked.cellButton.Content = textBlock;
            }
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
