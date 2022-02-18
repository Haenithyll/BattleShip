using EbaucheBN.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;

namespace EbaucheBN
{
    internal class MenuManager
    {
        Grid myMenuUI;

        public void Initialize(Grid MenuUI)
        {
            myMenuUI = MenuUI;
            MenuButton PlayGame = new MenuButton(MenuUI, "PLAY", OnClickPlay, 0);
            MenuButton ExitGame = new MenuButton(MenuUI, "EXIT", OnClickExit, 1);
        }

        public void StartGame()
        {
            myMenuUI.Children.Clear();
            MainPage.Instance.GridInitialization(MainPage.Instance.AllyBattleShipGrid, true);
        }

        void OnClickPlay(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        void OnClickExit(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
