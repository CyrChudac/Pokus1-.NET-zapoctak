using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class Enemy : Life
	{
		public Enemy(int maxHealth, int currHealth, Movement movement, EnemyType Type, Location location)
			: base(maxHealth, currHealth, location, movement)
		{
			this.Type = Type;
			movement.ChangeSpeed(Speed);
		}
		public abstract Enemy Copy();
		public EnemyType Type { get; protected set; }

		public abstract int WaitingOnWalkEnd { get; }

		public abstract EnemyAttack Attack { get; }

		public abstract int VisionRange { get; }

		public abstract int Speed { get; }
	}
	public class NormalEnemy : Enemy
	{
		public NormalEnemy(int maxHealth, int currHealth, Movement movement, EnemyType Type, Location location)
			: base(maxHealth, currHealth, movement, Type, location)
		{}
		public override void Update()
		{
			throw new NotImplementedException();
		}
		public override Enemy Copy()
		{
			throw new NotImplementedException();
		}

		public override int WaitingOnWalkEnd => 800; // time that enemy stays at the platform edge
		public override int Speed => 75;		//speed
		public override int VisionRange => 200;		//how far enemy sees

		public override EnemyAttack Attack { get; } = new EnemyAttack.Melee(1);
	}

	public class EnemyBase
	{
		public Enemy Get(int maxHealth, int currHealth, Movement movement, EnemyType type, Location location)
		{
			switch (type)
			{
				case EnemyType.normal:
					return new NormalEnemy(maxHealth, currHealth, movement, type, location);
				default:
					throw new WrongEnemyTypeFoundException();
			}
		}
		public class WrongEnemyTypeFoundException : Exception { }
	}
	public enum EnemyType { normal }
}
