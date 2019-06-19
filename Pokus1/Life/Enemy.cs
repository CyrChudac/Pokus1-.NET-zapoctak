using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using System.Runtime.Serialization;

namespace Pokus1
{
	[DataContract()]
	public abstract class Enemy : Life
	{
		public Enemy(int maxHealth, int currHealth, Movement movement,
			EnemyType Type, Location location, IAnimation animation, Size size)
			: base(maxHealth, currHealth, location, movement, animation, size)
		{
			this.Type = Type;
			movement.ChangeSpeed(Speed);
		}
		[DataMember()]
		public EnemyType Type { get; protected set; }

		public abstract int WaitingOnWalkEnd { get; }

		public abstract EnemyAttack Attack { get; }

		public abstract int VisionRange { get; }

		public abstract int Speed { get; }
	}
	[Serializable]
	public class NormalEnemy : Enemy
	{
		public NormalEnemy(int maxHealth, int currHealth, Movement movement, 
			EnemyType Type, Location location, IAnimation animation, Size size)
			: base(maxHealth, currHealth, movement, Type, location, animation, size)
		{}
		public override void Update()
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
		public Enemy Get(int maxHealth, int currHealth, Movement movement,
			EnemyType type, Location location, IAnimation animation, Size size)
		{
			switch (type)
			{
				case EnemyType.normal:
					return new NormalEnemy(maxHealth, currHealth, movement, type, location, animation, size);
				default:
					throw new WrongEnemyTypeFoundException();
			}
		}
		public class WrongEnemyTypeFoundException : Exception { }
	}
	public enum EnemyType { normal }
}
