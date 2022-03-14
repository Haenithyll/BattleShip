namespace EbaucheBN.Classes
{
    //This class' purpose is the creation of a 2D matrix composed of cells.

    #region BattleShipGridClassDefinition
    public class BattleShipGrid
    {
        #region Variables
        public Cell[,] grid = new Cell[GameDesign.GridSizeX, GameDesign.GridSizeY]; //Definition of a 2 dimensional cell array whose size depends on Game Designers. 
        #endregion
        #region Grid Initialization
        public void initGrid(bool Ally)                     //Proceeds to set up the grid.
        {
            for (int i = 0; i < GameDesign.GridSizeX; i++)                                 //For every Row index :
            {                                                                                                       
                for (int j = 0; j < GameDesign.GridSizeY; j++)                                  //For every Column index :
                {
                    grid[i, j] = new Cell(cellType.Water, Ally,                                       //Creates and appends a new cell at that specific couple of indexes.
                                i, j, Ally ? MainPage.Instance.GetPlayerGrid() 
                                        : MainPage.Instance.GetAIGrid());
                }
            }
        }
        #endregion
        #region Get Cell
        public Cell getCell(int x, int y)       //Returns the cell that is stored on the xth row and yth column.
        {
            return grid[x, y];
        }
        #endregion
    }
    #endregion
}