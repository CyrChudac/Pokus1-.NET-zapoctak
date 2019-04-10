using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class ISkill
	{
		readonly Player player;
		public ISkill(Player player) { this.player = player; }
		public abstract void Use();
		public abstract void Update();
		public static ISkill Get(SkillType type, Player player)
		{
			switch (type)
			{
				case SkillType.noSkill:
					return new NoSkill(player);
				case SkillType.jump:
					return new Jump(player);
				case SkillType.knifeThrow:
					return new KnifeThrow(player);
				case SkillType.puddle:
					return new Puddle(player);
			}
			throw new Exception("Uknown skill type given while making a skill.");
		}
	}

	public class NoSkill : ISkill
	{
		public NoSkill(Player player) : base(player) { }
		public override void Update()
		{ }
		public override void Use()
		{ }
	}

	class Jump : ISkill
	{
		public Jump(Player player) : base(player) { }
		double lastTimeActivated = 0;
		public override void Update()
		{
			throw new NotImplementedException();
		}

		public override void Use()
		{
			lastTimeActivated = Time.Now;
			throw new NotImplementedException();
		}
	}

	class KnifeThrow : ISkill
	{
		public KnifeThrow(Player player) : base(player) { }
		public override void Update()
		{
		}

		public override void Use()
		{
			throw new NotImplementedException();
		}
	}

	class Puddle : ISkill
	{
		public Puddle(Player player) : base(player){ }
		bool active = false;
		public override void Update()
		{
			throw new NotImplementedException();
		}

		public override void Use()
		{
			active = !active;
		}
	}

	public enum SkillType { noSkill, jump, knifeThrow, puddle}
}