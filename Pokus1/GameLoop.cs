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
		readonly Ticker Timer = new IndependentTicker();
		GameScreenControl screenControl;
		public GameLoop(Game game, GameScreenControl screenControl)
		{
			this.game = game;
			this.screenControl = screenControl;
		}

		protected virtual bool Condition => !game.map.GameEnd;

		public void Start()
		{
			game.FirstRun();
			Timer.AddToTick((object sender, EventArgs e) => Update());
			Timer.Interval = Time.delay;
			Timer.Start();
		}
		delegate void Updating();
		void Update()
		{
			Updating u = screenControl.Update;
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

	public interface Ticker
	{
		void AddToTick(Action<object, EventArgs> action);
		void Start();
		void Stop();
		double Interval { get; set; }
	}

	class FormTicker :  Ticker
	{
		System.Windows.Forms.Timer t;
		public FormTicker(System.Windows.Forms.Timer t)
		{
			this.t = t;
		}
		public void AddToTick(Action<object, EventArgs> action) => t.Tick += new EventHandler(action);
		public void Start() => t.Start();
		public void Stop() => t.Stop();
		public double Interval
		{
			get => t.Interval;
			set => t.Interval = (int)value;
		}
	}

	class IndependentTicker : Ticker
	{
		System.Timers.Timer Timer = new System.Timers.Timer();
		public void AddToTick(Action<object, EventArgs> action)
		{
			Timer.Elapsed += new System.Timers.ElapsedEventHandler(
				(object o, System.Timers.ElapsedEventArgs e) => action(o, e));
		}
		public double Interval
		{
			get => Timer.Interval;
			set => Timer.Interval = value;
		}

		public void Start() => Timer.Start();
		public void Stop() => Timer.Stop();
	}
}
