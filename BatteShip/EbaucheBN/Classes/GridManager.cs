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
        public void Setup(Grid CurrentGrid, BattleShipGrid CurrentBattleShipGrid, bool Ally)
        {
            bool initialized = false;
            Grid UI = Ally ? MainPage.Instance.GetAllyGrid() : MainPage.Instance.GetEnemyGrid();

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
                        Index NewIndex = new Index(i.ToString(), UI);
                        CurrentGrid.Children.Add(NewIndex.button);
                        Grid.SetRow(NewIndex.button, i);
                        Grid.SetColumn(NewIndex.button, j);
                    }
                    else if (i == 0)
                    {
                        if (j != 0)
                        {
                            char currentChar = Convert.ToChar(j + 64);
                            Index NewIndex = new Index(currentChar.ToString(), UI);
                            CurrentGrid.Children.Add(NewIndex.button);
                            Grid.SetRow(NewIndex.button, i);
                            Grid.SetColumn(NewIndex.button, j);
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
    }
}
