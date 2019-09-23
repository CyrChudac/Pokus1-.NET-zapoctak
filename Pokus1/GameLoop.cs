using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CoreLib;

namespace Pokus1
{
	public class GameLoop
	{
		readonly Game game;
		Ticker Timer;
		GameScreenControl screenControl;
		public GameLoop(Game game, GameScreenControl screenControl)
		{
			this.game = game;
			this.screenControl = screenControl;
		}

		protected virtual bool Condition => !game.map.GameEnd;

		public void Start()
		{
			Timer = new IndependentTicker();
			game.FirstRun();
			Timer.AddToTick((object sender, EventArgs e) => Update());
			Timer.Interval = Time.delay;
			Timer.Start();
		}
		void Update()
		{
			Action u = screenControl.Update;
			screenControl.Invoke(u);
			//screenControl.Continue();
			if (Condition)
			{
				DuringUpdate();
				game.Update();
			}
			else
			{
				Timer.Stop();
				//screenControl.end = true;
			}
		}

		protected virtual void DuringUpdate() { }
	}
}

