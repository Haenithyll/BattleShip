using System;
using System.Collections.Generic;

namespace EbaucheBN.Classes
{
    //This class' purpose is to setup randomly every AI ship while ensuring that ships do not overlap.

    #region AIShipSetupManagerClassDefinition
    internal class AIShipSetupManager
    {
        #region Variables
        private BattleShipGrid AIBSGrid = new BattleShipGrid();     //Definition of a BattleShipGrid to retrieve the cells from the AI BSG.

        private Random Hit = new Random();                          //A random variable is declared.

        private List<Cell> shipCoordonates = new List<Cell>();      //Creation of a list that will store every cell that already host a ship position.
        private List<Cell> newShipCoordonates = new List<Cell>();   //Creation of a list that will store the cells of a new ship that is being setup.

        private List<Ship> AIShipList = new List<Ship>();           //Creation of a list that will store every Ship created and positioned.

        private int PositionX;                                      //This integer is used to store the current starting row index.
        private int PositionY;                                      //This integer is used to store the current starting column index.
        #endregion
        #region Ship Setup
        public List<Ship> SetupAIShips(BattleShipGrid grid)     //Returns the list of all the AI Ship once their setup has been completed. 
        {
            AIBSGrid = grid;                                            //Proceeds to retrieve the AI BattleShipGrid from the input.

            foreach (Ship ship in GameDesign.ShipList)                  //For each ship that has to be setup according to GameDesigners
            {
                Ship newShip = InitializeNewShip(ship);                         //Creation of a new blank ship.
                bool vertical = Hit.Next(2) == 0;                               //Assigment of a random bool to set alignment : 0=vertical et 1=horizontal

                if (vertical)                                                   //If the boat is vertical :
                    SetupNewAIShip(newShip, true);                                      //Proceeds to Setup the new ship vertically.
                else                                                            //else, meaning the boat is horizontal :
                    SetupNewAIShip(newShip, false);                                     //Proceeds to Setup the new ship horizontally.

                AppendNewShip(newShip);                                         //Adds the Ship to the ship list.
            }

            return AIShipList;                                          //Once every ship is setup, returns the Ship list.
        }
        private Ship InitializeNewShip(Ship ship)               //Returns a new Ship for setup. 
        {
            return new Ship(ship.Name, ship.Size);
        }
        private void SetupNewAIShip(Ship ship, bool vertical)   //Proceeds to setup the Ship. 
        {
            do                                                                               //Proceeds to :
            {
                newShipCoordonates.Clear();                                                         //Clear existing coordinates in the new ship list.

                PositionX = Hit.Next(1, GameDesign.GridSizeX - (vertical ? ship.Size : 0));         //Defines a random starting Row position for the new Ship.
                PositionY = Hit.Next(1, GameDesign.GridSizeY - (vertical ? 0 : ship.Size));         //Defines a random starting Column position for the new Ship.

                                                                                                    //Adds the ships coordinates to the new ship list.
                for (int shipCellCount = 0; shipCellCount < ship.Size; shipCellCount++)             //Starts from the startposition and extends for the length of the ship.
                    newShipCoordonates.Add(AIBSGrid.getCell(PositionX + (vertical ? shipCellCount : 0), PositionY + (vertical ? 0 : shipCellCount)));

            } while (!TestValidShipPosition());                                               //As long as the new ship is overlapping with another existing ship.

            shipCoordonates.AddRange(newShipCoordonates);                                     //Adds the new ship coordinates to the list to avoid future ships to overlap.
        }
        private bool TestValidShipPosition()                    //Returns whether or not another ship is overlapping with the new ship. 
        {
            foreach (Cell newShipCell in newShipCoordonates)        //For each cell upon which the new ship is positioned :
            {
                if (shipCoordonates.Contains(newShipCell))                  //If the cell is already in the list of every cells occupied :
                    return false;                                                   //Return false
            }
            return true;                                                    //If no ship is overlapping, return true.
        }
        private void AppendNewShip(Ship ship)                   //Sets up the ship's positions and adds the ship to the AI Ship list. 
        {
            ship.Position.AddRange(newShipCoordonates);
            AIShipList.Add(ship);
        }
        #endregion
    }
    #endregion
}