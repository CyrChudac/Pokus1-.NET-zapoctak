using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class Movement
	{
		public Movement(int speed) { this.speed = speed; }
		protected int speed;
		protected Location location = new Location { x = 0, y = 0};
		public Location FinalDirection => location.Normalize();
		public void AddToDirection(Location loc)
		{
			location.x += loc.x;
			location.y += loc.y;
		}
		protected abstract void Move(int speed);
		protected abstract void Move();
		public virtual void AddKey(Input input) => ActiveKeys.Add(input);
		public abstract List<Input> ActiveKeys { set; protected get; }
		public void ChangeSpeed(int newSpeed) { speed = newSpeed; }
	}

	public sealed class NoMovement: Movement
	{
		private NoMovement():base(0) { }
		protected override void Move(int speed) { }
		protected override void Move() { }
		public static readonly NoMovement instance = new NoMovement();
		public override List<Input> ActiveKeys { protected get { return null; } set { } }
		public override void AddKey(Input input){}
	}
}
