﻿using System;
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
		public static Size DefaultSize => new Size(88, 88);
		public static readonly int defaultSpeed = 6 * Environment.OneTileWidth;
		public static readonly int defaultHealth = 100;
		[JsonConstructor]
		public Life(int maxHealth, int currHealth, Location location,
			Movement movement, IAnimation animation, Size size, string name, Environment map)
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
		public string Name { get; private set; }
		[JsonIgnore]
		public Location Middle => Location + (Location)Size / 2;
		[JsonIgnore]
		public int Height => Size.Height;
		[JsonIgnore]
		public int Width => Size.Width;
		public bool Alive { get; protected set; } = true;
		public int CurrHealth { get; protected set; }
		public int MaxHealth { get; private set; }
		protected virtual void DuringUpdate() { }
		public void Update()
		{
			Movement.BeforeMove();
			if (Alive)
			{
				DuringUpdate();
			}
			Movement.Fall();
			Location vector = Movement.CalculatedVector;
			Location += (Movement.FinalLocation = Map.GoToLocation(this, vector));
			Movement.AfterMove();
			Movement.Reset();
		}
		public Direction LookingAt { get; protected set; } = Direction.right;
		public IAnimation Animation { get; private set; }
		public virtual IAnimation DeadAnimation => DefaultDeadAnimation.instance;
		public Movement Movement { get; private set; }
		public Location Location { get; set; }
		public Size Size { get; protected set; }
		public Environment Map { get; set; }
		public void Attacked(INoninteractiveItem weapon)
		{
			CurrHealth -= Attack.DefaultDamage;
			if (CurrHealth <= 0)
				Alive = false;
		}
	}


}
