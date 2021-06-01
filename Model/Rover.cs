using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Model
{
    class Rover
    {
        public int Number { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public char Facing { get; set; }
        public string CurrentInstructions { get; set; }
    }
}