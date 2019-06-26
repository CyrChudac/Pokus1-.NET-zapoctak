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
		public Camera(Map map)
		{
			this.map = map;
		}
		Map map;
		int width => map.oneTileWidth * map.Width;
		int height => map.oneTileHeight * map.Height;
		int maxX => width - Screen.PrimaryScreen.Bounds.Size.Width;
		int maxY => height - Screen.PrimaryScreen.Bounds.Size.Height;
		bool freezeX, freezeY;
		public Movement Movement { protected get; set; }

		public void NewPlayerLocation(Location location)
		{
			RealLocation = new Location(
				location.x - Screen.PrimaryScreen.Bounds.Size.Width / 2,
				location.y - Screen.PrimaryScreen.Bounds.Size.Height/ 2);
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
			if(RealLocation != Location)
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
