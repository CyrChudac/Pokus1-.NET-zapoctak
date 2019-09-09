using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Pokus1
{
	class GameLoop
	{
		readonly Game Game;
		readonly System.Windows.Forms.Timer Timer;
		ManualResetEvent WaitHandle = new ManualResetEvent(false);
		bool IShouldRun = false;
		public GameLoop(Game game, System.Windows.Forms.Timer timer)
		{
			this.Game = game;
			this.Timer = timer;
		}
		public void Start()
		{
			Timer.Tick += (object sender, EventArgs e) => Run();
			while (true)
			{
				if (!IShouldRun)
					WaitHandle.WaitOne();
				Stop();
				Game.Update();
			}
		}

		void Run()
		{
			IShouldRun = true;
			WaitHandle.Set();
		}
		void Stop()
		{
			IShouldRun = false;
			WaitHandle.Reset();
		}
	}
}
