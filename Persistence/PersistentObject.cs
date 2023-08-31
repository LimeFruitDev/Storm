using System;
using System.Threading.Tasks;
using Sandbox;

namespace Storm;

public class PersistentObject<T, SaveRpc> : IPersistent
	where T : class
	where SaveRpc : BaseRpc
{
	private readonly WeakReference<T> _object;
	private bool _disabled;

	public PersistentObject(T @object, bool disabled = false)
	{
		_object = new WeakReference<T>(@object);

		PersistenceService.PersistentObjects.Add(this);
		_disabled = disabled;
	}

	public void Disable() => _disabled = true;
	public void Enable() => _disabled = false;

	public void MarkDirty()
	{
		Game.AssertServer();

		if (!_disabled)
			PersistenceService.SaveSingle(this);
	}

	public void Save()
	{
		Game.AssertServer();

		if (_disabled)
			return;

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