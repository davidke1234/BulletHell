using Matrix.Controllers;
using System;
namespace Matrix.Models
{
    public class Observer : IObserver
    {
        public Observer(ISubject subject)
        {
            subject.RegisterObserver(this);
        }

        public void update(int score)
        {
            PlayerManager.Player.Score.Value += score;
        }
    }
}