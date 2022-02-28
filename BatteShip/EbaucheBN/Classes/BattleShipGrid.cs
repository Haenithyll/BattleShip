using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbaucheBN.Classes
{
    public class BattleShipGrid
    {
        public Cell[,] grid = new Cell[GameDesign.GridSizeX, GameDesign.GridSizeY];
        public void initGrid(bool Ally)
        {
            for (int i = 0; i < GameDesign.GridSizeX; i++)
            {
                for (int j = 0; j < GameDesign.GridSizeY; j++)
                {
                    grid[i, j] = new Cell(cellType.Water, Ally, i, j, Ally ? MainPage.Instance.GetAllyGrid() : MainPage.Instance.GetEnemyGrid());
                }
            }
        }

        public Cell getCell(int x, int y)
        {
            return grid[x, y];
        }

        public void DisableGridButtons()
        {
            foreach(Cell cell in grid)
            {
                cell.cellButton.IsEnabled = false;
            }
        }
    }
}
