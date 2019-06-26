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
		public Item(Size size, Color color)
		{
			Size = size;
			Color = color;
		}
		public Item(int height, int width, Color color) : this(new Size(width, height), color)
		{
			
		}
		public Size Size { get; protected set; }
		public int Height => Size.Height;
		public int Width => Size.Width;
		public abstract IInteractiveItem Copy();
		public Location Location {get; protected set; }
		public string Name { get; protected set; }
		Color _color;
		public Color Color { get => _color;
			private set
			{
				Brush = new SolidBrush(value);
				_color = value;
			}
		}
		public Brush Brush { get; private set; }
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
