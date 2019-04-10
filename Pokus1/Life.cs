﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class Life : IGameObject
	{
		public Life(int maxHealth, int currHealth, Location location, Movement movement)
		{
			this.Health = currHealth;
			this.StartingHealth = maxHealth;
			this.Movement = movement;
		}
		public int Height { get; protected set; }
		public int Width { get; protected set; }
		public bool Alive { get; protected set; } = true;
		public int Health { get; protected set; }
		public int StartingHealth { get; private set; }
		public abstract void Update();
		public Directions LookingAt { get; protected set; }
		public virtual int FallingSpeed { get; }
		public IAnimation Animation { get; }
		public Movement Movement { get;}
		public Location Location { get; set; }
		public Location Size { get; protected set; }
		protected Map map;
	}


}