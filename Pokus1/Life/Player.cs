using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;

namespace Pokus1
{
	public class Player : Life
	{
		readonly public string name;
		public new Movement Movement { get { return base.Movement; }}
		public Player(SkillType skillType, int maxHealth, int currHealth, 
			Movement movement, string name, Location location, IAnimation animation, Size size)
			:base(maxHealth, currHealth, location, movement, animation, size)
		{
			this.Skill = ISkill.Get(skillType, this);
			this.name = name;
		}
		public Inventory Items { get; protected set; } = new Inventory();
		public ISkill Skill { get; protected set; }
		public void PropagateInput(Input input)
		{
			if ((input == null) || (input is Input.Player.Movement)){
				Movement.AddKey(input);
			}
			if (input is Input.Player.SkillUse)
				Skill.Use();
		}
		public override void Update()
		{

			Movement.
			throw new NotImplementedException();
		}
		protected List<IAction> actions = new List<IAction>();

		public Player Copy()
		{
			throw new NotImplementedException();
		}
	}

	public class Inventory : List<Item> { }
}
