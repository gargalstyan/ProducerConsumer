using ProducerConsumerPattern;
ProducerConsumer producerConsumer = new ProducerConsumer(5, 2,25);
producerConsumer.Run();

//Queue<int> stream = new Queue<int>();
//Random random = new Random();
//Object o = new Object();

// void Produce()
//{
//    int input;

//    while (true)
//    {
//        input = random.Next(300, 1000);
//        Thread.Sleep(input);

//        lock (o)
//        {
//            if (stream.Count > 10)
//                Monitor.PulseAll(o);

//            if (stream.Count > 20)
//            {

//                Monitor.PulseAll(o);
//                Monitor.Wait(o);
//            }

//            stream.Enqueue(input);
//            Console.WriteLine($"PRODUCE - {stream.Count}");
//        }
//    }
//}

// void Consume()
//{
//    while (true)
//    {
//        int input = random.Next(300, 1000);
//        Thread.Sleep(input);

//        lock (o)3
//        {
//            if (stream.Count < 10)
//            {
//                Monitor.PulseAll(o);
//                Monitor.Wait(o);
//            }

//            stream.Dequeue();
//            Console.WriteLine($"CONSUME  - {stream.Count}");
//        }
//    }
//}


//Thread t1 = new Thread(Produce);
//t1.Name = "Producer";

//Thread t2 = new Thread(Consume);
//t2.Name = "Consumer";

//t1.Start();
//t2.Start();

//t1.Join();
//t2.Join();



//Queue<int> buffer = new Queue<int>();
//object o = new object();
//void Producer()
//{
//    Random rnd = new Random();
//    while (true)
//    {
//        lock (o)
//        {
//            int number = rnd.Next(0, 100);
//            buffer.Enqueue(number);
//            Console.WriteLine($"Producer thread {number}");
//            if (buffer.Count > 100)
//            {
//                Monitor.Pulse(o);
//                Monitor.Wait(o);
//            }
//            Thread.Sleep(rnd.Next(0, 500));
//        }

//    }
//}
//void Consumer()
//{
//    Random rnd = new Random();
//    while (true)
//    {
//        lock (o)
//        {
//            if (buffer.Count < 90)
//            {
//                Monitor.Pulse(o);
//            }
//            if (buffer.Count == 0)
//            {
//                Monitor.Pulse(o);
//                Monitor.Wait(o);
//            }

//            int number = buffer.Dequeue();
//            Console.WriteLine($"Consumer Thread {number}");
//            Thread.Sleep(rnd.Next(0, 500));
//        }

//    }
//}
//Thread thread = new Thread(() => Producer());
//Thread thread1 = new Thread(() => Consumer());
//thread.Start();
//thread1.Start();
//thread1.Join();
//thread.Join();

