using System.Threading.Tasks;

namespace Storm;

public interface IPersistent
{
	public void Save();
}