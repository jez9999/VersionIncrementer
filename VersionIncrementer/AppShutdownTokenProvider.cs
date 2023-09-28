namespace VersionIncrementer;

internal class AppShutdownTokenProvider {
	private readonly CancellationTokenSource _tokenSource;
	private readonly CancellationToken _token;

	public AppShutdownTokenProvider() {
		_tokenSource = new();
		_token = _tokenSource.Token;
	}

	public CancellationTokenSource TokenSource => _tokenSource;
	public CancellationToken Token => _token;

	public void DoShutdown() => TokenSource.Cancel();
}
