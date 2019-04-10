using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;
using System.IO;

namespace Pokus1
{
	interface IMapBuilder
	{
		Map GetMap(string path);
	}

	class DefaultMap : IMapBuilder
	{
		readonly int width;
		readonly int height;
		public DefaultMap(int width, int height)
		{
			this.width = width;
			this.height = height;
		}
		public Map GetMap(string NOTHING)
		{
			IMapTile[,] field = new IMapTile[width, height];
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j< height; j++)
				{
					if ((i == 0) || (i == width - 1) || (j == 0) || (j == height - 1))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			List<Player> players = new List<Player>() { new Player(SkillType.noSkill, 100, 100, NoMovement.instance,
				 "Vlad", new Location{ x = 100, y = 100}) };
			List<Enemy> enemies = null;
			List<IInteractiveItem> otherItems = null;
			return new Map(field, enemies, players, otherItems, 20,20);
		}
	}

	class MapReader : IMapBuilder
	{
		public Map GetMap(string path)
		{
			StreamReader reader = new StreamReader(path);

			IMapTile[,] tiles = ReadTiles(reader);

			int tileHeight = ReadNextInt(reader);
			int tileWidth = ReadNextInt(reader);

			List<Player> characters = ReadCharacters(reader);

			List<Enemy> enemies = ReadEnemies(reader);

			List<IInteractiveItem> items = ReadItems(reader);

			return new Map(tiles, enemies, characters, items, tileHeight, tileWidth);
		}

		IMapTile[,] ReadTiles(TextReader reader)
		{
			int height = ReadNextInt(reader);
			int width = ReadNextInt(reader);

			IMapTile[,] tiles = new IMapTile[width, height];

			for (int i = 0; i < height; i++)
				for (int j = 0; j < width; j++)
					tiles[i, j] = TileBase.Get((TileType)ReadNextInt(reader));
			return tiles;
		}

		List<Player> ReadCharacters(TextReader reader)
		{
			int charsCount = ReadNextInt(reader);
			List<Player> characters = new List<Player>(charsCount);
			for (int i = 0; i < charsCount; i++)
			{
				ReadNextString(reader); // <----- něco jako "name:"
				string name = ReadNextString(reader);
				ReadNextString(reader); // <----- něco jako "maxHealth:"
				int maxHealth = ReadNextInt(reader);	//	.
				ReadNextString(reader);					//	.
				int currHealth = ReadNextInt(reader);	//	.
				SkillType skill = (SkillType)ReadNextInt(reader);
				ReadNextString(reader);
				Location location = new Location { x = ReadNextInt(reader), y = ReadNextInt(reader) };
				ReadNextString(reader);
				int baseSpeed = ReadNextInt(reader);

				characters.Add(new Player(skill, maxHealth, currHealth, 
					new UsualMovement(baseSpeed), name, location));
			}
			return characters;
		}

		List<Enemy> ReadEnemies(TextReader reader) {
			int enemiesCount = ReadNextInt(reader);
			List<Enemy> enemies = new List<Enemy>(enemiesCount);
			for (int i = 0; i < enemiesCount; i++)
			{
				ReadNextString(reader); // <----- něco jako "type:"
				EnemyType type = (EnemyType)ReadNextInt(reader);
				ReadNextString(reader); // <----- něco jako "maxHealth:"
				int maxHealth = ReadNextInt(reader);    //	.
				ReadNextString(reader);                 //	.
				int currHealth = ReadNextInt(reader);   //	.
				Location location = new Location { x = ReadNextInt(reader), y = ReadNextInt(reader) };
				//ReadNextString(reader);
				//int baseSpeed = ReadNextInt(reader);  <---- zatim neni pohyb, tak neni ani baseSpeed

				enemies.Add(new NormalEnemy(maxHealth, currHealth,
					NoMovement.instance, type, location));
			}
			return enemies;
		}

		List<IInteractiveItem> ReadItems(TextReader reader)
		{
			throw new NotImplementedException();
		}

		int lastLetter = (int)' ';
		string ReadNextString(TextReader reader)
		{
			int letter = lastLetter;
			while (char.IsWhiteSpace((char)letter)){ letter = reader.Read(); }
			StringBuilder sb = new StringBuilder();
			while (!char.IsWhiteSpace((char)letter)){
				sb.Append((char)letter);
			}
			lastLetter = letter;

			if (sb[0] == '.')  // <----------- pro komentare zacni slovo teckou
				return ReadNextString(reader);
			return sb.ToString();
		}
		int ReadNextInt(TextReader reader)
		{
			return int.Parse(ReadNextString(reader));
		}
	}
}
