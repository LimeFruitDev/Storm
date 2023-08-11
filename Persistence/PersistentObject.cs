using System;
using System.Threading.Tasks;
using Sandbox;

namespace Storm;

public class PersistentObject<T, SaveRpc> : IPersistent
	where T : class
	where SaveRpc : BaseRpc
{
	private readonly WeakReference<T> _object;

	public PersistentObject(T @object)
	{
		_object = new WeakReference<T>(@object);

		PersistenceService.PersistentObjects.Add(this);
	}

	public void Save()
	{
		Game.AssertServer();

		if (!_object.TryGetTarget(out var @object))
		{
			PersistenceService.PersistentObjects.Remove(this);
			return;
		}

		var saveRpc = TypeLibrary.GetType<SaveRpc>().Create<SaveRpc>(new object[]
		{
			@object
		});
		ApiService.SendMessage(saveRpc);
	}
}