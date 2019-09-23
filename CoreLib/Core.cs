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

	public static class JsonDefault
	{
		public static JsonSerializer DefaultSerializer 
				=> new JsonSerializer()
				{
					TypeNameHandling = TypeNameHandling.Auto,
					Formatting = Formatting.Indented,
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					PreserveReferencesHandling = PreserveReferencesHandling.Objects
				};
	}
}
