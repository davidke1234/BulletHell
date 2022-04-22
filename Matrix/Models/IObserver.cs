namespace Matrix.Models
{
    public interface IObserver
    {
        void updateScore(int score);
        void updateHealth(int health);
    }
}
