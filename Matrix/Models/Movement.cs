using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Sprites
{
    public class Movement
    {
        public float X { get; internal set; }
        public float Y { get; internal set; }
        public float Seconds { get; internal set; }

        public float StartX { get; internal set; }
        public float StartY { get; internal set; }
    }
}
