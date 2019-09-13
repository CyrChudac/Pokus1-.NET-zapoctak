using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	public abstract class Movement
	{
		public Movement(int speed, int fallingSpeed):this(speed)
		{
			this.fallingSpeed = fallingSpeed;
		}
		public Movement(int speed) {
			this.Speed = speed;
		}
		[JsonRequired]
		public int fallingSpeed { get; protected set; } = 10;

		protected static readonly int directionsImportness = Time.delay / 3;
		public static float shift => Time.TimeFlow * Time.DeltaTime ;
		[JsonRequired]
		public int Speed { get; protected set; }
		[JsonIgnore]
		protected Location location = new Location();
		[JsonIgnore]
		public Location FinalDirection
			=> location.Normalize(Speed, fallingSpeed);
		
		public void Fall() => AddToDirection(new Location(0, fallingSpeed)* shift);
		public void AddToDirection(Location loc)
		{
			location += loc * directionsImportness;
		}
		public virtual void Move(int speed)
		{
			int tmp = this.Speed;
			this.Speed = speed;
			Move();
			this.Speed = tmp;
		}
		public void ResetAndMove()
		{
			Reset();
			Move();
		}
		public void ResetAndMove(int speed)
		{
			Reset();
			Move(speed);
		}
		public void Reset() => location = new Location();
		public abstract void Move();
		public void ChangeSpeed(int newSpeed) { Speed = newSpeed; }
	}

	public interface IMovableObject : IGameObject
	{
		Movement Movement { get; }
	}
	
	public abstract class PlayerMovement: Movement
	{
		public PlayerMovement(int speed) : base(speed) { }
		public PlayerMovement(int speed, int fallingSpeed) : base(speed, fallingSpeed) { }
		public virtual void AddKey(Input input) => ActiveKeys.Add(input);
		public abstract List<Input> ActiveKeys { set; protected get; }
	}
	
	public sealed class NoMovement: PlayerMovement
	{
		private NoMovement():base(0) { }
		public override void Move(int speed) { }
		public override void Move() { }

		public override void AddKey(Input input) { }

		public static readonly NoMovement instance = new NoMovement();

		public override List<Input> ActiveKeys { protected get => new List<Input>(); set { } }
	}
}
