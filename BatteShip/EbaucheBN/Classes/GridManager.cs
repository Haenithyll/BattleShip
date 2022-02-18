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
    internal class GridManager
    {
        public void Setup(Grid myGrid, BattleShipGrid newBSgrid)
        {
            bool initialized = false;

            for (int i = 0; i <= Constants.GridSizeX; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());

                for (int j = 0; j <= Constants.GridSizeY; j++)
                {
                    if (!initialized)
                    {
                        myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        initialized = j == Constants.GridSizeY ? true : false;
                    }

                    if (j == 0 && i != 0)
                    {
                        Index NewIndex = new Index("\n" + i.ToString(), false);
                        myGrid.Children.Add(NewIndex.index);
                        Grid.SetRow(NewIndex.index, i);
                        Grid.SetColumn(NewIndex.index, j);
                    }
                    else if (i == 0)
                    {
                        if (j != 0)
                        {
                            char currentChar = Convert.ToChar(j + 64);
                            Index NewIndex = new Index("\n" + currentChar.ToString(), true);
                            myGrid.Children.Add(NewIndex.index);
                            Grid.SetRow(NewIndex.index, i);
                            Grid.SetColumn(NewIndex.index, j);
                        }
                        else
                        {
                            Index EmptyIndex = new Index("", false);
                            myGrid.Children.Add(EmptyIndex.index);
                            Grid.SetRow(EmptyIndex.index, i);
                            Grid.SetColumn(EmptyIndex.index, j);
                        }
                    }
                    else
                    {
                        myGrid.Children.Add(newBSgrid.grid[i - 1, j - 1].cellButton);
                        Button CurrentButton = newBSgrid.grid[i - 1, j - 1].cellButton;
                        Grid.SetRow(CurrentButton, i);
                        Grid.SetColumn(CurrentButton, j);
                    }
                }
            }
        }

        public void SetSize(Grid myGrid)
        {
            myGrid.Width = Constants.GridWidth;
            myGrid.Height = Constants.GridHeight;
        }
    }
}