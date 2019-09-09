using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus1
{
	interface UiDoThis
	{
		void Do(Action d);
		void Do<T>(Func<T> f);
	}

	interface ToDo
	{
		void RunAll();
		Action What { get; }
		bool Any { get; }
	}

	class DelegatesQueue : UiDoThis, ToDo
	{
		Queue<Action> delegates = new Queue<Action>();
		public void Do(Action d) => delegates.Enqueue(d);
		public void Do<T>(Func<T> f) => delegates.Enqueue(new NonReturning<T>(f).Run);
		public Action What => delegates.Dequeue();
		public bool Any => delegates.Any();
		public void RunAll()
		{
			while (delegates.Any())
			{
				delegates.Dequeue()();
			}
		}
		class NonReturning<T>
		{
			public NonReturning(Func<T> f)
			{
				this.f = f;
			}
			Func<T> f;
			public void Run() => f();
		}
	}
}
