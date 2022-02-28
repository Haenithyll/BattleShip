using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbaucheBN.Classes
{
    public class Ship
    {
        public string Name;
        public int Size;

        public List<Cell> Position = new List<Cell>();

        public Ship(string inputName, int inputSize)
        {
            this.Name = inputName;
            this.Size = inputSize;
        }
    }
}
