using System;
using System.Threading;
using System.Threading.Tasks;

internal class Program
	{
		static void Main(string[] args)
		{
			// TestTaskWhenAll();
			TestTaskWhenAny();
			Console.ReadLine();
		}

		public static int TestTaskMethod(int second)
		{
			Thread.Sleep(TimeSpan.FromSeconds(second));
			return second;
		}

		public static void TestTaskWhenAll()
		{
			Task<int> task1 = new Task<int>(() => TestTaskMethod(1));
			Task<int> task2 = new Task<int>(() => TestTaskMethod(2));
			task1.Start();
			task2.Start();
			Console.WriteLine("1");

			Task.WhenAll(task1, task2).ContinueWith((t) =>
			{
				Console.WriteLine("2");

				if (t.Status == TaskStatus.RanToCompletion)
				{
					foreach(var result in t.Result)
					{
						Console.WriteLine($"result: {result}");
					}
				}

				if (t.Status == TaskStatus.Faulted)
				{

				}
			});

			Console.WriteLine("3");
		}

		public static void TestTaskWhenAny()
		{
			Task<int> task1 = new Task<int>(() => TestTaskMethod(1));
			Task<int> task2 = new Task<int>(() => TestTaskMethod(2));
			task1.Start();
			task2.Start();
			Task.WhenAny(task1, task2).ContinueWith((t) =>
			{
				Console.WriteLine($"completed: {t.Result.Result}");
			});
		}
	}