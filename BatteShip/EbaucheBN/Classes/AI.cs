using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbaucheBN.Classes
{
    internal class AI
    {
        static BattleShipGrid grid = new BattleShipGrid();

        static bool lastHitSuccessful = false;
        int hitstreak = new int();

        List<Cell> potentialShipPosition = new List<Cell>();

        int CurrentShipCount = GameDesign.ShipCount;
        Cell currentCellHit = new Cell();

        public void Initialize()
        {
            grid = MainPage.Instance.GetAllyBSGrid();
        }
        public void RandomHit()
        {
            int HitX;
            int HitY;

            if (lastHitSuccessful)
            {

            }
            else
            {
                Random Hit = new Random();

                do
                {
                    HitX = Hit.Next(0, GameDesign.GridSizeX);
                    HitY = GetHitY(HitX);
                }
                while (grid.getCell(HitX, HitY).HitYet == true);

                if()

                grid.getCell(HitX, HitY).HitYet = true;
                MainPage.Instance.myGameManager.Hit(grid.getCell(HitX, HitY));
            }

            int GetHitY(int currentHitX)
            {
                Random Hit = new Random();

                if (GameDesign.GridSizeY % 2 == 0)
                    return 2 * Hit.Next(0, (GameDesign.GridSizeX / 2)) + (currentHitX % 2 == 0 ? 0 : 1);
                else
                {
                    if (currentHitX % 2 == 0)
                        return (2 * Hit.Next(0, (GameDesign.GridSizeX + 1) / 2));
                    else
                        return (2 * Hit.Next(0, (GameDesign.GridSizeX - 1) / 2) + 1);
                }
            }

        }
    }
}
