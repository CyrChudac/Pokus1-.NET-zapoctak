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
		public int fallingSpeed { get; protected set; } = 10;
		protected static readonly int arrowImportness = Time.delay / 3;
		public static float shift => Time.TimeFlow * 10;
		protected int speed;
		protected Location location = new Location();
		public Location FinalDirection
			=> location.Normalize(shift);
		
		public void Fall() => AddToDirection(new Location(0, fallingSpeed));
		public void AddToDirection(Location loc)
		{
			location += loc * arrowImportness;
		}
		public virtual void Move(int speed)
		{
			int tmp = this.speed;
			this.speed = speed;
			ResetAndMove();
			this.speed = tmp;
		}
		public abstract void ResetAndMove();
		public virtual void AddKey(Input input) => ActiveKeys.Add(input);
		public abstract List<Input> ActiveKeys { set; protected get; }
		public void ChangeSpeed(int newSpeed) { speed = newSpeed; }
	}

	public sealed class NoMovement: Movement
	{
		private NoMovement():base(0) { }
		public override void Move(int speed) { }
		public override void ResetAndMove() { }
		public static readonly NoMovement instance = new NoMovement();
		public override List<Input> ActiveKeys { protected get { return null; } set { } }
		public override void AddKey(Input input){}
	}
}
