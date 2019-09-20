using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using System.Windows.Forms;
using System.Drawing;

namespace Pokus1
{
	public interface IWithCanvasSize
	{
		Size CanvasSize { get; }
	}
	public interface ICamera
	{
		ILocationHolder locationHolder { set; }
		void NewPlayerLocation(Location location);
		Location Location { get; }
		void Update();
	}
	public class Camera : ICamera
	{
		public Camera(Size mapSize, IWithCanvasSize renderer)
		{
			this.MapSize = mapSize;
			this.renderer = renderer;
		}

		public Camera(Size mapSize, IWithCanvasSize renderer, Location location)
			: this(mapSize, renderer)
		{
			this.Location = location;
			this.RealLocation = location;
		}
		IWithCanvasSize renderer;
		readonly Size MapSize;
		int width => MapSize.Width;
		int height => MapSize.Height;
		int maxX => Math.Max(0, width - renderer.CanvasSize.Width);
		int maxY => Math.Max(0, height - renderer.CanvasSize.Height);
		bool freezeX, freezeY;
		public ILocationHolder locationHolder { protected get; set; }

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
			RealLocation += locationHolder.GetHolding();
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
