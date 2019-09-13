using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	/// <summary>
	/// Every MapTile, that contains wall should be IWall type.
	/// </summary>
	interface IWall : IMapTile
	{}

	/// <summary>
	/// MapTile, that has nothing else than wall.
	/// </summary>
	class FullWall: IWall
	{
		[JsonIgnore]
		public Color Color { get; } = Color.Black;
		[JsonIgnore]
		public Brush Brush { get; private set; }
		private FullWall() { Brush = new SolidBrush(Color); }
		public static readonly IMapTile Tile = new FullWall();
	}

	/// <summary>
	/// This MapTile kills the player.
	/// </summary>
	class Killer : IMapTile
	{
		[JsonIgnore]
		public Color Color { get; } = Color.IndianRed;
		[JsonIgnore]
		public Brush Brush { get; private set; }
		private Killer() { Brush = new SolidBrush(Color); }
		public static readonly IMapTile Tile = new Killer();
	}

	/// <summary>
	/// This represents nonexistent mapTile.
	/// </summary>
	class NoTile : IMapTile
	{
		[JsonIgnore]
		public Color Color { get; } = Color.SkyBlue;
		[JsonIgnore]
		public Brush Brush { get; private set; }
		private NoTile() { Brush = new SolidBrush(Color); }
		public static readonly IMapTile Tile = new NoTile();
	}
}
