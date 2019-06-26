﻿using System;
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
	public abstract class Life : IGameObject
	{
		public static readonly int defaultSpeed = 3;
		public Life(int maxHealth, int currHealth, Location location,
			Movement movement, IAnimation animation, Size size)
		{
			this.Health = currHealth;
			this.StartingHealth = maxHealth;
			this.Movement = movement;
			this.Size = size;
			this.Location = location;
			this.Animation = animation;
		}
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
		public abstract void Update();
		[DataMember()]
		public Direction LookingAt { get; protected set; }
		[DataMember()]
		public virtual int FallingSpeed { get; }
		[DataMember()]
		public readonly IAnimation Animation;
		[DataMember()]
		public Movement Movement { get; }
		[DataMember()]
		public Location Location { get; set; }
		[DataMember()]
		public Size Size { get; protected set; }
		public Map map { protected get; set; }
	}


}
