using System.Linq;
using VersionIncrementer.Logic.Interfaces;
using VersionIncrementer.Logic.Enums;

namespace VersionIncrementer.Logic;

/// <summary>
/// Interprets/processes commandline arguments.
/// </summary>
public class CommandlineProcessor : ICommandlineProcessor {
	public IncrementType ProcessArgs(string[] args) {
		if (args.Length < 1) {
			return IncrementType.None;
		}

		if (args.Any(x => x.ToLower() == "feature")) {
			if (args.Any(x => x.ToLower() == "bugfix")) {
				return IncrementType.None;
			}
			return IncrementType.Feature;
		}
		if (args.Any(x => x.ToLower() == "bugfix")) {
			return IncrementType.Bugfix;
		}

		return IncrementType.None;
	}
}
