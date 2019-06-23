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
		double lastTimeActivated;
		protected override void UseSkill()
		{
			lastTimeActivated = Time.Now;
			throw new NotImplementedException();
		}

		protected override void SkillUpdate()
		{
			throw new NotImplementedException();
		}

		public Jumper(int maxHealth, int currHealth,
			Movement movement, string name, Location location, IAnimation animation,
			Size size) : base(maxHealth, currHealth, movement, name, location, animation, size)
		{
		}
	}

	class KnifeThrower : Player
	{
		protected override void UseSkill()
		{
			throw new NotImplementedException();
		}

		protected override void SkillUpdate()
		{
			throw new NotImplementedException();
		}

		public KnifeThrower(int maxHealth, int currHealth,
			Movement movement, string name, Location location, IAnimation animation,
			Size size) : base(maxHealth, currHealth, movement, name, location, animation, size)
		{
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

		public Puddler(int maxHealth, int currHealth,
			Movement movement, string name, Location location, IAnimation animation,
			Size size) : base(maxHealth, currHealth, movement, name, location, animation, size)
		{
		}
	}

	class Unskilled : Player
	{
		public Unskilled(int maxHealth, int currHealth,
			Movement movement, string name, Location location, IAnimation animation,
			Size size) : base(maxHealth, currHealth, movement, name, location, animation, size)
		{
		}

		protected override void SkillUpdate() {}

		protected override void UseSkill() {}

	}
}