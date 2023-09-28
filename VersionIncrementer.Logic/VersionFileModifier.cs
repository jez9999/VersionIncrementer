using System.Linq;
using VersionIncrementer.Logic.Interfaces;

namespace VersionIncrementer.Logic;

public class VersionFileModifier : IVersionFileModifier {
	// Note: this is not unit tested because it hits the actual file system
	public string ReadVersion(string filepath) {
		return File.ReadLines(filepath).First();
	}

	public void WriteVersion(string filepath, string version) {
		File.WriteAllText(filepath, version);
	}
}
