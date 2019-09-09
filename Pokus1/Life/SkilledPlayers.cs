using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public enum SkillType { noSkill, jump, knifeThrow, puddle}

	class Jumper : Player
	{
		static int jumpDuration = 800;
		int jumpSpeed = 3;
		double lastTimeActivated;
		protected override void UseSkill()
		{
			if (!map.AmIFalling(this))
				lastTimeActivated = Time.Now;
		}

		protected override void SkillUpdate()
		{
			if (lastTimeActivated + jumpDuration > Time.Now)
				if (map.DirectionAccesable(this, Direction.up))
					Movement.AddToDirection(new Location(0, -jumpSpeed - Movement.fallingSpeed));
				else
					lastTimeActivated = Time.Now - jumpDuration;
			else if (lastTimeActivated + 2 * jumpDuration > Time.Now
				&& map.AmIFalling(this)
				&& map.DirectionAccesable(this, Direction.up))
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
		readonly long cooldown = 1500;
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
		int shotRange = 550;
		static readonly int sizeModifier = 2;
		static int projectileSpeed => Life.defaultSpeed * 2;
		public KnifeThrower(int maxHealth, int currHealth, PlayerMovement movement, 
			string name, Location location, IAnimation animation, Size size, Map map)
			: base(maxHealth, currHealth, movement, name, location, animation, size, map)
		{
			attack = new RangedAttack(AttackSource.ally, shotRange, 
				new Size(map.oneTileWidth / sizeModifier, map.oneTileHeight / sizeModifier), projectileSpeed);
		}
	}

	class Puddler : Player
	{
		bool active = false;
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