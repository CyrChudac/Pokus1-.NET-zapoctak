using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using System.Runtime.Serialization;

namespace Pokus1
{
	[Serializable]
	public abstract class Life : IMovableObject
	{
		public static Size DefaultSize => new Size(100, 100);
		public static readonly int defaultSpeed = 10;
		public Life(int maxHealth, int currHealth, Location location,
			Movement movement, IAnimation animation, Size size, string name, Map map)
		{
			this.Health = currHealth;
			this.StartingHealth = maxHealth;
			this.Movement = movement;
			this.Size = size;
			this.Location = location;
			this.Animation = animation;
			this.Name = name;
			this.map = map;
		}
		[DataMember()]
		public readonly string Name;
		public Location Middle => Location + (Location)Size / 2;
		[DataMember()]
		public int Height => Size.Height;
		[DataMember()]
		public int Width => Size.Width;
		[DataMember()]
		public bool Alive { get; protected set; } = true;
		[DataMember()]
		public int Health { get; protected set; }
		[DataMember()]
		public int StartingHealth { get; private set; }
		protected virtual void DuringUpdate() { }
		public void Update()
		{
			Movement.Reset();
			if (Alive)
			{
				Movement.Move();
				DuringUpdate();
			}
			if (map.AmIFalling(this))
				Movement.Fall();
			Location += Movement.FinalDirection;
		}
		[DataMember()]
		public Direction LookingAt { get; protected set; } = Direction.right;
		[DataMember()]
		public virtual int FallingSpeed { get; }
		[DataMember()]
		public readonly IAnimation Animation;
		public virtual IAnimation DeadAnimation => DefaultDeadAnimation.instance;
		[DataMember()]
		public Movement Movement { get; }
		[DataMember()]
		public Location Location { get; set; }
		[DataMember()]
		public Size Size { get; protected set; }
		public Map map { get; set; }
		public void Attacked(INoninteractiveItem weapon)
		{
			Health -= Attack.DefaultDamage;
			if (Health <= 0)
				Alive = false;
		}
	}


}
