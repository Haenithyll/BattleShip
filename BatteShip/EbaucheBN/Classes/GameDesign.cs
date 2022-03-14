using System.Collections.Generic;
using System.Linq;
using Windows.UI;

namespace EbaucheBN.Classes
{
    //This class' purpose is to set the overall settings of the game.
    //It also allows graphic customization.

    #region GameDesignClassDefinition
    internal class GameDesign
    {
        #region GridSizeSettings 
        //Sets GridSize - X lines ; Y columns
        public const int GridSizeX = 13;
        public const int GridSizeY = 13;
        #endregion  
        #region GeneralSettings
        //Sets the amount of hit available every turn, default set to 1
        public const int HitPerTurn = 1;
        #endregion
        #region ShipDefinition
        //Allows Ship creation : name size and amount
        private static Ship Fregate = new Ship("Fregate", 2);
        private static Ship Scout = new Ship("Scout", 3);
        private static Ship Submarine = new Ship("Submarine", 3);
        private static Ship Destroyer = new Ship("Destroyer", 4);
        private static Ship PlaneCarrier = new Ship("AirCraft\nCarrier", 5);

        public static readonly HashSet<Ship> ShipList = new HashSet<Ship>()
        {
            Fregate,
            Scout,
            Submarine,
            Destroyer,
            PlaneCarrier
        };
        public static int ShipCount = ShipList.Count;
        public static readonly Ship[] ShipArray = ShipList.ToArray();
        #endregion
        #region ShipSizeDefinition
        //Sets the amount of total coordinates for ships as well as the size of the largest ship available
        public static int MaxSize = maxSize();
        public static int TotalSize = totalSize();
        public static int maxSize()
        {
            int max = 0;

            for (int count = 0; count < ShipList.Count; count++)
            {
                if (max < ShipArray[count].Size)
                    max = ShipArray[count].Size;
            }

            return max;
        }
        public static int totalSize()
        {
            int sum = 0;

            foreach (Ship ship in ShipList)
                sum += ship.Size;

            return sum;
        }
        #endregion
        #region ColorSettings
        //Sets Colors for graphic items
        public static Windows.UI.Color DefaultColor = Colors.LightGray;
        public static Windows.UI.Color ShipColor = Colors.DarkOrange;
        public static Windows.UI.Color ShipHitColor = Colors.Red;
        public static Windows.UI.Color ShipSunkColor = Colors.DarkRed;
        public static Windows.UI.Color WaterColor = Colors.RoyalBlue;
        public static Windows.UI.Color ShipSelectColor = Colors.MediumPurple;
        public static Windows.UI.Color CellSelectColor = Colors.MediumPurple;
        public static Windows.UI.Color ShipCoordColor = Colors.Orange;
        public static Windows.UI.Color ResetButtonColor = Colors.IndianRed;
        public static Windows.UI.Color StartGameColor = Colors.MediumSlateBlue;
        public static Windows.UI.Color InGameShipColor = Colors.SlateBlue;
        #endregion
    }
    #endregion
}