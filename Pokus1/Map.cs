using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public class Map
	{
		bool victory = false;
		public bool Victory => !Enemies.Any(Enemy => Enemy.Alive) ||victory;
		public void SetVictory() { victory = true; }
		bool defeat = false;
		public bool Defeat => !Players.All(Player => Player.Alive) || defeat;
		public void SetDefeat() { defeat = true; }
		public bool GameEnd => victory || defeat;

		public readonly int oneTileHeight;
		public readonly int oneTileWidth;
		public readonly int Height;
		public readonly int Width;
		public List<Player> Players { get; private set; }
		public List<Enemy> Enemies { get; private set; }

		public List<IInteractiveItem> OtherItems { get; private set; }
		private readonly IMapTile[,] map;

		/// <param name="x">Width</param>
		/// <param name="y">Height</param>
		public IMapTile this [int x, int y]
		{
			get { return map[x, y]; }

			set { map[x, y] = value; }
		}

		public Map Copy()
		{
			IMapTile[,] tiles = new IMapTile[map.GetLength(0), map.GetLength(1)];
			for(int i = 0; i < map.GetLength(0); i++)
				for (int j = 0; j < map.GetLength(1); j++)
				{
					tiles[i, j] = map[i, j];
				}

			List<Enemy> enemies = new List<Enemy>(Enemies.Count);
			foreach (Enemy e in Enemies)
				enemies.Add(e.Copy());

			List<Player> players = new List<Player>(Players.Count);
			foreach (Player p in Players)
				players.Add(p.Copy());

			List<IInteractiveItem> items = new List<IInteractiveItem>(OtherItems.Count);
			foreach (IInteractiveItem i in OtherItems)
				items.Add(i.Copy());

			Map ret = new Map(tiles, enemies, players, items, oneTileHeight, oneTileWidth);

			return ret; //<----------- už?? nechybí něco?
		}

		public void Update(ref int activePlayer)
		{
			if (!GameEnd)
			{ 
				if (!Players.ElementAt(activePlayer).Alive)
					activePlayer = (activePlayer + 1) % Players.Count;


				foreach (Player p in Players)
				{
					p.Update();
				}
				foreach (Enemy enemy in Enemies)
					enemy.Update();
			}
		}
		bool LocationAccesable(Location loc)
		{ return LocationAccesable(loc.x, loc.y); }
		bool LocationAccesable(double x, double y)
		{
			int X = (int)x / oneTileWidth;
			int Y = (int)y / oneTileHeight;
			switch (map[X, Y])
			{
				case null:
					return true;
				case var ig when (ig is Killer):
				case var ig2 when (ig2 is FullWall):
					return false;
			}
			throw new Exception("Unknown map tile detected next to given object.");
		}

		public bool[] ObjectPossibleDirections(IGameObject obj)
		{
			double left = obj.Location.x - obj.Width/ 2;
			double top = obj.Location.y - obj.Height / 2;
			bool[] ret = new bool[4] { true, true, true, true };
			for(double i = 0; i <= obj.Width; i += oneTileWidth)
			{
				if(!LocationAccesable(top, left + i))
				{
					ret[(int)Directions.up] = false;
					break;
				}
			}
			for (double i = 0; i <= obj.Width; i += oneTileWidth)
			{
				if (!LocationAccesable(top + obj.Height, left + i))
				{
					ret[(int)Directions.down] = false;
					break;
				}
			}
			for (double i = 0; i <= obj.Height; i += oneTileHeight)
			{
				if (!LocationAccesable(top + i, left))
				{
					ret[(int)Directions.left] = false;
					break;
				}
			}
			for (double i = 0; i <= obj.Height; i += oneTileHeight)
			{
				if (!LocationAccesable(top + i, left + obj.Width))
				{
					ret[(int)Directions.right] = false;
					break;
				}
			}
			return ret;
		}

		public Map(IMapTile[,] map, List<Enemy> enemies, List<Player> players,
			List<IInteractiveItem> otherItems, int tileHeight, int tileWidth)
		{
			this.Players = players;
			this.Enemies = enemies;
			this.OtherItems = otherItems;
			this.map = map;
			this.Height = map.GetLength(1);
			this.Width = map.GetLength(0);
			this.oneTileHeight = tileHeight;
			this.oneTileWidth = tileWidth;
		}
	}

	public interface IMapTile {
		System.Drawing.Color Color { get; }
	}
}
