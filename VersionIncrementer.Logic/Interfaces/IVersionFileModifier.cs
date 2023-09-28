namespace VersionIncrementer.Logic.Interfaces;

public interface IVersionFileModifier {
	string ReadVersion(string filepath);
	void WriteVersion(string filepath, string version);
}
