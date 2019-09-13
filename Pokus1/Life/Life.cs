using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	public abstract class Life : IMovableObject
	{
		public static Size DefaultSize => new Size(100, 100);
		public static readonly int defaultSpeed = 10;
		[JsonConstructor]
		public Life(int maxHealth, int currHealth, Location location,
			Movement movement, IAnimation animation, Size size, string name, Map map)
		{
			this.CurrHealth = currHealth;
			this.MaxHealth = maxHealth;
			this.Movement = movement;
			this.Size = size;
			this.Location = location;
			this.Animation = animation;
			this.Name = name;
			this.Map = map;
		}
		public readonly string Name;
		public Location Middle => Location + (Location)Size / 2;
		public int Height => Size.Height;
		public int Width => Size.Width;
		public bool Alive { get; protected set; } = true;
		public int CurrHealth { get; protected set; }
		public int MaxHealth { get; private set; }
		protected virtual void DuringUpdate() { }
		public void Update()
		{
			Movement.Reset();
			if (Alive)
			{
				Movement.Move();
				DuringUpdate();
			}
			if (Map.AmIFalling(this))
				Movement.Fall();
			Location += Movement.FinalDirection;
		}
		public Direction LookingAt { get; protected set; } = Direction.right;
		public readonly IAnimation Animation;
		public virtual IAnimation DeadAnimation => DefaultDeadAnimation.instance;
		public Movement Movement { get; private set; }
		public Location Location { get; set; }
		public Size Size { get; protected set; }
		public Map Map { get; set; }
		public void Attacked(INoninteractiveItem weapon)
		{
			CurrHealth -= Attack.DefaultDamage;
			if (CurrHealth <= 0)
				Alive = false;
		}
	}


}
