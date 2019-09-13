using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using System.Windows.Forms;

namespace Pokus1
{
	public interface ICamera
	{
		Movement Movement { set; }
		void NewPlayerLocation(Location location);
		Location Location { get; }
		void Update();
	}
	public class Camera : ICamera
	{
		public Camera(Map map, IMapRenderer renderer)
		{
			this.map = map;
			this.renderer = renderer;
		}
		IMapRenderer renderer;
		Map map;
		int width => Map.OneTileWidth * map.Width;
		int height => Map.OneTileHeight * map.Height;
		int maxX => width - renderer.CanvasSize.Width;
		int maxY => height - renderer.CanvasSize.Height;
		bool freezeX, freezeY;
		public Movement Movement { protected get; set; }

		public void NewPlayerLocation(Location location)
		{
			RealLocation = new Location(
				location.x - renderer.CanvasSize.Width / 2,
				location.y - renderer.CanvasSize.Height / 2);
			freezeX = freezeY = false;
			Location = new Location(Clamp(RealLocation.x, maxX), Clamp(RealLocation.y, maxY));
		}

		private Location RealLocation;
		public Location Location { get; protected set; }

		public void Update()
		{
			RealLocation += Movement.FinalDirection;
			int x, y;
			if (freezeX)
				x = Location.x;
			else
				x = Clamp(RealLocation.x, maxX);
			if (freezeY)
				y = Location.y;
			else
				y = Clamp(RealLocation.y, maxY);
			Location = new Location(x, y);
			CheckAxes();
		}

		void CheckAxes()
		{
			if (RealLocation != Location)
			{
				if (RealLocation.x < 0 || RealLocation.x > maxX)
					freezeX = true;
				else
				{
					freezeX = false;
					RealLocation = new Location(Location.x, RealLocation.y);
				}
				if (RealLocation.y < 0 || RealLocation.y > maxY)
					freezeY = true;
				else
				{
					freezeY = false;
					RealLocation = new Location(RealLocation.x, Location.y);
				}
			}
		}

		int Clamp(int input, int max, int min = 0)
		{
			if (input > max)
				return max;
			else
				if (input < min)
				return min;
			else
				return input;
		}

	}
}
