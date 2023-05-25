using System;
using System.Threading.Tasks;
using Sandbox;

namespace Storm;

public class PersistentObject<T, CommitRpc, SaveRpc> : IPersistent
	where T : class
	where CommitRpc : BaseRpc
	where SaveRpc : BaseRpc
{
	private readonly WeakReference<T> _object;
	private bool _wasCommitted;

	public PersistentObject(T @object)
	{
		_wasCommitted = false;
		_object = new WeakReference<T>(@object);

		PersistenceService.PersistentObjects.Add(this);
	}

	public async Task Save()
	{
		Game.AssertServer();

		if (!_object.TryGetTarget(out var @object))
		{
			PersistenceService.PersistentObjects.Remove(this);
			return;
		}

		if (!_wasCommitted) await Commit(@object);

		var saveRpc = TypeLibrary.GetType<SaveRpc>().Create<SaveRpc>(new object[]
		{
			@object
		});
		ApiService.SendMessage(saveRpc);
	}

	private async Task Commit(T @object)
	{
		Game.AssertServer();

		var commitRpc = TypeLibrary.GetType<CommitRpc>().Create<CommitRpc>(new object[]
		{
			@object
		});
		ApiService.SendMessage(commitRpc, (_, _) => { _wasCommitted = true; });

		while (!_wasCommitted)
			await GameTask.Delay(200);
	}
}