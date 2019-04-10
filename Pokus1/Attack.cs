using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class Attack : IAction
	{
		public void Proceed()
		{
			throw new NotImplementedException();
		}
	}
	public abstract class EnemyAttack : Attack
	{
		public class Melee : EnemyAttack
		{
			public readonly float attackDistance;
			/// <param name="attackDistance">How far (relatively to map tiles) does attack go.</param>
			public Melee(float attackDistance)
			{ this.attackDistance = attackDistance; }
		}
		public class Ranged : EnemyAttack
		{

		}
	}
}
