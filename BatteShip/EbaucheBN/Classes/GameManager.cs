using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    public class GameManager
    {
        private AI enemyAI = new AI();
        public bool AItoPlay = new bool();

        private int currentHitCounter = new int();

        public void Initialize()
        {
            AItoPlay = false;
            enemyAI.Initialize();
            UpdateShipCellType();
        }
        public void Hit(Cell cellHit)
        {

            ManagePlayerTurn();

            cellHit.HitYet = true;
            cellHit.cellButton.Background = new SolidColorBrush(cellHit.typeOfCell == cellType.Water ? GameDesign.WaterColor : TestNotSunk(cellHit));

            if (AItoPlay)
                enemyAI.AIHit();
        }
        private Color TestNotSunk(Cell cell)
        {
            if (enemyAI.shipCellHits.Contains(cell))
                return GameDesign.ShipHitColor;
            else
                return GameDesign.ShipSunkColor;
        }
        public bool VerifyShipSunk(bool Ally)
        {
            if (Ally)
            {
                return false;
            }
            else
            {
                int hitCount;

                foreach(Ship ship in MainPage.Instance.allyShips)
                {
                    hitCount = 0;

                    foreach(Cell position in ship.Position)
                    {
                        if (position.HitYet)
                            hitCount++;
                    }

                    if(hitCount == ship.Size)
                    {
                        ShipSunk(ship);
                        return true;
                    }  
                }

                return false;
            }
        }
        private void UpdateShipCellType()
        {
            foreach (Ship ship in MainPage.Instance.allyShips)
            {
                foreach (Cell cell in ship.Position)
                    cell.typeOfCell = cellType.Ship;
            }
        }
        private void ShipSunk(Ship ship)
        {
            foreach (Cell position in ship.Position)
            {
                position.cellButton.Background = new SolidColorBrush(GameDesign.ShipSunkColor);
                enemyAI.shipCellHits.Remove(position);
            }

            enemyAI.UpdatePotentialShipPosition();
            MainPage.Instance.allyShips.Remove(ship);
        }
        private void ManagePlayerTurn()
        {
            currentHitCounter++;
            if (currentHitCounter == GameDesign.HitPerTurn)
            {
                currentHitCounter = 0;
                AItoPlay = AItoPlay ? false : true;
            }
        }
    }
}
