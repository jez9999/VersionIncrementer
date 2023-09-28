namespace VersionIncrementer.Logic.Interfaces;

public interface IVersionNumberParser {
	VersionNumber Parse(string versionString);
}
