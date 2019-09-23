using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Newtonsoft.Json;

namespace CoreLib
{

	public struct Location
	{
		public int x, y;


		public Location(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		/// <summary>
		/// Returns new Location(x * xModificator / Distance, Sign(y) * yMOdificator).
		/// </summary>
		public Location PseudoNormalize(float xModificator, float yModificator)
		{
			int d = Distance;
			if (d != 0)
			{
				int x = (int)((this.x * xModificator) / d);
				int y = Math.Sign(this.y) * (int)yModificator; //(int)((this.y * yModificator) / d);
				return new Location(x, y);
			}
			return new Location();
		}

		[JsonIgnore]
		public int Distance => (int)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

		#region operators

		public override bool Equals(object obj)
		{
			if (obj is Location)
			{
				return this == (Location)obj;
			}
			return false;

		}

		public override int GetHashCode() => 17 + 13 * x + 21 * y;

		public static bool operator ==(Location first, Location second) => first.x == second.x && first.y == second.y;

		public static bool operator !=(Location first, Location second) => !(first == second);

		public static explicit operator Size(Location l) => new Size(l);
		public static explicit operator Location(Size s) => new Location(s.Width, s.Height);

		public static implicit operator Location(Point p) => new Location(p.X, p.Y);
		public static implicit operator Point(Location p) => new Point(p.x, p.y);

		public static Location operator +(Point first, Location second)
			=> new Location(first.X + second.x, first.Y + second.y);
		public static Location operator +(Location second, Point first)
			=> first + second;

		public static Location operator +(Location first, Location second)
			=> new Location(first.x + second.x, first.y + second.y);

		public static Location operator -(Location first, Location second)
			=> new Location(first.x - second.x, first.y - second.y);

		public static Location operator -(Location location)
			=> new Location(-location.x, -location.y);

		public static Location operator *(Location first, int second)
			=> new Location(first.x * second, first.y * second);
		public static Location operator *(int second, Location first)
			=> first * second;

		public static Location operator /(Location first, int second)
			=> new Location(first.x / second, first.y / second);
		public static Location operator /(int second, Location first)
			=> first / second;

		public static Location operator *(Location first, float second)
			=> new Location((int)(first.x * second), (int)(first.y * second));
		public static Location operator *(float second, Location first)
			=> first * second;

		/// <summary>
		/// Works as: (a,b) * (c,d) = (a*c,b*d).
		/// </summary>
		public static Location operator *(Location first, Location second)
			=> new Location(first.x * second.x, first.y * second.y);

		public static explicit operator Location(Direction direction)
		{
			Location result = new Location();
			if (direction == Direction.right)
				result = new Location(result.x + 1, result.y);
			if (direction == Direction.left)
				result = new Location(result.x - 1, result.y);
			if (direction == Direction.up)
				result = new Location(result.x, result.y - 1);
			if (direction == Direction.down)
				result = new Location(result.x, result.y + 1);
			return result;
		}

		#endregion

		public override string ToString()
		{
			return nameof(Location) + ": X = " + x.ToString() + "; Y = " + y.ToString();
		}
	}

}
