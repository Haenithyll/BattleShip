using System.Collections.Generic;

namespace EbaucheBN.Classes
{
    //This class' purpose is to define an element ship that corresponds to battleship's boats.

    #region ShipClassDefinition
    public class Ship
    {
        #region Variables
        public string Name;     //Corresponds to the Ship's name. This will be useful when proceeding to the ships' setup.
        public int Size;        //Corresponds to the Ship's size, meaning how many cells will be necessary for its positioning.

        public List<Cell> Position = new List<Cell>(); //Stores the cells upon which the ship is positioned.
        #endregion
        #region Constructor
        public Ship(string inputName, int inputSize) 
        {
            this.Name = inputName; //Sets the ship's name according to the input sent to the constructor.
            this.Size = inputSize; //Sets the ship's size according to the input sent to the constructor.
        }
        #endregion
    }
    #endregion
}