using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public interface IInteractiveItem : INotLife
	{
		void Interact(Player interactingPlayer);
		IInteractiveItem Copy();
		string Name { get; }
		System.Drawing.Brush Brush { get; }
		System.Drawing.Size Size { get; }
	}
}
