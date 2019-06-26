using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CoreLib
{
    public interface IGameObject
    {
		int Width { get; }
		int Height { get; }
		Location Location { get; }
    }

	public interface INotLife : IGameObject
	{
	}

	public struct Location
	{
		public Location(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public Location Normalize(float modificator)
		{
			int d = (int)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
			if (d != 0)
			{
				int x = (int)((this.x * modificator)/ d);
				int y = (int)((this.y * modificator) / d);
				return new Location(x, y);
			}
			return new Location();
		}
		public int x, y;

		public static Size DefaultLifeSize => new Size(100, 100);

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

		#endregion

		public override string ToString()
		{
			return nameof(Location) + ": X = " + x.ToString() + "; Y = " + y.ToString();
		}
	}

	public class InputPoss {
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

}
