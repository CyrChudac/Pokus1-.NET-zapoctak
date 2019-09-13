using System;
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

	class Jumper : Player
	{
		static int jumpDuration = 800;
		[JsonIgnore]
		int jumpSpeed => 3;
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
				if (Map.DirectionAccesable(this, Direction.up))
					Movement.AddToDirection(new Location(0, -jumpSpeed - Movement.fallingSpeed));
				else
					lastTimeActivated = Time.Now - jumpDuration;
			else if (lastTimeActivated + 2 * jumpDuration > Time.Now
				&& Map.AmIFalling(this)
				&& Map.DirectionAccesable(this, Direction.up))
				Movement.AddToDirection(new Location(0, -Movement.fallingSpeed / 2));
		}

		public Jumper(int maxHealth, int currHealth, PlayerMovement movement,
			string name, Location location, IAnimation animation, Size size, Map map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
		}
	}
	
	class KnifeThrower : Player
	{
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
		static readonly int shotRange = 550;
		static readonly int sizeModifier = 2;
		static int projectileSpeed => Life.defaultSpeed * 2;
		public KnifeThrower(int maxHealth, int currHealth, PlayerMovement movement, 
			string name, Location location, IAnimation animation, Size size, Map map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
			attack = new RangedAttack(AttackSource.ally, shotRange, 
				new Size(Map.OneTileWidth / sizeModifier, Map.OneTileHeight / sizeModifier), projectileSpeed);
		}
	}
	
	class Puddler : Player
	{
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

		public Puddler(int maxHealth, int currHealth, PlayerMovement movement,
			string name, Location location, IAnimation animation, Size size, Map map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
		}
	}
	
	class Unskilled : Player
	{
		public Unskilled(int maxHealth, int currHealth, PlayerMovement movement,
			string name, Location location, IAnimation animation, Size size, Map map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
		}

		protected override void SkillUpdate() {}

		protected override void UseSkill() {}

	}
}