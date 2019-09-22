using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace CoreLib
{
    public interface IGameObject
    {
		Size Size { get; }
		int Width { get; }
		int Height { get; }
		Location Location { get; }
		string Name { get; }
		IAnimation Animation { get; }
	}

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

		public static implicit operator Location(Point p) => new Location(p.X,p.Y);
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
			=> new Location(- location.x, -location.y);

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

	public class InputPoss {
		public static readonly Input down = new Input.Player.Movement.Down();
		public static readonly Input up = new Input.Player.Movement.Up();
		public static readonly Input skill = new Input.Player.SkillUse(); 
		public static readonly Input right = new Input.Player.Movement.Right();
		public static readonly Input left = new Input.Player.Movement.Left();
		public static readonly Input menu = new Input.WholeGame.Menu(); 
		public static readonly Input restart = new Input.WholeGame.Restart(); 
		public static readonly Input changePlayerRight = new Input.WholeGame.ChangeChar.Right(); 
		public static readonly Input changePlayerLeft = new Input.WholeGame.ChangeChar.Left(); 
		public static readonly Input nothing = null;
		public static readonly Exception wrongInputFoundException = new WrongInputFoundException();
		class WrongInputFoundException : Exception{ }
	}
	
	public enum Direction { down, left, right, up}

	public static class IntExtesions
	{
		/// <summary>
		/// Works properly for negative values.
		/// </summary>
		public static int Modulo(this int a, int b)
		{
			int result = a - ((a / b) * b);
			if (result < 0)
				return result + b;
			return result;
		}
	}

	public static class ColorExtensions
	{
		public static Color OpositeColor(this Color c)
		{
			return Color.FromArgb(
				255 - c.R,
				255 - c.G,
				255 - c.B);
		}
	}

	public static class Json
	{
		public static JsonSerializer DefaultSerializer { get
			{
				JsonSerializer js = new JsonSerializer();
				js.TypeNameHandling = TypeNameHandling.Auto;
				js.Formatting = Formatting.Indented;
				js.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				js.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
				return js;
			} }
	}
}
