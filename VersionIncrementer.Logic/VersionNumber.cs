namespace VersionIncrementer.Logic;

/// <summary>
/// Represents a version number.
/// </summary>
public class VersionNumber {
	#region Constructors

	/// <summary>
	/// Creates a version number with the default version.
	/// </summary>
	public VersionNumber() {
		First = Second = Third = Fourth = 0;
	}

	/// <summary>
	/// Creates a version number with the given segments.
	/// </summary>
	/// <param name="first">The first version number segment.</param>
	/// <param name="second">The second version number segment.</param>
	/// <param name="third">The third version number segment.</param>
	/// <param name="fourth">The fourth version number segment.</param>
	public VersionNumber(uint first, uint second, uint third, uint fourth) {
		First = first;
		Second = second;
		Third = third;
		Fourth = fourth;
	}

	#endregion

	public uint First { get; set; }
	public uint Second { get; set; }
	public uint Third { get; set; }
	public uint Fourth { get; set; }

	public override string ToString() {
		return $"{First}.{Second}.{Third}.{Fourth}";
	}
}
