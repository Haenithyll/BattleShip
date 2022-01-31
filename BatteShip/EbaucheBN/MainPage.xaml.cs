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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EbaucheBN
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 


    public enum cellType
    {
        Water,
        Ship
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            BattleShipGrid newBSgrid = new BattleShipGrid();
            newBSgrid.initGrid();

            bool initialized = false;

            Canvas newCanvas = new Canvas();

            Grid myGrid = new Grid();
            myGrid.Width = Constants.GridWidth;
            myGrid.Height = Constants.GridHeight;

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

                    if(j == 0 && i!= 0)
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
                            Index NewIndex = new Index("\n\n" + currentChar.ToString(),true);
                            myGrid.Children.Add(NewIndex.index);
                            Grid.SetRow(NewIndex.index, i);
                            Grid.SetColumn(NewIndex.index, j);
                        }
                        else
                        {
                            Index EmptyIndex = new Index("",false);
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

            newCanvas.Children.Add(myGrid);
            newCanvas.HorizontalAlignment = HorizontalAlignment.Left;
            newCanvas.VerticalAlignment = VerticalAlignment.Top;

            Content = newCanvas;
        }
        static class Constants
        {
            public const int GridSizeX = 5;
            public const int GridSizeY = 5;

            public const int GridHeight = 900;
            public const int GridWidth = 900;
        }

        class BattleShipGrid
        {
            public Cell[,] grid = new Cell[Constants.GridSizeX, Constants.GridSizeY];
            public void initGrid()
            {
                for (int i = 0; i < Constants.GridSizeX; i++)
                {
                    for (int j = 0; j < Constants.GridSizeY; j++)
                    {
                        grid[i, j] = new Cell(cellType.Ship);
                        grid[i, j].initCell(i, j);
                    }
                }
            }

            public Cell getCell(int x, int y)
            {
                return grid[x, y];
            }
        }

        class Cell
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
                cellButton.Width = Constants.GridWidth * 0.85 / (Constants.GridSizeX+1);
                cellButton.Height = Constants.GridHeight * 0.85 / (Constants.GridSizeY+1);
                cellButton.Click += OnClick;
                cellButton.Background = new SolidColorBrush(Colors.LightGray);
            }

            void OnClick(object sender, RoutedEventArgs e)
            {
                this.cellButton.Background = typeOfCell == cellType.Water ? new SolidColorBrush(Colors.SkyBlue) : new SolidColorBrush(Colors.Red);
            }
        }

        class Index
        {
            public TextBlock index = new TextBlock();

            public Index(string name, bool horizontal)
            {
                this.index.Text = name;
                index.HorizontalTextAlignment = horizontal ? TextAlignment.Center : TextAlignment.Right;
                index.Width = Constants.GridWidth * 0.85 / (Constants.GridSizeX+1);
                index.Height = Constants.GridHeight * 0.85 / (Constants.GridSizeY+1);
            }
        }
    }

}
