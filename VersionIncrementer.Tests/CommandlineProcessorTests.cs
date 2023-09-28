using NUnit.Framework;
using FluentAssertions;
using VersionIncrementer.Logic;

namespace VersionIncrementer.Tests;

[TestFixture]
internal class CommandlineProcessorTests {
	[Test]
	public void Valid_commandline_args_ok() {
		// Arrange
		var processor = new CommandlineProcessor();

		// Assert
		processor.ProcessArgs(new string[] { "feature" })
			.Should().Be(Logic.Enums.IncrementType.Feature);
		processor.ProcessArgs(new string[] { "bugfix" })
			.Should().Be(Logic.Enums.IncrementType.Bugfix);
	}

	[Test]
	public void Extra_commandline_args_ok() {
		// Arrange
		var processor = new CommandlineProcessor();

		// Assert
		processor.ProcessArgs(new string[] { "feature", "xyz" })
			.Should().Be(Logic.Enums.IncrementType.Feature);
		processor.ProcessArgs(new string[] { "abc", "bugfix" })
			.Should().Be(Logic.Enums.IncrementType.Bugfix);
	}

	[Test]
	public void No_commandline_args_invalid() {
		// Arrange
		var processor = new CommandlineProcessor();

		// Assert
		processor.ProcessArgs(Array.Empty<string>())
			.Should().Be(Logic.Enums.IncrementType.None);
	}

	[Test]
	public void Combined_commandline_args_invalid() {
		// Arrange
		var processor = new CommandlineProcessor();

		// Assert
		processor.ProcessArgs(new string[] { "feature", "bugfix" })
			.Should().Be(Logic.Enums.IncrementType.None);
	}
}
