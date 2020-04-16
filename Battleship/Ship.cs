using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Ship
    {
        public string name;
        public List<int[]> placement = new List<int[]>();
        
        public void Shipyard(int [] coordinate)
        {
            placement.Add(coordinate);
        }
    }
}
