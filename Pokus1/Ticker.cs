using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus1
{
	public abstract class Ticker
	{
		public void AddToTick(Action action) => AddToTick((object o, EventArgs e) => action());
		public abstract void AddToTick(Action<object, EventArgs> action);
		public abstract void Start();
		public abstract void Stop();
		public abstract double Interval { get; set; }
	}

	class FormTicker : Ticker
	{
		System.Windows.Forms.Timer t;
		public FormTicker(System.Windows.Forms.Timer t)
		{
			this.t = t;
		}
		public override void AddToTick(Action<object, EventArgs> action) => t.Tick += new EventHandler(action);
		public override void Start() => t.Start();
		public override void Stop() => t.Stop();
		public override double Interval
		{
			get => t.Interval;
			set => t.Interval = (int)value;
		}
	}

	class IndependentTicker : Ticker
	{
		System.Timers.Timer Timer = new System.Timers.Timer();
		public override void AddToTick(Action<object, EventArgs> action)
		{
			Timer.Elapsed += new System.Timers.ElapsedEventHandler(
				(object o, System.Timers.ElapsedEventArgs e) => action(o, e));
		}
		public override double Interval
		{
			get => Timer.Interval;
			set => Timer.Interval = value;
		}

		public override void Start() => Timer.Start();
		public override void Stop() => Timer.Stop();
	}
}
