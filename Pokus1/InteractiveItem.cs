using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public interface IItem: INotLife
	{
		string Name { get; }
		IAnimation Animation { get; }
	}
	public interface IInteractiveItem : IItem
	{
		void Interact(PlayerCharacter interactingPlayer);
	}

	public interface INoninteractiveItem : IItem
	{
		void Update();
	}
}
