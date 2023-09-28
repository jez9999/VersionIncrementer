namespace VersionIncrementer;

internal class ArgsProvider {
	public string[] Args { get; init; }

	internal ArgsProvider(string[] args) {
		Args = args;
	}
}
