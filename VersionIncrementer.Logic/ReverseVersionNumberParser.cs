using VersionIncrementer.Logic.Interfaces;

namespace VersionIncrementer.Logic;

/// <summary>
/// Parses version number strings with segments in reverse.
/// </summary>
public class VersionNumberParser : IVersionNumberParser {
	public VersionNumber Parse(string versionString) {
		var segments = versionString.Split('.');
		if (segments.Length != 4) {
			throw new Exception("Invalid version string: must have 4 number segments.");
		}

		uint[] segmentInts = new uint[4];
		for (int i = 0; i < 4; i++) {
			if (!uint.TryParse(segments[i], out uint segmentInt)) {
				throw new Exception($"Invalid version string: segment {i + 1} is not a non-negative number.");
			}
			segmentInts[i] = segmentInt;
		}

		return new VersionNumber(segmentInts[0], segmentInts[1], segmentInts[2], segmentInts[3]);
	}
}
