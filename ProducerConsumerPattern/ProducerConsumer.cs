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
        public int Size { get; set; }
        public ProducerConsumer(int producerCount, int consumerCount, int size)
        {
            ProducerCount = producerCount;
            ConsumerCount = consumerCount;
            Size = size;
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
            foreach (var item in producerThreads)
            {
                item.Join();
            }
            foreach (var item in consumerThreads)
            {
                item.Join();
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
                    if (buffer.Count == Size)
                    {
                        Console.WriteLine("Full");
                        for (int i = 0; i < producerThreads.Count; i++)
                        {
                            Thread thread = Thread.CurrentThread;
                            Monitor.Pulse(o);
                            Monitor.Wait(o);

                        }
                    }
                    else
                    {
                        if (buffer.Count == 0)
                        {
                           
                            Monitor.Pulse(o);
                        }
                        buffer.Enqueue(rnd.Next(0, 500));
                        Console.WriteLine($"{Thread.CurrentThread.Name} {buffer.Count}");

                    }

                }
                Thread.Sleep(rnd.Next(1000, 1500));
            }

        }
        public void Consumer()
        {
            Random rnd = new Random();
            while (true)
            {

                lock (o)
                {
                    if (buffer.Count == 0)
                    {
                        Monitor.PulseAll(o);
                        Monitor.Wait(o);
                    }

                    if (buffer.Count > 0)
                    {
                        buffer.Dequeue();
                        Console.WriteLine($"{Thread.CurrentThread.Name} {buffer.Count}");
                    }

                }
                Thread.Sleep(rnd.Next(1000, 1500));

            }
        }
        public void WaitAll(List<Thread> threads)
        {
            for (int i = 0; i < threads.Count; i++)
            {
                Monitor.Wait(o);
            }
        }
    }
}