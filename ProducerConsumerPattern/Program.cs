

Queue<int> buffer = new Queue<int>();
object o = new object();
void Producer()
{
    Random rnd = new Random();
    while (true)
    {
        lock (o)
        {
            int number = rnd.Next(0, 100);
            buffer.Enqueue(number);
            Console.WriteLine($"Producer thread {number}");
            if (buffer.Count > 100)
            {
                Monitor.Pulse(o);
                Monitor.Wait(o);
            }
            Thread.Sleep(rnd.Next(0, 500));
        }

    }
}
void Consumer()
{
    Random rnd = new Random();
    while (true)
    {
        lock (o)
        {
            if (buffer.Count < 90)
            {
                Monitor.Pulse(o);
            }
            if (buffer.Count == 0)
            {
                Monitor.Pulse(o);
                Monitor.Wait(o);
            }

            int number = buffer.Dequeue();
            Console.WriteLine($"Consumer Thread {number}");
            Thread.Sleep(rnd.Next(0, 500));
        }

    }
}
Thread thread = new Thread(() => Producer());
Thread thread1 = new Thread(() => Consumer());
thread.Start();
thread1.Start();
thread1.Join();
thread.Join();

