using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;

namespace EbaucheBN.Classes
{
    //This class' purpose is to manage the menu that is displayed at the very beginning of the game and proceed to start/exit the game depending on user action.

    #region MenuManagerClassDefinition
    internal class MenuManager
    {
        #region Variables
        private Grid myMenuUI; //Definition of a Grid that will display the Menu Buttons.
        #endregion
        #region Initialization
        public void Initialize(Grid MenuUI) //Proceeds to initialize the menu => creates and displays 2 buttons : Play / Exit.
        {
            myMenuUI = MenuUI;                                                          //Retrieves the Grid on which the buttons will be displayed.
            MenuButton PlayGame = new MenuButton(MenuUI, "PLAY", OnClickPlay, 0);       //Creates a new 'play' MenuButton that starts the game upon being pressed.
            MenuButton ExitGame = new MenuButton(MenuUI, "EXIT", OnClickExit, 1);       //Creates a new 'exit' MenuButton that quits the game upon being pressed.
        }
        #endregion
        #region Game Start
        public void StartGame()         //Proceeds to launch the game.
        {
            myMenuUI.Children.Clear();                                                          //Removes every child on the current Grid.
            MainPage.Instance.GridInitialization(MainPage.Instance.AllyBattleShipGrid, true);   //Proceeds to initialize and display the ally grid.
            MainPage.Instance.shipSetupManager.Initialize();                                    //Launches the ship setup process.
        }
        #endregion
        #region Click Events
        void OnClickPlay(object sender, RoutedEventArgs e)  //Starts the game.
        {
            StartGame();
        }
        void OnClickExit(object sender, RoutedEventArgs e)  //Quits the application.
        {
            CoreApplication.Exit();
        }
        #endregion
    }
    #endregion
}