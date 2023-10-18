using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Parser;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>SexyParsers</c> of this Program. </summary>

internal class SexyParsers : Framework
{
/// <summary> Creates a new Instance of the <c>SexyParsers</c>. </summary>

public SexyParsers()
{
ID = 7;
DisplayName = "Text.LocalizedData.FRAMEWORK_SEXY_PARSERS";

FunctionsList = new()
{
// Function A - RTON Parser: Encode FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = "Text.LocalizedData.FUNCTION_ENCODE_FILESYSTEM_RTON",
Process = () => RTON_Parser.EncodeFileSystem(Program.userParams.SexyParsersParams.InputPath, Program.userParams.SexyParsersParams.OutputPath, Program.userParams.SexyParsersParams.EndianEncoding, Program.userParams.SexyParsersParams.RtonParseInfo.UseReferenceStrings)
},

// Function B - RTON Parser: Decode FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = "Text.LocalizedData.FUNCTION_DECODE_FILESYSTEM_RTON",
Process = () => RTON_Parser.DecodeFileSystem(Program.userParams.SexyParsersParams.InputPath, Program.userParams.SexyParsersParams.OutputPath, Program.userParams.SexyParsersParams.RtonParseInfo.UseReferenceStrings)
},

// Continue adding more functions here...

};

}

}

}