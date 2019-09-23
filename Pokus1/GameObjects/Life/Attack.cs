using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public enum AttackSource { enemy, ally, both }
	public abstract class Attack
	{
		public static int DefaultDamage => 10;
		public Attack(AttackSource source, int attackRange)
		{
			this.source = source;
			this.attackRange = attackRange;
		}
		protected int attackRange;
		public readonly AttackSource source;
		public abstract void Proceed(Life source);

	}


	public class RangedAttack : Attack
	{
		readonly Size projectileSize;
		int projectileSpeed;
		public RangedAttack(AttackSource source, int attackRange,
			Size projectileSize, int projectileSpeed)
			: base (source, attackRange)
		{
			this.projectileSize = projectileSize;
			this.projectileSpeed = projectileSpeed;
		}
		public override void Proceed(Life source)
		{
			IAnimation animation;
			if (this.source == AttackSource.ally)
				animation = new SingleColorAnimation(Color.ForestGreen);
			else if (this.source == AttackSource.enemy)
				animation = new SingleColorAnimation(Color.OrangeRed);
			else if (this.source == AttackSource.both)
				animation = new SingleColorAnimation(Color.RosyBrown);
			else throw new Exception("Unknown type (" + this.source.ToString() +
				") of attackSource is trying to fire a projectile.");
			source.Map.NoninteractiveItems.Add(
			new Projectile(
				this.source,
				attackRange,
				source.Middle + ((Location)source.LookingAt * ((Location)source.Size / 2)),
				projectileSize,
				source.Name,
				source.Map,
				projectileSpeed,
				source.LookingAt,
				animation));
		}
	}

	public class MeleeAttack : Attack
	{
		public MeleeAttack(AttackSource source, int attackRange) : base(source, attackRange)
		{
		}

		public override void Proceed(Life source)
		{
			throw new NotImplementedException();
		}
	}

}
