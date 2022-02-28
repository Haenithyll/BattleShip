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

        GameManager myGameManager = new GameManager();
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
            if (Selection)
            {
                shipSetupManager.ProceedShipSetup(CellClicked);
            }
            if (!CellClicked.Ally)
            {
                CellClicked.cellButton.Background = CellClicked.typeOfCell == cellType.Water ? new SolidColorBrush(GameDesign.WaterColor) : new SolidColorBrush(GameDesign.ShipColor);
            }
        }

        public void StartGame()
        {
            foreach(ShipSelection shipSelection in shipSetupManager.buttons)
            {
                shipSelection.ship.Position.AddRange(shipSelection.ShipCells);
            }

            ShipSetupUI.Children.Clear();
            ShipSetupSelectionUI.Children.Clear();

            GridInitialization(EnemyBattleShipGrid, false);
            Test();
            //myGameManager.Initialize();
        }

        public void Test()
        {
            foreach(Ship allyShip in allyShips)
            {
                foreach (Cell cell in allyShip.Position)
                    cell.cellButton.Background = new SolidColorBrush(Colors.BlueViolet);
            }
        }

        public Grid GetShipSetupUI()
        {
            return ShipSetupUI;
        }
        public Grid GetShipSelectionSetupUI()
        {
            return ShipSetupSelectionUI;
        }

        public Grid GetAllyGrid()
        {
            return allyGrid;
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
