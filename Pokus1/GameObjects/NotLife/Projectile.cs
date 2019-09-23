using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreLib;
using System.Drawing;

namespace Pokus1
{

	public class Projectile : INoninteractiveItem, IMovableObject
	{
		readonly Location startingLoc;
		readonly long distance;
		public IAnimation Animation { get; protected set; }
		public System.Drawing.Size Size { get; protected set; }

		public string Name { get; protected set; }

		public int Width => Size.Width;

		public int Height => Size.Height;

		public Location Location { get; protected set; }

		public Movement Movement { get; protected set; }

		Environment map;

		List<Direction> dirs = new List<Direction>();

		readonly AttackSource source;
		public Projectile(AttackSource source, int distance, Location location,
			Size size, string sourceName, Environment map, int speed, Direction direction, IAnimation animation)
			: this(source, distance, location, size, sourceName, map, new StrightMovement(speed, direction), animation)
		{
			dirs.Add(direction);
		}

		public Projectile(AttackSource source, int distance, Location location,
			Size size, string sourceName, Environment map, int speed, Location vector, IAnimation animation)
			: this(source, distance, location, size, sourceName, map, new StrightMovement(speed, vector), animation)
		{
			if (vector.x < 0)
				dirs.Add(Direction.up);
			else if (vector.x > 0)
				dirs.Add(Direction.down);
			if (vector.y < 0)
				dirs.Add(Direction.right);
			else if (vector.y > 0)
				dirs.Add(Direction.left);
		}

		private Projectile(AttackSource source, int distance, Location location,
			Size size, string sourceName, Environment map, Movement movement, IAnimation animation)
		{
			this.Movement = movement;
			this.source = source;
			this.distance = distance;
			this.Location = location;
			this.startingLoc = location;
			this.Size = size;
			this.Name = "Projectile of " + sourceName;
			this.map = map;
			this.Animation = animation;
		}
		public void Update()
		{
			Life corpse = null;
			if (source != AttackSource.ally && (corpse = map.AmIOnPlayer(this)) != null)
			{
				corpse.Attacked(this);
				map.RemoveMe(this);
			}
			else if (source != AttackSource.enemy && (corpse = map.AmIOnEnemy(this)) != null)
			{
				corpse.Attacked(this);
				map.RemoveMe(this);
			}
			if ((startingLoc - Location).Distance > distance)
				map.RemoveMe(this);
			if (dirs.TrueForAll(dir => map.DirectionAccesable(this, dir)))
			{
				Movement.BeforeMove();
				this.Location += Movement.CalculatedVector;
				Movement.AfterMove();
			}
			else
				map.RemoveMe(this);
		}
	}

}
