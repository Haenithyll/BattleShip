using Windows.UI.Xaml.Controls;

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

        BattleShipGrid newBSgrid = new BattleShipGrid();
        GridManager myGridManager = new GridManager();
        MenuManager myMenuManager = new MenuManager();

        public MainPage()
        {
            Instance = this;

            this.InitializeComponent();
            myMenuManager.Initialize(MenuUI);
        }

        public void GridInitialization()
        {
            newBSgrid.initGrid();

            myGridManager.Setup(myGrid, newBSgrid);
            myGridManager.SetSize(myGrid);
        }
    }

}
