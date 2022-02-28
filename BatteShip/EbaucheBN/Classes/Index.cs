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
    internal class Index
    {
        public TextBlock index = new TextBlock();
        public Button button = new Button();

        public Index(string name, Grid UI)
        {
            index.Text = name;

            button.Width = UI.Width * 0.85 / (GameDesign.GridSizeX + 1);
            button.Height = UI.Height * 0.85 / (GameDesign.GridSizeY + 1);
            button.Background = null;
            button.IsEnabled = false;
            button.Content = index;
        }
    }
}
