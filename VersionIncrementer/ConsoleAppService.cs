using Microsoft.Extensions.Hosting;
using VersionIncrementer.Logic;
using VersionIncrementer.Logic.Enums;
using VersionIncrementer.Logic.Interfaces;

namespace VersionIncrementer;

internal class ConsoleAppService : BackgroundService {
	private readonly AppShutdownTokenProvider _shutdownTokenProvider;
	private readonly string[] _args;
	private readonly ICommandlineProcessor _processor;
	private readonly IVersionNumberParser _parser;
	private readonly IVersionFileModifier _file;

	#region Constructors

	public ConsoleAppService(AppShutdownTokenProvider shutdownTokenProvider, ArgsProvider argsProvider, ICommandlineProcessor processor, IVersionNumberParser parser, IVersionFileModifier file) {
		_shutdownTokenProvider = shutdownTokenProvider;
		_args = argsProvider.Args;
		_processor = processor;
		_parser = parser;
		_file = file;
	}

	#endregion

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		await Task.Yield();
		doWork();
		_shutdownTokenProvider.DoShutdown();
	}

	private void doWork() {
		// Assuming fixed location of version file for now
		var versionFileLocation = "C:\\Development\\Temp\\ProductInfo.cs";
		Console.WriteLine($"Incrementing version number in file at: {versionFileLocation}");

		var incrementType = _processor.ProcessArgs(_args);
		if (incrementType == Logic.Enums.IncrementType.None) {
			Console.WriteLine("Either a 'feature' or 'bugfix' argument must be specified (not both) to increment the version number.");
			return;
		}

		string versionString;
		try {
			versionString = _file.ReadVersion(versionFileLocation);
		}
		catch (Exception ex) {
			Console.WriteLine($"Couldn't read version file: {ex.Message}");
			return;
		}

		VersionNumber version;
		try {
			version = _parser.Parse(versionString);
		}
		catch (Exception ex) {
			Console.WriteLine($"Couldn't parse version string: {ex.Message}");
			return;
		}

		incrementVersion(incrementType, version);

		try {
			_file.WriteVersion(versionFileLocation, $"{version}");
		}
		catch (Exception ex) {
			Console.WriteLine($"Couldn't write to version file: {ex.Message}");
			return;
		}

		Console.WriteLine($"Version file successfully incremented for: {incrementType}.");
	}

	private static void incrementVersion(IncrementType incrementType, VersionNumber version) {
		// TODO: this logic could be moved into VersionNumber (ie. version.Increment(IncrementType.Feature))
		switch (incrementType) {
			case IncrementType.Feature:
				version.Third++;
				version.Fourth = 0;
				break;

			case IncrementType.Bugfix:
				version.Fourth++;
				break;
		}
	}
}
