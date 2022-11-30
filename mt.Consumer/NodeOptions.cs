using System.Diagnostics;

namespace mt.Consumer;

public class NodeOptions
{
    public string NodeId { get; }

	public NodeOptions()
	{
		NodeId = Process.GetCurrentProcess().Id.ToString();
	}
}
