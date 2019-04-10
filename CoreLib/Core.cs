using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Location Normalize()
		{
			int d = (int)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
			int x = this.x/d;
			int y = this.y/d;
			return new Location(x, y);
		}
		public int x, y;
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
	
	public enum Directions { down, left, right, up}

}
