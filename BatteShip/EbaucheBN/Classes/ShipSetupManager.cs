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
        HashSet<ShipSelectionButton> buttons = new HashSet<ShipSelectionButton>();

        public TextBlock Coordinates = new TextBlock();
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
            SelectionUI.RowDefinitions.Add(new RowDefinition());

            for (int ShipCount = 0; ShipCount < GameDesign.ShipCount; ShipCount++)
            {
                SelectionUI.ColumnDefinitions.Add(new ColumnDefinition());

                ShipSelectionButton newShipSelectionButton = new ShipSelectionButton(ShipCount, UI, SelectionUI);
                buttons.Add(newShipSelectionButton);
            }
        }

        public void Click(ShipSelectionButton ButtonClicked)
        {
            UpdateButtonColor(ButtonClicked);
            StartCoordinateSelection(ButtonClicked);  
        }

        public void UpdateButtonColor(ShipSelectionButton ButtonClicked)
        {
            foreach (ShipSelectionButton Button in buttons)
            {
                if (Button != ButtonClicked)
                    Button.Button.Background = new SolidColorBrush(GameDesign.DefaultColor);
                else
                    Button.Button.Background = new SolidColorBrush(GameDesign.ShipSelectColor);
            }
        }

        public void StartCoordinateSelection(ShipSelectionButton ButtonClicked)
        {
            MainPage.Instance.Selection = true;
            Coordinates = ButtonClicked.Coordinates;
        }
    }
}
