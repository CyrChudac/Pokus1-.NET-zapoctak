using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CoreLib;

namespace Pokus1
{
	interface IMapRenderer
	{
		void FirstRender(Map map);
		void Render();
	}

	public partial class GameControl : IMapRenderer
	{
		Map map;
		Label[,] tiles;
		List<PictureBox> players;
		List<PictureBox> enemies;
		List<Label> items;

		public virtual void FirstRender(Map map)
		{
			this.map = map;
			MakeAll();
			RenderMap();
			Render();
		}
		public virtual void Render()
		{
			RenderPlayers();
			RenderEnemies();
			RenderItems();
		}

		void RenderMap()
		{
			
			for(int i = 0; i < tiles.GetLength(0); i++)
				for(int j = 0; j < tiles.GetLength(1); j++)
				{
					if(map[i,j] != NoTile.Tile)
						tiles[i, j].BackColor = map[i, j].Color;
				}
		}

		void RenderPlayers()
		{
			for (int i = 0; i < players.Count; i++)
			{
				players[i].Image = map.Players[i].Animation.Image;
				players[i].Size = map.Players[i].Size;
				players[i].Location = map.Players[i].Location;
			}
		}

		void RenderEnemies()
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].Image = map.Enemies[i].Animation.Image;
				enemies[i].Location =
					new Point(map.Enemies[i].Location.x, map.Enemies[i].Location.y);
			}
		}

		void RenderItems()
		{
			for (int i = 0; i < items.Count; i++)
			{
				items[i].BackColor = map.OtherItems[i].Color;
				items[i].Location = 
					new System.Drawing.Point(map.OtherItems[i].Location.x, map.OtherItems[i].Location.y);
			}
		}

		#region void MakeAll()...
		void MakeAll()
		{
			MakeTiles();
			MakeEnemies();
			MakeItems();
			MakePlayers();
		}

		void MakeTiles()
		{
			tiles = new Label[map.Height, map.Height];

			int height = map.oneTileHeight;//ClientSize.Height / map.Height;
			int width = map.oneTileWidth;//ClientSize.Width / map.Width;

			for (int i = 0; i < map.Width; i++)
				for (int j = 0; j < map.Height; j++)
				{
					if (map[i, j] != NoTile.Tile)
					{
						Label label = new Label()
						{
							Location = new Point(i * width, j * height),
							Text = "",
							Height = height,
							Width = width,
							Tag = i.ToString() + " " + j.ToString()
						};
						tiles[i, j] = label;
						this.Controls.Add(label);
					}
				}

			Form.Size = new Size(tiles[map.Width - 1, map.Height - 1].Location + tiles[map.Width - 1, map.Height - 1].Size);
			Refresh();
		}

		void MakePlayers()
		{
			players = new List<PictureBox>(map.Players.Count);
			foreach (Player player in map.Players)
			{
				PictureBox pic = new PictureBox();
				PictureBox pic2 = new PictureBox();
				pic.Controls.Add(pic2);
				pic2.Location = player.Location;
				pic2.BackColor = Color.Transparent;
				pic2.BorderStyle = BorderStyle.None;
				pic.BorderStyle = BorderStyle.None;
				pic.SizeMode = PictureBoxSizeMode.AutoSize;
				pic.Tag = player.name;
				players.Add(pic);
			}
		}

		void MakeEnemies()
		{
			enemies = new List<PictureBox>(map.Enemies.Count);
			foreach (Enemy enemy in map.Enemies)
			{
				PictureBox pic = new PictureBox();
				PictureBox pic2 = new PictureBox();
				pic.Controls.Add(pic2);
				pic2.Location = new Point(0, 0);
				pic2.BackColor = Color.Transparent;
				pic2.BorderStyle = BorderStyle.None;
				pic.BorderStyle = BorderStyle.None;
				pic.SizeMode = PictureBoxSizeMode.AutoSize;
				pic.Tag = enemy.Type.ToString();
				players.Add(pic);
			}
		}

		void MakeItems()
		{
			items = new List<Label>(map.OtherItems.Count);
			int width = 10;
			int height = 10;
			foreach (IInteractiveItem item in map.OtherItems)
			{
				Label label = new Label()
				{
					BackColor = item.Color,
					Location = new Point(item.Location.x, item.Location.y),
					Text = "",
					Tag = item.Name,
					Height = height,
					Width = width
				};
				this.Controls.Add(label);
				items.Add(label);
			}
		}
		#endregion
	}
}
