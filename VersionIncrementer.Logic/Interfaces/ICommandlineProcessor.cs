using VersionIncrementer.Logic.Enums;

namespace VersionIncrementer.Logic.Interfaces;

public interface ICommandlineProcessor {
	IncrementType ProcessArgs(string[] args);
}
