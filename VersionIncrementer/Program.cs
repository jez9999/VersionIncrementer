using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using VersionIncrementer.Logic.Interfaces;
using VersionIncrementer.Logic;

namespace VersionIncrementer;

internal class Program {
	public static async Task Main(string[] args) {
		var tokenProvider = new AppShutdownTokenProvider();
		var host = createHost(args, tokenProvider);

		await host.StartAsync();
		await host.WaitForShutdownAsync(tokenProvider.Token);
	}

	private static IHost createHost(string[] args, AppShutdownTokenProvider shutdownToken) {
		// Create host for console app to allow built-in DI
		return Host
			.CreateDefaultBuilder()

			.ConfigureLogging((hostContext, builder) => {
				builder.ClearProviders();
			})

			.ConfigureServices((hostContext, services) => {
				services.AddSingleton(_ => shutdownToken);
				services.AddSingleton(_ => new ArgsProvider(args));
				services.AddTransient<ICommandlineProcessor, CommandlineProcessor>();
				services.AddTransient<IVersionNumberParser, VersionNumberParser>();
				//services.AddTransient<IVersionNumberParser, ReverseVersionNumberParser>();
				services.AddTransient<IVersionFileModifier, VersionFileModifier>();

				services.AddHostedService<ConsoleAppService>();
			})

			.Build();
	}
}
