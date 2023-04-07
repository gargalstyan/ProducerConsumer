using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerPattern
{
    internal class ProducerConsumer
    {
        Queue<int> buffer = new Queue<int>();
        List<Thread> producerThreads = new List<Thread>();
        List<Thread> consumerThreads = new List<Thread>();
        private readonly object o = new object();
        public int ProducerCount { get; }
        public int ConsumerCount { get; }
        public ProducerConsumer(int producerCount, int consumerCount)
        {
            ProducerCount = producerCount;
            ConsumerCount = consumerCount;
        }
        public void Run()
        {
            CreateThreads();
            foreach (var item in producerThreads)
            {
                item.Start();
            }
            foreach (var item in consumerThreads)
            {
                item.Start();
            }
        }
        public void CreateThreads()
        {
            for (int i = 0; i < ProducerCount; i++)
            {
                int index = i;
                producerThreads.Add(new Thread(() => Producer()));
                producerThreads[i].Name = $"ThreadProducer-{i}";
            }
            for (int i = 0; i < ConsumerCount; i++)
            {
                consumerThreads.Add(new Thread(() => Consumer()));
                consumerThreads[i].Name = $"ThreadConsumer-{i}";
            }
        }
        public void Producer()
        {
            Random rnd = new Random();
            while (true)
            {
                lock (o)
                {
                    int number = rnd.Next(0, 100);
                    buffer.Enqueue(number);
                    Console.WriteLine($"{Thread.CurrentThread.Name} {number}");
                    if (buffer.Count >= 50)
                    {
                        Console.WriteLine("Queue is full!!!!!");
                        Monitor.PulseAll(o);
                        Monitor.Wait(o);
                    }
                  
                    Thread.Sleep(rnd.Next(0, 1000));
                }
            }
        }
        public void Consumer()
        {
            Random rnd = new Random();
            while (true)
            {
                lock (o)
                {
                    if (buffer.Count < 40)
                    {
                        Monitor.PulseAll(o);
                    }
                    if (buffer.Count == 0)
                    {
                        Monitor.PulseAll(o);
                        Monitor.Wait(o);
                    }
                    if (buffer.Count >= 50)
                    {
                        while (buffer.Count >= 40)
                        {
                            int number = buffer.Dequeue();
                            Console.WriteLine($"{Thread.CurrentThread.Name} {number}");
                        }
                    }
                    else
                    {
                        int number = buffer.Dequeue();
                        Console.WriteLine($"{Thread.CurrentThread.Name} {number}");
                    }
                    Thread.Sleep(rnd.Next(0, 1000));
                }

            }
        }
    }
}
