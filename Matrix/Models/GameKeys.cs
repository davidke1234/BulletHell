using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class GameKeys
    {
        public Keys  Up;
        public Keys Down;
        public Keys Left;
        public Keys Right;
        public Keys UpUpperCase;
        public Keys DownUpperCase;
        public Keys LeftUpperCase;
        public Keys RightUpperCase;

        public GameKeys(string keysType)
        {
            if (keysType == "wasd")
            {
                Up = (Keys)119;  //w
                UpUpperCase = (Keys) 87; //W
                Down = (Keys) 115; //s
                DownUpperCase = (Keys) 83; //S
                Left = (Keys) 97;  // a
                LeftUpperCase = (Keys) 65; //A
                Right = (Keys) 100; //d
                RightUpperCase = (Keys) 68; //D
            }
            else
            {
                //arrows
                Up = (Keys) 38;
                UpUpperCase = (Keys) 38;
                Down = (Keys) 40;
                DownUpperCase = (Keys) 40;
                Left = (Keys) 37;
                LeftUpperCase = (Keys) 37;
                Right = (Keys) 39;
                RightUpperCase = (Keys) 39;
            }
        }
    }
}
