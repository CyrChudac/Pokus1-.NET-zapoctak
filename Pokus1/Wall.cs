using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;

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
		public Color Color { get; } = Color.Black;
		private FullWall() { }
		public static readonly IMapTile Tile = new FullWall();
	}

	/// <summary>
	/// This MapTile kills the player.
	/// </summary>
	class Killer : IMapTile
	{
		public Color Color { get; } = Color.IndianRed;
		private Killer() { }
		public static readonly IMapTile Tile = new Killer();
	}

	/// <summary>
	/// This represents nonexistent mapTile.
	/// </summary>
	class NoTile : IMapTile
	{
		public Color Color { get; } = Color.SkyBlue;
		private NoTile() { }
		public static readonly IMapTile Tile = new NoTile();
	}

	/// <summary>
	/// All MapTile singletons should be listed here.
	/// </summary>
	static class TileBase
	{
		public static IMapTile Get(TileType type)
		{
			switch (type)
			{
				case TileType.nothing:
					return NoTile.Tile;
				case TileType.killer:
					return Killer.Tile;
				case TileType.wall:
					return FullWall.Tile;
				default:
					throw new WrongTileTypeFoundException();
			}
		}
		public class WrongTileTypeFoundException : Exception { } 
	}
	enum TileType { nothing, killer, wall}
}
