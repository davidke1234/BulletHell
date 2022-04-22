using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Subject : ISubject
    {
        private static List<IObserver> observers = new List<IObserver>();
        public string NameOfSubject;

        public Subject(string nameOfSubject)
        {
            NameOfSubject = nameOfSubject;
        }

        private string SubjectName { get; set; }
        private int ScoreToAdd { get; set; }
        private int HealthToAdjust { get; set; }

        public void SetScore(int score)
        {
            this.ScoreToAdd = score;
            NotifyObservers();
        }

        public void SetHealth(int health)
        {
            this.HealthToAdjust = health;
            NotifyObservers();
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void AddObservers(IObserver observer)
        {
            observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                if (observer.Type == "PlayerScore" && this.ScoreToAdd != 0)
                {
                    observer.updateScore(this.ScoreToAdd);
                    this.ScoreToAdd = 0;
                }
                else if (observer.Type == "PlayerHealth" && this.HealthToAdjust != 0)
                {
                    observer.updateHealth(this.HealthToAdjust);
                    this.HealthToAdjust = 0;
                }
            }
        }
    }
}