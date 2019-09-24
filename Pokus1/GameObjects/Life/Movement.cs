using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	public class Movement : ILocationHolder
	{
		[JsonConstructor]
		public Movement(int speed, int fallingSpeed):this(speed)
		{
			this.fallingSpeed = fallingSpeed;
		}
		public Movement(int speed) {
			this.Speed = speed;

			AddToDirection(new Location(1, 1));
			_exLoc = CalculatedVector;
		}
		[JsonRequired]
		public int fallingSpeed { get; protected set; } = 10 * Environment.OneTileHeight;

		public void AddDirectionToDirection(Direction dir) => AddToDirection((Location)dir * DirectionsImportness);
		public static readonly int DirectionsImportness = Time.delay / 3;
		public static float shift => Math.Max(1f / Life.defaultSpeed, Time.TimeFlow * Math.Max(Time.DeltaTime, 1) / 1000);
		[JsonRequired]
		public int Speed { get; protected set; }
		[JsonIgnore]
		private Location location = new Location();
		public object locationLocker = new object();
		[JsonIgnore]
		public Location CalculatedVector
			=> location.PseudoNormalize(Speed * shift, fallingSpeed * shift);

		Location _exLoc;
		public Location ExampleLoc => _exLoc;

		public void Fall() => AddToDirection(new Location(0, DirectionsImportness));
		public void AddToDirection(Location loc)
		{
			location += loc;
		}
		public void Reset() => location = new Location();
		public virtual void BeforeMove() { }
		public virtual void AfterMove() { }
		public void ChangeSpeed(int newSpeed) { Speed = newSpeed; }


		public Location FinalLocation { private get; set; } = new Location();
		Location ILocationHolder.GetHolding()
			=> FinalLocation;
	}

	public interface IMovableObject : IGameObject
	{
		Movement Movement { get; }
	}
	
	public sealed class NoMovement: Movement
	{
		private NoMovement():base(0, 0) { }

		public static readonly NoMovement instance = new NoMovement();
	}

	public static class LocationExtensions
	{
		public static Location PseudoNormalize(this Location l, float xModificator, float yModificator)
		{
			int d = l.Distance;
			if (d != 0)
			{
				int x = (int)((l.x * xModificator) / d);
				int y = Math.Sign(l.y) * (int)yModificator;
				return new Location(x, y);
			}
			return new Location();
		}
	}
}
