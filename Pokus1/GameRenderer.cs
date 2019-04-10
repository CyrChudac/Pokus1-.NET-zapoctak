using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pokus1
{
	interface IMapRenderer
	{
		void FirstRender(ref Map map);
		void Render();
	}
	public partial class GameControl : IMapRenderer
	{
		Map map;
		Label[,] tiles;
		List<PictureBox> players;
		List<PictureBox> enemies;
		List<Label> items;

		public virtual void FirstRender(ref Map map)
		{
			this.map = map;
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
					tiles[i, j].BackColor = map[i, j].Color;
				}
		}

		void RenderPlayers()
		{
			for (int i = 0; i < players.Count; i++)
			{
				players[i].Image = map.Players[i].Animation.Image;
				players[i].Location =
					new System.Drawing.Point(map.Players[i].Location.x, map.Players[i].Location.y);
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
	}
}
