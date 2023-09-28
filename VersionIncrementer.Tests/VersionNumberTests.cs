using NUnit.Framework;
using FluentAssertions;
using VersionIncrementer.Logic;

namespace VersionIncrementer.Tests;

[TestFixture]
internal class VersionNumberTests {
	[Test]
	public void Default_version_number_ok() {
		// Arrange
		VersionNumber verNbr = new();

		// Assert
		verNbr.First.Should().Be(0);
		verNbr.Second.Should().Be(0);
		verNbr.Third.Should().Be(0);
		verNbr.Fourth.Should().Be(0);
		$"{verNbr}".Should().Be("0.0.0.0");
	}

	[Test]
	public void Given_version_number_ok() {
		// Arrange
		VersionNumber verNbr = new(5, 39, 8, 7);

		// Assert
		verNbr.First.Should().Be(5);
		verNbr.Second.Should().Be(39);
		verNbr.Third.Should().Be(8);
		verNbr.Fourth.Should().Be(7);
		$"{verNbr}".Should().Be("5.39.8.7");
	}

	[Test]
	public void Change_version_number_ok() {
		// Arrange
		VersionNumber verNbr = new(5, 39, 8, 7);

		// Act
		verNbr.First = 1;
		verNbr.Second = 2;
		verNbr.Third = 3;
		verNbr.Fourth = 4;

		// Assert
		verNbr.First.Should().Be(1);
		verNbr.Second.Should().Be(2);
		verNbr.Third.Should().Be(3);
		verNbr.Fourth.Should().Be(4);
		$"{verNbr}".Should().Be("1.2.3.4");
	}
}
