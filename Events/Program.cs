﻿namespace Events
{
    public class Publisher
    {
        // delegate for event
        public delegate void MyEvtDelegate(object? sender, EventArgs e);

        // event itself
        public event MyEvtDelegate? OnSayHaHaHa;

        public void SayHaHaHa()
        {
            Console.WriteLine("Saying ha-ha-ha");

            if (OnSayHaHaHa != null)
            {
                OnSayHaHaHa(this, EventArgs.Empty);
            }
        }
    }

    public class Subscriber
    {
        public Subscriber(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
        
        public void Subscribe(Publisher publisher)
        {
            publisher.OnSayHaHaHa += (object? sender, EventArgs e) => Console.WriteLine($"{Name} subscribed to an event of {sender?.GetType()?.Name} and the event was called");
        }

        public void SubscribeForDelegate(DelegatePublisher publisher)
        {
            publisher.OnSayHaHaHa += (object? sender, EventArgs e) => Console.WriteLine($"{Name} subscribed to a delegate of {sender?.GetType()?.Name} and the delegate was called");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            var publisher = new Publisher();

            var delegatePublisher = new DelegatePublisher();

            var subsriber_1 = new Subscriber("First subscriber");
            subsriber_1.Subscribe(publisher);
            subsriber_1.SubscribeForDelegate(delegatePublisher);

            var subsriber_2 = new Subscriber("Second subscriber");
            subsriber_2.Subscribe(publisher);
            subsriber_2.SubscribeForDelegate(delegatePublisher);

            publisher.SayHaHaHa();
            Console.WriteLine();

            delegatePublisher.SayHaHaHa();
            Console.WriteLine();
            Console.ReadLine();

            // 1. can not sign to delegate directly
            // 2. can not invoke delegate directly

            // set null to delegate from subsribtion
            // delegatePublisher.OnSayHaHaHa = null;
            delegatePublisher.SayHaHaHa();
            Console.WriteLine();
            Console.ReadLine();
        }
    }

    public class DelegatePublisher
    {
        public delegate void MyEvtDelegate(object? sender, EventArgs e);

        public MyEvtDelegate? OnSayHaHaHa;

        public void SayHaHaHa()
        {
            Console.WriteLine("Saying ha-ha-ha");
    
            if (OnSayHaHaHa != null)
            {
                OnSayHaHaHa(this, EventArgs.Empty);
            }
        }
    }
}