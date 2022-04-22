using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Subject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string NameOfSubject;

        public Subject(string nameOfSubject)
        {
            NameOfSubject = nameOfSubject;
        }

        private string SubjectName { get; set; }
        private int ScoreToAdd { get; set; }

        public void SetScore(int score)
        {
            this.ScoreToAdd = score;
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
            foreach (IObserver observer in observers)
            {
                observer.update(this.ScoreToAdd);
            }
        }
    }
}