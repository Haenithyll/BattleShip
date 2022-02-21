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
        public string contentCell;
        cellType typeOfCell;
        public Button cellButton = new Button();
        bool Ally;
        public bool HitYet;

        public void initCell(int x, int y)
        {
            this.contentCell = Convert.ToChar(x + 64) + Convert.ToString(y);
        }

        public Cell(cellType SetCellType, bool SetAlly, int x, int y)
        {
            typeOfCell = SetCellType;

            Ally = SetAlly;
            HitYet = false;

            contentCell = Convert.ToChar(y + 65) + Convert.ToString(x + 1);

            cellButton.Width = GameDesign.GridWidth * 0.85 / (GameDesign.GridSizeX + 1);
            cellButton.Height = GameDesign.GridHeight * 0.85 / (GameDesign.GridSizeY + 1);
            cellButton.Background = new SolidColorBrush(GameDesign.DefaultColor);

            cellButton.Click += OnClick;
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.Click(this);
        }
    }
}
