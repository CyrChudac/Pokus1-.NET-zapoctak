using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CoreLib;
using System.IO;
using Newtonsoft.Json;

namespace Pokus1
{
	interface IMapBuilder
	{
		Map GetMap();
	}

	class DefaultMap
	{
		readonly int width;
		readonly int height;
		public DefaultMap(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		IMapTile[,] OnlyBorders_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) || (i == width - 1) || (j == 0) || (j == height - 1))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}

		IMapTile[,] BordersAndSomething_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) || (i == width - 1) || (j == 0) || (j == height - 1) || ((j == 10) && (i % 7 == 1)))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}
		IMapTile[,] BordersAndSomething2_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) ||
						(i == width - 1) ||
						(j == 0) ||
						(j == height - 1) ||
						((j == 10) && (i < 11)) ||
						(i == 15)
						)
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}
		public class JustExistWithPhysics : IMapBuilder
		{
			DefaultMap builder;
			public JustExistWithPhysics(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Map GetMap()
			{
				IMapTile[,] field = builder.OnlyBorders_Wall();
				Map result = new Map(field);
				result.Players.Add(new Unskilled(100, 100, NoMovement.instance,
				"Vlad", new Location{ x = 143, y = 59}, new SingleColorAnimation(Color.GreenYellow),
				Life.DefaultSize, result));
				return result;
			}
		}
		public class AlsoMove : IMapBuilder
		{
			DefaultMap builder;
			public AlsoMove(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Map GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething_Wall();
				Map result = new Map(field);
				result.Players.Add(new Jumper(100, 100, new UsualMovement(Life.defaultSpeed),
				"Vlad", new Location{ x = 143, y = 59}, new SingleColorAnimation(Color.GreenYellow),
				Life.DefaultSize, result));
				result.Players.Add(new Jumper(100, 100, new UsualMovement(Life.defaultSpeed),
				"Vlad", new Location{ x = 300, y = 59}, new SingleColorAnimation(Color.OrangeRed),
				Life.DefaultSize, result));
				return result;
			}
		}
		public class WithKnifeThrower : IMapBuilder
		{
			DefaultMap builder;
			public WithKnifeThrower(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Map GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething2_Wall();
				Map result = new Map(field);
				result.Players.Add(new Jumper(100, 100, new UsualMovement(Life.defaultSpeed),
					"Vlad", new Location { x = 143, y = 59 }, new SingleColorAnimation(Color.GreenYellow),
					Life.DefaultSize, result));
				result.Players.Add(new KnifeThrower(100, 100, new UsualMovement(Life.defaultSpeed),
					"Vlad2", new Location{ x = 300, y = 59}, new SingleColorAnimation(Color.OrangeRed),
					Life.DefaultSize, result));
				return result;
			}
		}
		public class WithPassiveEnemy : IMapBuilder
		{
			DefaultMap builder;
			public WithPassiveEnemy(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Map GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething2_Wall();
				Map result = new Map(field);
				result.Players.Add(new KnifeThrower(100, 100, new UsualMovement(Life.defaultSpeed),
					"Vlad", new Location { x = 300, y = 59 }, new SingleColorAnimation(Color.GreenYellow),
					Life.DefaultSize, result));
				result.Enemies.Add(new PassiveEnemy(new Location { x = 100, y = 30 }, 1, result));
				return result;
			}
		}
	}

	//class MapReader : IMapBuilder
	//{
	//	readonly string path;
	//	public MapReader(string path)
	//	{
	//		this.path = path;
	//	}
	//	public Map GetMap()
	//	{
	//		StreamReader reader = new StreamReader(path);

	//		IMapTile[,] tiles = ReadTiles(reader);

	//		int tileHeight = ReadNextInt(reader);
	//		int tileWidth = ReadNextInt(reader);

	//		List<Player> characters = ReadCharacters(reader);

	//		List<Enemy> enemies = ReadEnemies(reader);

	//		List<IInteractiveItem> iItems = ReadInteraItems(reader);

	//		List<INoninteractiveItem> nItems = ReadNoninteraItems(reader);

	//		return new Map(tiles, enemies, characters, iItems, nItems, tileHeight, tileWidth);
	//	}

	//	IMapTile[,] ReadTiles(TextReader reader)
	//	{
	//		int height = ReadNextInt(reader);
	//		int width = ReadNextInt(reader);

	//		IMapTile[,] tiles = new IMapTile[width, height];

	//		for (int i = 0; i < height; i++)
	//			for (int j = 0; j < width; j++)
	//				tiles[i, j] = TileBase.Get((TileType)ReadNextInt(reader));
	//		return tiles;
	//	}

	//	List<Player> ReadCharacters(TextReader reader)
	//	{
	//		int charsCount = ReadNextInt(reader);
	//		List<Player> characters = new List<Player>(charsCount);
	//		for (int i = 0; i < charsCount; i++)
	//		{
	//			ReadNextString(reader); // <----- něco jako "name:"
	//			string name = ReadNextString(reader);
	//			ReadNextString(reader); // <----- něco jako "maxHealth:"
	//			int maxHealth = ReadNextInt(reader);	//	.
	//			ReadNextString(reader);					//	.
	//			int currHealth = ReadNextInt(reader);	//	.
	//			SkillType skill = (SkillType)ReadNextInt(reader);
	//			ReadNextString(reader);
	//			Location location = new Location { x = ReadNextInt(reader), y = ReadNextInt(reader) };
	//			ReadNextString(reader);
	//			int baseSpeed = ReadNextInt(reader);

	//			//characters.Add(new Player(skill, maxHealth, currHealth, new UsualMovement(baseSpeed),
	//			//	name, location, new Animation(Time.TimeFlow, name), Location.DefaultLifeSize));
	//		}
	//		return characters;
	//	}

	//	List<Enemy> ReadEnemies(TextReader reader) {
	//		int enemiesCount = ReadNextInt(reader);
	//		List<Enemy> enemies = new List<Enemy>(enemiesCount);
	//		for (int i = 0; i < enemiesCount; i++)
	//		{
	//			ReadNextString(reader); // <----- něco jako "type:"
	//			EnemyType type = (EnemyType)ReadNextInt(reader);
	//			ReadNextString(reader); // <----- něco jako "maxHealth:"
	//			int maxHealth = ReadNextInt(reader);    //	.
	//			ReadNextString(reader);                 //	.
	//			int currHealth = ReadNextInt(reader);   //	.
	//			Location location = new Location { x = ReadNextInt(reader), y = ReadNextInt(reader) };
	//			//ReadNextString(reader);
	//			//int baseSpeed = ReadNextInt(reader);  <---- zatim neni pohyb, tak neni ani baseSpeed

	//			enemies.Add(new NormalEnemy(maxHealth, currHealth, NoMovement.instance, type,
	//				location, new Animation(Time.TimeFlow, type.ToString()), Location.DefaultLifeSize, i));
	//		}
	//		return enemies;
	//	}

	//	List<IInteractiveItem> ReadInteraItems(TextReader reader)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	List<INoninteractiveItem> ReadNoninteraItems(TextReader reader)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	int lastLetter = (int)' ';
	//	string ReadNextString(TextReader reader)
	//	{
	//		int letter = lastLetter;
	//		while (char.IsWhiteSpace((char)letter)){ letter = reader.Read(); }
	//		StringBuilder sb = new StringBuilder();
	//		while (!char.IsWhiteSpace((char)letter)){
	//			sb.Append((char)letter);
	//		}
	//		lastLetter = letter;

	//		if (sb[0] == '.')  // <----------- pro komentare zacni slovo teckou
	//			return ReadNextString(reader);
	//		return sb.ToString();
	//	}
	//	int ReadNextInt(TextReader reader)
	//	{
	//		return int.Parse(ReadNextString(reader));
	//	}
	//}

	interface IMapDesearilizer : IMapBuilder {}

	class MapDeserializer : IMapDesearilizer, IDisposable
	{
		readonly Stream stream;
		public MapDeserializer(Stream stream)
		{
			this.stream = stream;
		}
		public void Dispose() => stream.Dispose();
		public Map GetMap()
		{
			return GetMap(Json.DefaultSerializer);
		}
		public Map GetMap(JsonSerializer js)
		{
			StreamReader sr = new StreamReader(stream);
			Map result = (Map)js.Deserialize(sr, typeof(Map));
			foreach (Player p in result.Players)
				p.Map = result;
			foreach (Enemy e in result.Enemies)
				e.Map = result;
			return result;
			//DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(Map));
			//return (Map)jsonSer.ReadObject(stream);
		}
	}
}
