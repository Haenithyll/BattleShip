﻿using System;
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

namespace EbaucheBN
{
    public enum cellType
    {
        Water,
        Ship
    }

    internal class Cell
    {
        string contentCell { get; set; }
        cellType typeOfCell;
        public Button cellButton = new Button();

        public void initCell(int x, int y)
        {
            this.contentCell = Convert.ToChar(x + 64) + Convert.ToString(y);
        }

        public Cell(cellType setCellType)
        {
            this.typeOfCell = setCellType;
            cellButton.Width = Constants.GridWidth * 0.85 / (Constants.GridSizeX + 1);
            cellButton.Height = Constants.GridHeight * 0.85 / (Constants.GridSizeY + 1);
            cellButton.Click += OnClick;
            cellButton.Background = new SolidColorBrush(Colors.LightGray);
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            this.cellButton.Background = typeOfCell == cellType.Water ? new SolidColorBrush(Colors.SkyBlue) : new SolidColorBrush(Colors.Red);
        }
    }
}