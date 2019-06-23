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
		[DataMember()]
		readonly public string name;
		[DataMember()]
		public new Movement Movement { get { return base.Movement; }}
		public Player(int maxHealth, int currHealth, 
			Movement movement, string name, Location location, IAnimation animation, Size size)
			:base(maxHealth, currHealth, location, movement, animation, size)
		{
			this.name = name;
		}
		[DataMember()]
		public Inventory Items { get; protected set; } = new Inventory();

		protected abstract void UseSkill();

		public void PropagateInput(Input input)
		{
			if ((input == null) || (input is Input.Player.Movement)){
				Movement.AddKey(input);
			}
			if (input is Input.Player.SkillUse)
				UseSkill();
		}
		public override void Update()
		{
			Movement.Move();
			if (map.AmIFalling(this))
				Movement.Fall();
			SkillUpdate();
			Location += Movement.FinalDirection;
		}
		protected abstract void SkillUpdate();
		protected List<IAction> actions = new List<IAction>();
	}

	[Serializable]
	public class Inventory : List<Item> { }
}
