using System;
using System.Collections.Generic;

namespace EbaucheBN.Classes
{
    internal class AI
    {
        static BattleShipGrid grid = new BattleShipGrid();

        static bool lastHitSuccessful = false;

        public List<Cell> potentialShipPosition = new List<Cell>();
        public List<Cell> shipCellHits = new List<Cell>();

        private int HitX = new int();
        private int HitY = new int();
        private int ShipOffSet = new int();
        private int TestOffSet = new int();
        private int CurrentShipCount = GameDesign.ShipCount;

        public void Initialize()
        {
            grid = MainPage.Instance.GetAllyBSGrid();
        }
        public void AIHit()
        {
            Cell currentCellHit = null;

            if (shipCellHits.Count > 2)
                currentCellHit = FinishShip();
            else if (shipCellHits.Count > 0)
                currentCellHit = HitPotentialPosition();
            else
                currentCellHit = RandomHit();

            currentCellHit.HitYet = true;

            if (currentCellHit.typeOfCell == cellType.Ship)
            {
                shipCellHits.Add(currentCellHit);

                if (!MainPage.Instance.myGameManager.VerifyShipSunk(false))
                    AddPotentialShipPosition(currentCellHit);
            }

            MainPage.Instance.myGameManager.Hit(currentCellHit);
        }
        private Cell HitPotentialPosition()
        {
            Cell cellToReturn = potentialShipPosition[0];
            potentialShipPosition.RemoveAt(0);
            return cellToReturn;
        }
        private Cell FinishShip()
        {
            ShipOffSet = GetOffSet();

            foreach (Cell potentialCell in potentialShipPosition)
            {
                foreach (Cell HitCell in shipCellHits)
                {
                    TestOffSet = Math.Abs(HitCell.cellIndex - potentialCell.cellIndex);
                    if (TestOffSet == ShipOffSet)
                    {
                        potentialShipPosition.Remove(potentialCell);
                        return potentialCell;
                    }
                }
            }

            return HitPotentialPosition();
        }
        public Cell RandomHit()
        {
            Random Hit = new Random();

            do
            {
                HitX = Hit.Next(0, GameDesign.GridSizeX);
                HitY = GetHitY(HitX);
            }
            while (grid.getCell(HitX, HitY).HitYet == true);

            return(grid.getCell(HitX, HitY));
        }
        private int GetHitY(int currentHitX)
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
        public void UpdatePotentialShipPosition()
        {
            potentialShipPosition.Clear();

            foreach (Cell cell in shipCellHits)
            {
                AddPotentialShipPosition(cell);
            }
        }
        private void AddPotentialShipPosition(Cell cell)
        {
            if ((cell.cellIndex - 1) % GameDesign.GridSizeX > 0)
            {
                if (!grid.getCell(cell.X, cell.Y - 1).HitYet)
                    potentialShipPosition.Add(grid.getCell(cell.X, cell.Y - 1));
            }

            if (cell.cellIndex % GameDesign.GridSizeX > 0)
            {
                if (!grid.getCell(cell.X, cell.Y + 1).HitYet)
                    potentialShipPosition.Add(grid.getCell(cell.X, cell.Y + 1));
            }

            if (cell.cellIndex > GameDesign.GridSizeX)
            {
                if (!grid.getCell(cell.X - 1, cell.Y).HitYet)
                    potentialShipPosition.Add(grid.getCell(cell.X - 1, cell.Y));
            }

            if (cell.cellIndex <= GameDesign.GridSizeX * (GameDesign.GridSizeY - 1))
            {
                if (!grid.getCell(cell.X + 1, cell.Y).HitYet)
                    potentialShipPosition.Add(grid.getCell(cell.X + 1, cell.Y));
            }
        }
        private int GetOffSet()
        {
            return Math.Abs(shipCellHits[shipCellHits.Count - 2].cellIndex - shipCellHits[shipCellHits.Count - 1].cellIndex);
        }
    }
}
