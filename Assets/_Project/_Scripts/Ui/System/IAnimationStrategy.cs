using JetBrains.Annotations;

namespace Smash.Ui.System
{
	public interface IAnimationStrategy<T>
	{
		void Activate(T obj);
		void Deactivate(T obj);
	}
}
