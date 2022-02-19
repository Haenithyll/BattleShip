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

        public Index(string name, bool horizontal)
        {
            this.index.Text = name;
            index.HorizontalTextAlignment = horizontal ? TextAlignment.Center : TextAlignment.Right;
            index.Width = GameDesign.GridWidth * 0.85 / (GameDesign.GridSizeX + 1);
            index.Height = GameDesign.GridHeight * 0.85 / (GameDesign.GridSizeY + 1);
        }
    }
}
