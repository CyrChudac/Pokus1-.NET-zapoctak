using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;

namespace Pokus1
{
	public abstract class Enemy : Life
	{
		public Enemy(int maxHealth, int currHealth, Movement movement, EnemyType Type,
			Location location, IAnimation animation, Size size, string name, Map map)
			: base(maxHealth, currHealth, location, movement, animation, size, name, map)
		{
			this.Type = Type;
			movement.ChangeSpeed(Speed);
		}
		public EnemyType Type { get; protected set; }

		public abstract int WaitingOnWalkEnd { get; } // time that enemy stays at the platform edge

		public abstract Attack Attack { get; }

		public abstract int VisionRange { get; }

		public abstract int Speed { get; }
	}

	public class NormalEnemy : Enemy
	{
		public static readonly int DefaultMaxHealth = Attack.DefaultDamage;
		public NormalEnemy(int currHealth, Movement movement, 
			EnemyType Type, Location location, IAnimation animation, Size size, int number, Map map)
			: base(DefaultMaxHealth, Math.Min(currHealth,Attack.DefaultDamage), movement, Type, location,
				  animation, size, nameof(NormalEnemy) + number.ToString(), map)
		{}
		public NormalEnemy(Movement movement, EnemyType Type, Location location,
			IAnimation animation, Size size, int number, Map map)
			: base(DefaultMaxHealth, DefaultMaxHealth, movement, Type, location,
				  animation, size, nameof(NormalEnemy) + number.ToString(), map)
		{ }

		public override int WaitingOnWalkEnd => 800; 
		public override int Speed => Life.defaultSpeed;
		public override int VisionRange => 200;

		public override Attack Attack { get; } = new MeleeAttack(AttackSource.enemy, 150);
		protected override void DuringUpdate() => throw new NotImplementedException();
	}
	
	public class PassiveEnemy : NormalEnemy
	{
		public PassiveEnemy(Location location, int number, Map map)
			: base(new UsualMovement(Life.defaultSpeed), EnemyType.normal, location,
				  new SingleColorAnimation(Color.OrangeRed), Life.DefaultSize, number, map)
		{ }
		protected override void DuringUpdate() { }
	}

	//public class EnemyBase
	//{
	//	public Enemy Get(int maxHealth, int currHealth, Movement movement,
	//		EnemyType type, Location location, IAnimation animation, Size size)
	//	{
	//		switch (type)
	//		{
	//			case EnemyType.normal:
	//				return new NormalEnemy(maxHealth, currHealth, movement, type, location, animation, size);
	//			default:
	//				throw new WrongEnemyTypeFoundException();
	//		}
	//	}
	//	public class WrongEnemyTypeFoundException : Exception { }
	//}

	public enum EnemyType { normal }
}
