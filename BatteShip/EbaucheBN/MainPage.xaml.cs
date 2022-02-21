using EbaucheBN.Classes;
using System.Collections.Generic;
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

        GridManager myGridManager = new GridManager();
        MenuManager myMenuManager = new MenuManager();

        public bool GameStarted = false;

        public bool Selection = false;

        private List<string> Compare = new List<string>();
        private string[] CompareArray;

        public MainPage()
        {
            Instance = this;

            this.InitializeComponent();
            myMenuManager.Initialize(MenuUI);
        }

        public void GridInitialization(BattleShipGrid BSGridToInitialize, bool Ally)
        {
            BSGridToInitialize.initGrid(Ally);

            myGridManager.Setup(Ally ? allyGrid : enemyGrid, BSGridToInitialize);
            myGridManager.SetSize(Ally ? allyGrid : enemyGrid);
        }

        public void Click(Cell CellClicked)
        {
            if (Selection)
            {
                CellClicked.cellButton.Background = new SolidColorBrush(GameDesign.CellSelectColor);

                if (!Compare.Contains(CellClicked.contentCell))
                {
                    Compare.Add(CellClicked.contentCell);
                    Compare.Sort();
                    CompareArray = Compare.ToArray();
                    shipSetupManager.Coordinates.Text = string.Empty;
                    for (int count = 0; count < Compare.Count; count++)
                        shipSetupManager.Coordinates.Text += CompareArray[count];
                }
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
    }


}
