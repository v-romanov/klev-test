namespace Second;

public static class Counter
{
    private static ReaderWriterLock locker = new ReaderWriterLock();
    private static int counter = 0;


    public static void IncrementCounter()
    {
        locker.AcquireWriterLock(-1);

        try
        {
            Interlocked.Increment(ref counter);
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }

    public static int GetCounter()
    {
        locker.AcquireReaderLock(-1);
        var output = counter;
        locker.ReleaseReaderLock();

        return output;
    }

    public static void Main()
    {
        var threads = new List<Thread>(11);

        var watcher = new Thread(() =>
        {
            var counter = GetCounter();

            while (counter != 10000)
            {
                counter = GetCounter();

                Thread.Sleep(5);
                Console.WriteLine("Counter is " + counter.ToString());
            }
        });
        watcher.Start();

        threads.Add(watcher);

        for (var i = 0; i < 10; ++i)
        {
            var thread =
            new Thread(() =>
            {
                for (var j = 0; j < 1000; ++j)
                {
                    IncrementCounter();
                    Thread.Sleep(1);
                }
            });
            thread.Start();

            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}
