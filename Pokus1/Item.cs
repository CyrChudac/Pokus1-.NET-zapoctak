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
		public Item(Size size, IAnimation animation)
		{
			Size = size;
			this.Animation = animation;
		}
		public Item(int height, int width, IAnimation animation) : this(new Size(width, height), animation)
		{
			
		}
		public Size Size { get; protected set; }
		public int Height => Size.Height;
		public int Width => Size.Width;
		public abstract IInteractiveItem Copy();
		public Location Location {get; protected set; }
		public string Name { get; protected set; }
		public IAnimation Animation { get; }
		             
		public void Interact(PlayerCharacter interactingPLayer)
		{
			PickUp(interactingPLayer);
		}
		protected void PickUp(PlayerCharacter interactingPlayer)
		{
			interactingPlayer.Items.Add(this);
		}
	}
}
