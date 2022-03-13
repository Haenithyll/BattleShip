using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    internal class AIShipSetupManager
    {
        private Random Hit = new Random();

        private List<Cell> shipCoordonates = new List<Cell>();
        private List<Cell> newShipCoordonates = new List<Cell>();

        private List<Ship> AIShipList = new List<Ship>();

        private int HitX;
        private int HitY;

        private BattleShipGrid AIBSGrid = new BattleShipGrid();

        public List<Ship> SetupAIShips(BattleShipGrid grid)
        {
            AIBSGrid = grid;

            foreach (Ship ship in GameDesign.ShipList)
            {
                Ship newShip = InitializeNewShip(ship);

                bool vertical = Hit.Next(2) == 0;//0=vertical et 1=horizontal

                if (vertical)
                    SetupNewAIShip(newShip, true);
                else
                    SetupNewAIShip(newShip, false);

                AppendNewShip(newShip);
            }

            return AIShipList;
        }
        private Ship InitializeNewShip(Ship ship)
        {
            return new Ship(ship.Name, ship.Size);
        }
        private void AppendNewShip(Ship ship)
        {
            ship.Position.AddRange(newShipCoordonates);
            AIShipList.Add(ship);
        }
        private bool TestValidShipPosition()
        {
            foreach (Cell newShipCell in newShipCoordonates)
            {
                if (shipCoordonates.Contains(newShipCell))
                    return false;
            }
            return true;
        }
        private void SetupNewAIShip(Ship ship, bool vertical)
        {
            do
            {
                newShipCoordonates.Clear();

                HitX = Hit.Next(1, GameDesign.GridSizeX - (vertical ? ship.Size : 0));
                HitY = Hit.Next(1, GameDesign.GridSizeY - (vertical ? 0 : ship.Size));

                for (int shipCellCount = 0; shipCellCount < ship.Size; shipCellCount++)
                    newShipCoordonates.Add(AIBSGrid.getCell(HitX + (vertical ? shipCellCount : 0), HitY + (vertical ? 0 : shipCellCount)));

            } while (!TestValidShipPosition());

            shipCoordonates.AddRange(newShipCoordonates);
        }
    }
}
