using System;
using System.Collections.Generic;

namespace EbaucheBN.Classes
{
    //This class' purpose is the definition of an AI that will both proceed to randomly setup ships on its grid and hit the player's grid following a decision tree.

    #region AIClassDefinition
    internal class AI
    {
        #region Variables
        private AIShipSetupManager myAIshipSetupManager = new AIShipSetupManager(); //Definition of an SetupManager to setup AI's ships randomly.

        private BattleShipGrid PlayerGrid = new BattleShipGrid();       //Definition of a BattleShipGrid to retrieve and manage cells from the player's BSG. 
        private BattleShipGrid AIGrid = new BattleShipGrid();           //Definition of another BattleShipGrid to retrieve and manage cells from the AI's BSG. 

        public List<Cell> potentialShipPosition = new List<Cell>();     //Creation of a list used for keeping track of cells surrounding successful hits.
        public List<Ship> AIShips = new List<Ship>();                   //Creation of a list that will contain all AI's Ships once their positioning is complete.

        private int HitX = new int();                                   //This integer is used to store the current hit's row index.
        private int HitY = new int();                                   //This integer is used to store the current hit's column index.
        private int ShipOffSet = new int();                             //This integer is used to retrieve the ship's direction (horizontal / vertical).
        private int TestOffSet = new int();                             //This integer is used to test whether a targeted cell is aligned with former successful hits.
        #endregion 
        #region Initialization
        public void Initialize() //Proceeds to the initialization of the AI.
        {
            PlayerGrid = MainPage.Instance.GetAllyBSGrid();                 //Proceeds to retrieve the player's BattleShipGrid from the MainPage instance.
            AIGrid = MainPage.Instance.GetEnemyBSGrid();                    //Proceeds to retrieve the AI's BattleShipGrid from the MainPage instance.
            AIShips.AddRange(myAIshipSetupManager.SetupAIShips(AIGrid));    //Retrieves the AI Ships once they have been setup thanks to the SetupManager.

            //Retrieving the BattleShipGrids grants cells' access for modification upon being hit or hosting a ship.
            //Retrieving the AI's Ships allows the GameManager to keep track whether one of the ship is hit/sunk.
        }
        #endregion
        #region Hit Decision Tree
        public void AIHit(List<Cell> PlayerShipCellHits)         //Applies the decision tree for AI's strikes. 
                                                                 //It requires the list of previous successful hits to operate.
        {
            Cell currentCellHit;                                        //An empty cell is defined to store the cell that is targeted to be hit.

            if (PlayerShipCellHits.Count == 0)                          //If there is no previous successful hits, it strikes randomly following a pattern.
                currentCellHit = RandomHit();
            else if (PlayerShipCellHits.Count == 1)                     //If there is 1 previous successful hit, it strikes one of the cells around.
                currentCellHit = HitPotentialPosition();
            else                                                        //If there has already been 2 or more successful hits it attempts to finish off the ship hit.
                currentCellHit = FinishShip(PlayerShipCellHits);

            MainPage.Instance.myGameManager.Hit(currentCellHit, true); //Once the cell to hit is assigned, proceeds to hit the targeted cell.
        }
        private Cell RandomHit()                                 //Returns a random cell that has not been hit yet following a pattern to maximize efficiency.
        {
            Random Hit = new Random();                                  //A random variable is declared.

            do                                                          //Proceeds to :
            {
                HitX = Hit.Next(0, GameDesign.GridSizeX);                   //Selects a random row to strike.
                HitY = GetHitY(HitX);                                       //Retrieves a random column to strike while following a sweeping pattern.
            }
            while (PlayerGrid.getCell(HitX, HitY).HitYet == true);          //As long as the cell that is targeted has been hit yet.
      
            return (PlayerGrid.getCell(HitX, HitY));                    //Returns the targeted cell.
        }
        private Cell HitPotentialPosition()                      //Returns a cell at a potential ship position around a previous successful hit. 
        {
            Cell cellToReturn = potentialShipPosition[0];           //Returns the first cell on the list of potential positions.
            potentialShipPosition.RemoveAt(0);                      //Removes this cell from the list to avoid selecting it again.
            return cellToReturn;                                    //Returns the cell.
        }                       
        private Cell FinishShip(List<Cell> PlayerShipCellHits)   //Returns a cell that is aligned with previous successful hits.
                                                                 //Important note : Since ships can be positioned next to each other, in the event of 2 (or more)
                                                                                  //ships being in this situation, the AI will attempt to finish off what it supposes
                                                                                  //to be a ship until all cells matching the criterias have been hit.
        {
            ShipOffSet = GetOffSet(PlayerShipCellHits);                                     //Retrieves the ship's supposed alignment.

            foreach (Cell potentialCell in potentialShipPosition)                           //For each cell that might be a ship.
            {
                foreach (Cell HitCell in PlayerShipCellHits)                                //For each previous successful hits
                {
                    TestOffSet = Math.Abs(HitCell.cellIndex - potentialCell.cellIndex);     //Computes the alignment of a potential hit with respect to a successful hit. 
                    
                    if (TestOffSet == ShipOffSet)                                           //If the potential cell is aligned with the ship :
                    {
                        potentialShipPosition.Remove(potentialCell);                                //Removes this cell from the potential cells to hit
                        return potentialCell;                                                       //Returns it as the targeted cell to strike.
                    }
                }
            }

            return HitPotentialPosition();                      //In case there is no cell to strike, it means 2 (or more) ships are next to each other.
                                                                //Therefore, proceeds to hit a potential ship position around previous successful hits.
        }
        private int GetHitY(int currentHitX)                     //Returns a random row for the random hit following a sweeping pattern. 
        {
            Random Hit = new Random();                                                                  //Defines a new random variable.
                                                                                                      
            if (GameDesign.GridSizeY % 2 == 0)                                                          //If the amount of columns is even :
                return 2 * Hit.Next(0, (GameDesign.GridSizeX / 2)) + (currentHitX % 2 == 0 ? 0 : 1);        //Strikes oddly/evenly depending on the row targeted.
            else
            {                                                                                           //Else, meaning the amount of column is odd :
                if (currentHitX % 2 == 0)                                                                   //Strikes oddly/evenly depending on the row targeted.
                    return (2 * Hit.Next(0, (GameDesign.GridSizeX + 1) / 2));
                else
                    return (2 * Hit.Next(0, (GameDesign.GridSizeX - 1) / 2) + 1);
            }
        }
        private int GetOffSet(List<Cell> PlayerShipCellHits)     //Returns the ship's alignment according to previous successful hits.
        {
            return Math.Abs(PlayerShipCellHits[PlayerShipCellHits.Count - 2].cellIndex - PlayerShipCellHits[PlayerShipCellHits.Count - 1].cellIndex);
        }  
        #endregion
        #region Retrieval of Priority Cells to target
        public void UpdatePotentialShipPosition(List<Cell> PlayerShipCellHits)  //Updates the potential ship cells to strike whenever a ship is sunk. 
                                                                                //Important note : When this function is called, the list that keeps track of previous
                                                                                //successful hit has already been updated as to only contain cells of 
                                                                                //ship that are not sunk yet. 
        {
            potentialShipPosition.Clear();                      //Clears the potential ship cells to remove unnecessary cells to hit.

            foreach (Cell cell in PlayerShipCellHits)           //For each successful hit 
                AddPotentialShipPosition(cell);                     //Adds cells surrounding its position to be targeted next.
        }
        public void AddPotentialShipPosition(Cell cell)                         //Adds potential ship cells to strike whenever a ship is hit. 
        {                                                                       //Note : Since cells can be on the edge of the grid, it is necessary to make 4 successive
                                                                                //tests to avoid attempting to add cells out of the grid's boundaries.
            #region Left End Test
            if ((cell.cellIndex - 1) % GameDesign.GridSizeY > 0)                            //If the successful hit is not located on the left end of the grid :              
            {                                                                   
                if (!PlayerGrid.getCell(cell.X, cell.Y - 1).HitYet)                             //If the cell on the left has not been hit yet :
                    potentialShipPosition.Add(PlayerGrid.getCell(cell.X, cell.Y - 1));                  //Adds the left cell to the potential ship cells to strike.
            }
            #endregion
            #region Right End Test
            if (cell.cellIndex % GameDesign.GridSizeY > 0)                                  //If the successful hit is not located on the right end of the grid :
            {
                if (!PlayerGrid.getCell(cell.X, cell.Y + 1).HitYet)                             //If the cell on the right has not been hit yet :
                    potentialShipPosition.Add(PlayerGrid.getCell(cell.X, cell.Y + 1));                  //Adds the right cell to the potential ship cells to strike.
            }
            #endregion
            #region Top End Test
            if (cell.cellIndex > GameDesign.GridSizeX)                                      //If the successful hit is not located on the top end of the grid :
            {
                if (!PlayerGrid.getCell(cell.X - 1, cell.Y).HitYet)                             //If the cell on top has not been hit yet :
                    potentialShipPosition.Add(PlayerGrid.getCell(cell.X - 1, cell.Y));                  //Adds the top cell to the potential ship cells to strike.
            }
            #endregion
            #region Bottom End Test
            if (cell.cellIndex <= GameDesign.GridSizeX * (GameDesign.GridSizeY - 1))        //If the successful hit is not located on the bottom end of the grid :
            {
                if (!PlayerGrid.getCell(cell.X + 1, cell.Y).HitYet)                             //If the cell below has not been hit yet :
                    potentialShipPosition.Add(PlayerGrid.getCell(cell.X + 1, cell.Y));                  //Adds the cell below to the potential ship cells to strike.
            }
            #endregion
        }
        #endregion
    }
    #endregion
}