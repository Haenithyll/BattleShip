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
    internal class GridManager
    {
        public void Setup(Grid CurrentGrid, BattleShipGrid CurrentBattleShipGrid)
        {
            bool initialized = false;

            for (int i = 0; i <= GameDesign.GridSizeX; i++)
            {
                CurrentGrid.RowDefinitions.Add(new RowDefinition());

                for (int j = 0; j <= GameDesign.GridSizeY; j++)
                {
                    if (!initialized)
                    {
                        CurrentGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        initialized = j == GameDesign.GridSizeY ? true : false;
                    }

                    if (j == 0 && i != 0)
                    {
                        Index NewIndex = new Index("\n" + i.ToString(), false);
                        CurrentGrid.Children.Add(NewIndex.index);
                        Grid.SetRow(NewIndex.index, i);
                        Grid.SetColumn(NewIndex.index, j);
                    }
                    else if (i == 0)
                    {
                        if (j != 0)
                        {
                            char currentChar = Convert.ToChar(j + 64);
                            Index NewIndex = new Index("\n" + currentChar.ToString(), true);
                            CurrentGrid.Children.Add(NewIndex.index);
                            Grid.SetRow(NewIndex.index, i);
                            Grid.SetColumn(NewIndex.index, j);
                        }
                        else
                        {
                            Index EmptyIndex = new Index("", false);
                            CurrentGrid.Children.Add(EmptyIndex.index);
                            Grid.SetRow(EmptyIndex.index, i);
                            Grid.SetColumn(EmptyIndex.index, j);
                        }
                    }
                    else
                    {
                        CurrentGrid.Children.Add(CurrentBattleShipGrid.grid[i - 1, j - 1].cellButton);
                        Button CurrentButton = CurrentBattleShipGrid.grid[i - 1, j - 1].cellButton;
                        Grid.SetRow(CurrentButton, i);
                        Grid.SetColumn(CurrentButton, j);
                    }
                }
            }
        }

        public void SetSize(Grid CurrentGrid)
        {
            CurrentGrid.Width = GameDesign.GridWidth;
            CurrentGrid.Height = GameDesign.GridHeight;
        }
    }
}
