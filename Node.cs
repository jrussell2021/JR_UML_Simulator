using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Simulator_SDL2
{
    class Node
    {
        public Node(int pX, int pY)
        {
            x = pX; y = pY;
        }

        public int x, y;

        public double distance;
    }
}
