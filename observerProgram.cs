using System;
using System.Threading;
using System.Collections.Generic;

namespace observer
{

    // Nayyar / John
    public interface Subscriber
    { 
        void Update(IPublisher subject);
    }

    class ConcreteSubscriberA : Subscriber
    {
        public void Update(IPublisher subject)
        {            
            Console.WriteLine("Mobile Device (display): " + (subject as Publisher).behind +
            "/" + (subject as Publisher).goal +
            "/" + (subject as Publisher).Score);
        }
    }

    class ConcreteSubscriberB : Subscriber
    {
        public void Update(IPublisher subject)
        {
            Console.WriteLine("Laptop Device (display): " + (subject as Publisher).behind +
            "/" + (subject as Publisher).goal +
            "/" + (subject as Publisher).Score);
        }
    }

    public interface IPublisher
    {
        // Attach an observer to the subject.
        void Attach(Subscriber observer);

        // Detach an observer from the subject.
        void Detach(Subscriber observer);

        // Notify all observers about an event.
        void Notify();
    }

    public class Publisher : IPublisher
    {
        public int behind { get; set; } = -0;
        public int goal { get; set; } = -0;
        public int Score { get; set; } = -0;

        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        private List<Subscriber> subscribers = new List<Subscriber>();

        // The subscription management methods.
        public void Attach(Subscriber observer)
        {
            Console.WriteLine("\nSubject: Attached an observer.");
            this.subscribers.Add(observer);
        }

        public void Detach(Subscriber observer)
        {
            this.subscribers.Remove(observer);
            Console.WriteLine("\nSubject: Detached an observer.");
        }

        // Trigger an update in each subscriber.
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var subscriber in subscribers)
            {
                subscriber.Update(this);
            }
        }

        public void behindScore()
        {
            Thread.Sleep(5000);

            this.behind += 1;

            Console.WriteLine("\nSubject: Scored behind: " + this.behind);
            this.Notify();
        }
        public void goalScore()
        {
            Thread.Sleep(5000);

            this.goal += 6;

            Console.WriteLine("\nSubject: Goal Scored: " + this.goal);
            this.Notify();
        }

        public void scoreIncrease(int score)
        {
            Thread.Sleep(1000);

            this.Score += score;

            Console.WriteLine("Subject: Score Changed: " + this.Score);
            this.Notify();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            Console.WriteLine("Game between Collingwood and Carlton has started");
            var publisher = new Publisher();
            Console.WriteLine("Behind/Goals/Total Score");

            // Emily opening watching on her phone
            var subscriberA = new ConcreteSubscriberA();
            publisher.Attach(subscriberA);

            // Keefe watching on his laptop
            var subscriberB = new ConcreteSubscriberB();
            publisher.Attach(subscriberB);

            /* Game has started */
            publisher.behindScore();
            publisher.scoreIncrease(1);

            publisher.behindScore();
            publisher.scoreIncrease(1);

            publisher.goalScore();
            publisher.scoreIncrease(6);
            
            publisher.Detach(subscriberB);

            publisher.behindScore();
            publisher.scoreIncrease(1);

            publisher.goalScore();
            publisher.scoreIncrease(6);          
        }
    }
}
