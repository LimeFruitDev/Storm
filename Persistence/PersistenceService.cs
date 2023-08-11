using System.Collections.Generic;
using Sandbox;

namespace Storm;

public static class PersistenceService
{
#pragma warning disable CA2211
	public static List<IPersistent> PersistentObjects = new();
#pragma warning restore CA2211

	public static async void SaveAll()
	{
		await GameTask.RunInThreadAsync(SaveAllTask);
	}

	private static void SaveAllTask()
	{
		foreach (var persistentObject in PersistentObjects) 
			persistentObject.Save();
	}
}