using NUnit.Framework;
using FluentAssertions;
using VersionIncrementer.Logic;

namespace VersionIncrementer.Tests;

[TestFixture]
internal class VersionNumberParserTests {
	[Test]
	public void Valid_version_string_ok() {
		// Arrange
		var parser = new VersionNumberParser();

		// Act
		var verNbr = parser.Parse("1.2.3.4");

		// Assert
		$"{verNbr}".Should().Be("1.2.3.4");
	}

	[Test]
	public void Invalid_version_string_throws() {
		// Arrange
		var parser = new VersionNumberParser();

		// Assert
		parser.Invoking(x => x.Parse("1,2,3,4"))
			.Should().Throw<Exception>().WithMessage("Invalid version string: must have 4 number segments.");
		parser.Invoking(x => x.Parse("1.2.-3.4"))
			.Should().Throw<Exception>().WithMessage("Invalid version string: segment 3 is not a non-negative number.");
		parser.Invoking(x => x.Parse("1.2.3.x"))
			.Should().Throw<Exception>().WithMessage("Invalid version string: segment 4 is not a non-negative number.");
	}
}
