using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public interface INotLife: IGameObject
	{
	}
	public interface IInteractiveItem : INotLife
	{
		void Interact(PlayerCharacter interactingPlayer);
	}

	public interface INoninteractiveItem : INotLife
	{
		void Update();
	}
}
