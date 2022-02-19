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
    public class ShipSetupManager
    {
        TextBlock ShipSelectionText = new TextBlock();
        Grid UI, SelectionUI;

        public void Initialize()
        {
            UI = MainPage.Instance.GetShipSetupUI();
            SelectionUI = MainPage.Instance.GetShipSelectionSetupUI();
            InitializeInstructionText();
            InitializeBoatSelection();
        }

        void InitializeInstructionText()
        {
            ShipSelectionText.Text = "Please Select A Ship To Set Its Coordinates";
            ShipSelectionText.Foreground = new SolidColorBrush(Colors.White);
            ShipSelectionText.FontSize = 32;
            ShipSelectionText.FontFamily = new FontFamily("Times New Roman");
            ShipSelectionText.HorizontalAlignment = HorizontalAlignment.Center;

            UI.Children.Add(ShipSelectionText);
        }

        void InitializeBoatSelection()
        {
            SelectionUI.RowDefinitions.Add(new RowDefinition());

            for (int ShipCount = 0; ShipCount < GameDesign.ShipCount; ShipCount++)
            {
                SelectionUI.ColumnDefinitions.Add(new ColumnDefinition());

                Button ShipSelectionButton = new Button();
                TextBlock ShipSelectionText = new TextBlock();

                ShipSelectionText.Text = GameDesign.ShipArray[ShipCount].Name;
                ShipSelectionText.Foreground = new SolidColorBrush(Colors.Black);

                ShipSelectionButton.Width = UI.Width / (GameDesign.ShipCount+1);
                ShipSelectionButton.Height = 50;
                ShipSelectionButton.Background = new SolidColorBrush(GameDesign.DefaultColor);
                ShipSelectionButton.Content = ShipSelectionText;

                SelectionUI.Children.Add(ShipSelectionButton);
                Grid.SetRow(ShipSelectionButton, 0);
                Grid.SetColumn(ShipSelectionButton, ShipCount);
            }
        }
    }
}
