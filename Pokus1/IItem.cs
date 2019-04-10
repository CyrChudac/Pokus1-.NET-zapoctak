using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public abstract class Item : IInteractiveItem
	{
		public Item(int height, int width)
		{
			Height = height;
			Width = width;
		}
		public int Height { get; }
		public int Width { get; }
		public abstract IInteractiveItem Copy();
		public Location Location {get; protected set; }
		public string Name { get; protected set; }
		public Color Color { get; protected set; }
		public void Interact(Player interactingPLayer)
		{
			PickUp(interactingPLayer);
		}
		protected void PickUp(Player interactingPlayer)
		{
			interactingPlayer.Items.Add(this);
		}
	}
}
