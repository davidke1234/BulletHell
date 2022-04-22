using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public interface GameState
    {
        void GameStarted();
        void GameOver();
        void MainMenuShowing();
        void ConfigMenuShowing();
    }
}
