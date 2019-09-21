using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
	public static class Time
	{
		/// <summary>
		/// Number from 0 to 1 representing how much is time running at the moment.
		/// </summary>
		public static float TimeFlow { get; private set; } = 1;
		public static readonly int delay = 20;
		public static long Now { get; private set; } = 0;
		public static bool IsRunning => TimeFlow > 0;

		private static long milisOfInactive = 0;
		private static long _now => (long)DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
		private static long pauseTime = 0;

		public static void Stop()
		{
			pauseTime = _now;
			TimeFlow = 0;
		}
		public static void Start(int timeFlow = 1)
		{
			Now = _now;
			milisOfInactive += Now - pauseTime;
			TimeFlow = timeFlow;
		}
		public static void Update()
		{
			long now2 = _now;
			DeltaTime = now2 - Now;
			Now = now2;
		}
		public static long DeltaTime { get; private set; }
	}
}
