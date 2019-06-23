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
		public Movement(int speed, int fallingSpeed):this(speed)
		{
			this.fallingSpeed = fallingSpeed;
		}
		public Movement(int speed) {
			this.speed = speed;
		}
		protected int fallingSpeed = 2;
		protected static readonly int shift = 20;
		protected int speed;
		protected Location location = new Location();
		public Location FinalDirection
		{
			get
			{
				Location result = location.Normalize();
				location = new Location();
				return result;
			}
		}
		public void Fall() => AddToDirection(new Location(0, fallingSpeed));
		public void AddToDirection(Location loc)
		{
			location.x += loc.x * shift;
			location.y += loc.y * shift;
		}
		public virtual void Move(int speed)
		{
			int tmp = this.speed;
			this.speed = speed;
			Move();
			this.speed = tmp;
		}
		public abstract void Move();
		public virtual void AddKey(Input input) => ActiveKeys.Add(input);
		public abstract List<Input> ActiveKeys { set; protected get; }
		public void ChangeSpeed(int newSpeed) { speed = newSpeed; }
	}

	public sealed class NoMovement: Movement
	{
		private NoMovement():base(0) { }
		public override void Move(int speed) { }
		public override void Move() { }
		public static readonly NoMovement instance = new NoMovement();
		public override List<Input> ActiveKeys { protected get { return null; } set { } }
		public override void AddKey(Input input){}
	}
}
