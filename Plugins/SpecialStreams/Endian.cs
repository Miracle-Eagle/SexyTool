/// <summary> The Endian Encodings used when parsing Binary Data. </summary>

public enum Endian : uint
{
/// <summary> The Data won't use Endian Encoding. </summary>
None = 1,

/// <summary> The Data should be Parsed with Little Endian. </summary>
Little = 2,

/// <summary> The Data should be Parsed with Big Endian. </summary>
Big = 3
}
