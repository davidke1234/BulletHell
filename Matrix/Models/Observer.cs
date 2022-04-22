using Matrix.Controllers;
using System;
namespace Matrix.Models
{
    public class Observer : IObserver
    {
        public string Type;

        public Observer(Subject subject)
        {
            Type = subject.NameOfSubject;
            subject.RegisterObserver(this);
        }

        public void updateHealth(int health)
        {
            PlayerManager.Player.Health += health;
        }

        public void updateScore(int score)
        {
            PlayerManager.Player.Score.Value += score;
        }
    }
}