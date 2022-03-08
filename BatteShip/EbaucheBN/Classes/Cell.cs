using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EbaucheBN.Classes
{
    public enum cellType
    {
        Water,
        Ship
    }
    public class Cell
    {
        public string cellCoordinate;
        public int cellIndex;
        public cellType typeOfCell;
        public Button cellButton = new Button();
        public bool Ally;
        public bool HitYet;

        public int X;
        public int Y;

        public Cell(cellType SetCellType, bool SetAlly, int x, int y, Grid UI)
        {
            typeOfCell = SetCellType;

            Ally = SetAlly;
            HitYet = false;
            X = x;
            Y = y;

            cellCoordinate = Convert.ToChar(y + 65) + Convert.ToString(x + 1);
            cellIndex = x * GameDesign.GridSizeY + y + 1;

            cellButton.Width = UI.Width * 0.85 / (GameDesign.GridSizeX + 1);
            cellButton.Height = UI.Height * 0.85 / (GameDesign.GridSizeY + 1);
            cellButton.Background = new SolidColorBrush(GameDesign.DefaultColor);

            cellButton.Click += OnClick;
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.Click(this);
        }
    }
}
