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
    public class ShipSelection
    {
        public Button ShipButton = new Button();

        public List<Coordinate> Coordinates = new List<Coordinate>();

        public List<Cell> ShipCells = new List<Cell>();

        public int ShipSize;
        public bool Complete = false;
        public ResetButton reset;
        public Action ResetCoordinatesAction;
        public TextBlock ShipName = new TextBlock();

        public Color CurrentColor = GameDesign.DefaultColor;

        public ShipSelection(int ShipCount, Grid UI)
        {
            ResetCoordinatesAction += ResetCoordinates;

            reset = new ResetButton(ShipCount, UI, ResetCoordinatesAction);

            ShipSize = GameDesign.ShipArray[ShipCount].Size;

            ShipName.Text = GameDesign.ShipArray[ShipCount].Name;
            ShipName.FontSize = 20;
            ShipName.Foreground = new SolidColorBrush(Colors.Black);

            ShipButton.Width = UI.Width / (GameDesign.MaxSize + 3);
            ShipButton.Height = UI.Height / (GameDesign.ShipCount + 2); 
            ShipButton.Background = new SolidColorBrush(GameDesign.DefaultColor);
            ShipButton.Content = ShipName;
            ShipButton.Click += OnClick;

            UI.Children.Add(ShipButton);
            Grid.SetRow(ShipButton, ShipCount);
            Grid.SetColumn(ShipButton, 0);

            for(int CoordinatesAmount = 0; CoordinatesAmount < ShipSize; CoordinatesAmount++)
            {
                Coordinate newCoord = new Coordinate("", UI);

                UI.Children.Add(newCoord.Button);

                Grid.SetRow(newCoord.Button, ShipCount);
                Grid.SetColumn(newCoord.Button, CoordinatesAmount+1);
                Coordinates.Add(newCoord);
            }
        }

        void OnClick(object sender, RoutedEventArgs e)
        {
            if(!Complete)
                MainPage.Instance.shipSetupManager.Click(this);
        }

        public void ResetCoordinates()
        {
            ShipName.Foreground = new SolidColorBrush(Colors.Black);
            ShipButton.Background = new SolidColorBrush(GameDesign.DefaultColor);

            foreach (Cell cell in ShipCells)
            {
                Complete = false;

                cell.cellButton.Background = new SolidColorBrush(GameDesign.DefaultColor);
                MainPage.Instance.shipSetupManager.ShipCells.Remove(cell);
            }

            foreach(Coordinate coord in Coordinates)
            {
                coord.TextBlock.Text = string.Empty;
                coord.Button.ClearValue(Button.BackgroundProperty);
            }
            CurrentColor = GameDesign.DefaultColor;

            reset.button.IsEnabled = false;
            reset.button.Background = new SolidColorBrush(GameDesign.DefaultColor);
            reset.textBlock.Foreground = new SolidColorBrush(Colors.Black);

            ShipCells.Clear();
        }
    }
}