using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbaucheBN.Classes
{
    internal class AI
    {
        public static void ShootRandom(BattleShipGrid grid)
        {
            Random Hit = new Random();
            int HitX;
            int HitY;
            do
            {
                HitX = Hit.Next(1, GameDesign.GridSizeX);
                HitY = Hit.Next(1, GameDesign.GridSizeY);
            } while (grid.getCell(HitX, HitY).HitYet == true);
            //Console.Out.WriteLine(HitX+""+HitY);
            

        }
    }
}
