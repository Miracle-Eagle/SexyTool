using System;

namespace SexyTool.Program
{
/// <summary> The Language of this Program. </summary>

[Flags]

public enum Language : ushort
{
/// <summary> English (from United States). </summary>
English,

/// <summary> Simplified Chinese (from China). </summary>
中文 = 16191,

/// <summary> German (from Deutschland). </summary>
Deutsch = 25924,

/// <summary> Portuguese (from Brazil). </summary>
Português = 28496,

/// <summary> French (from France). </summary>
Français = 29254,

/// <summary> Spanish (from Spain). </summary>
Español = 29509,

/// <summary> Italian (from Itally). </summary>
Italiano = 29769
}

}