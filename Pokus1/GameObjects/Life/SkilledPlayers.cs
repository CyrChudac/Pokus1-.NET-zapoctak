﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	public enum SkillType { noSkill, jump, knifeThrow, puddle}

	class Jumper : PlayerCharacter
	{
		public static Color Color = Color.GreenYellow;
		[JsonIgnore]
		int jumpDuration => 666;
		[JsonIgnore]
		int jumpImportness => Movement.DirectionsImportness * 2 / 3;
		[JsonRequired]
		double lastTimeActivated = 0;
		protected override void UseSkill()
		{
			if (!Map.AmIFalling(this))
				lastTimeActivated = Time.Now;
		}

		protected override void SkillUpdate()
		{
			if (lastTimeActivated + jumpDuration > Time.Now)
			{
				Movement.AddToDirection(new Location(0, -jumpImportness - Movement.DirectionsImportness));
				if (!Map.DirectionAccesable(this, Direction.up))
					lastTimeActivated = Time.Now - jumpDuration;
			}
			else if (lastTimeActivated + 2 * jumpDuration > Time.Now
				&& Map.AmIFalling(this)
				&& Map.DirectionAccesable(this, Direction.up))
				Movement.AddToDirection(new Location(0, -Movement.DirectionsImportness / 2));
		}
		[JsonConstructor]
		public Jumper(int maxHealth, int currHealth, Movement movement,
			Location location, IAnimation animation, Size size, Environment map)
			: base(maxHealth, currHealth, movement, "Francis",
				  location, animation, size, map)
		{
		}
		public Jumper(int maxHealth, int currHealth, Movement movement,
			Location location, Size size, Environment map)
			: this(maxHealth, currHealth, movement,
				  location, new SingleColorAnimation(Color), size, map)
		{
		}
	}
	
	class KnifeThrower : PlayerCharacter
	{
		public static Color Color = Color.ForestGreen;
		[JsonIgnore]
		long cooldown => 1500;
		[JsonRequired]
		long lastTimeActivated = 0;
		protected override void UseSkill()
		{
			if (cooldown + lastTimeActivated < Time.Now)
			{
				lastTimeActivated = Time.Now;
				attack.Proceed(this);
			}
		}

		RangedAttack attack;
		protected override void SkillUpdate()
		{
			
		}
		static readonly int shotRange = Environment.OneTileWidth * 10;
		static readonly int sizeModifier = 2;
		static int projectileSpeed => Life.defaultSpeed * 2;
		public KnifeThrower(int maxHealth, int currHealth, Movement movement,
			Location location, Size size, Environment map)
			: this(maxHealth, currHealth, movement, location, new SingleColorAnimation(Color), size, map)
		{
		}
		[JsonConstructor]
		public KnifeThrower(int maxHealth, int currHealth, Movement movement,
			Location location, IAnimation animation, Size size, Environment map)
			: base(maxHealth, currHealth, movement, "Gerry", location, animation, size, map)
		{
			attack = new RangedAttack(AttackSource.ally, shotRange,
				new Size(Environment.OneTileWidth / sizeModifier, Environment.OneTileHeight / sizeModifier), projectileSpeed);
		}
	}
	
	class Puddler : PlayerCharacter
	{
		public static Color Color = Color.Orange;
		[JsonRequired]
		bool active = false;
		[JsonRequired]
		double lastTimeActivated = 0;
		protected override void UseSkill()
		{
			lastTimeActivated = Time.Now;
			active = !active;
			throw new NotImplementedException();
		}

		protected override void SkillUpdate()
		{
			throw new NotImplementedException();
		}

		[JsonConstructor]
		public Puddler(int maxHealth, int currHealth, Movement movement,
			Location location, IAnimation animation, Size size, Environment map)
			: base(maxHealth, currHealth, movement, "Igor", location, animation, size, map)
		{
		}
		public Puddler(int maxHealth, int currHealth, Movement movement,
			Location location, Size size, Environment map)
			: this(maxHealth, currHealth, movement, location, new SingleColorAnimation(Color), size, map)
		{
		}
	}
	
	class Unskilled : PlayerCharacter
	{
		public Unskilled(int maxHealth, int currHealth, Movement movement,
			string name, Location location, IAnimation animation, Size size, Environment map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
		}

		protected override void SkillUpdate() {}

		protected override void UseSkill() {}
	}
}