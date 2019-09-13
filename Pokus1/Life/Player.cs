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
	[Serializable]
	public abstract class Player : Life
	{
		public Player(int maxHealth, int currHealth, 
			PlayerMovement movement, string name, Location location, IAnimation animation, Size size, Map map)
			:base(maxHealth, currHealth, location, movement, animation, size, name, map)
		{
		}
		[DataMember()]
		public Inventory Items { get; protected set; } = new Inventory();

		protected abstract void UseSkill();

		public virtual Image DefaultImage => Animation.Image;
		public void PropagateInput(Input input)
		{
			if (input is Input.Player.Movement.Left)
			{
				LookingAt = Direction.left;
				if (Map.DirectionAccesable(this, Direction.left))
					((PlayerMovement)Movement).AddKey(input);
			}
			else
			if (input is Input.Player.Movement.Right)
			{
				LookingAt = Direction.right;
				if (Map.DirectionAccesable(this, Direction.right))
					((PlayerMovement)Movement).AddKey(input);
			}
			else
			if (input is Input.Player.SkillUse)
				UseSkill();
			else
				throw new ArgumentException("Unknown input type = " + input.GetType());
		}
		protected override void DuringUpdate() => SkillUpdate();
		protected abstract void SkillUpdate();
	}

	[Serializable]
	public class Inventory : List<Item> { }
}
