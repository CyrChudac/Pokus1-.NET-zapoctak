using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
	public static class Time
	{
		static Time() { }
		/// <summary>
		/// Number from 0 to 1 representing how much is time running at the moment.
		/// </summary>
		public static float TimeFlow { get; private set; } = 1;
		private static  long milisOfInactive = 0;
		public static long Now { get; private set; } = 0;
		public static bool IsRunning => TimeFlow > 0; 
		public static long pauseTime = 0;
		public static void Stop()
		{
			pauseTime = Now;
			TimeFlow = 0;

		}
		public static void Start()
		{
			Now = (long)DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
			milisOfInactive += Now - pauseTime;
			TimeFlow = 1;
		}
		public static void Update()
		{
			long now2 = (long)DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
			DeltaTime = now2 - Now;
			Now = now2;
		}
		public static long DeltaTime { get; private set; }
	}
}
