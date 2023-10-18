using SexyTool.Program.Core.Functions.SexyUtils;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>SexyUtils</c> of this Program. </summary>

internal class SexyUtils : Framework
{
/// <summary> Creates a new Instance of the <c>SexyUtils</c>. </summary>

public SexyUtils()
{
ID = 98;
DisplayName = "Text.LocalizedData.FRAMEWORK_SEXY_UTILS";

FunctionsList = new()
{
// Function A - Encode & Encrypt RTON FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = "Text.LocalizedData.FUNCTION_ENCODE_AND_ENCRYPT_RTON_FILESYSTEM",
Process = () => RTON_Util.EncodeAndEncryptFileSystem(Program.userParams.SexyUtilsParams.InputPath, Program.userParams.SexyUtilsParams.OutputPath)
},

// Function B - Decrypt & Decode RTON FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = "Text.LocalizedData.FUNCTION_DECRYPT_AND_DECODE_RTON_FILESYSTEM",
Process = () => RTON_Util.DecryptAndDecodeFileSystem(Program.userParams.SexyUtilsParams.InputPath, Program.userParams.SexyUtilsParams.OutputPath)
},

// Continue adding more functions here...

};

}

}

}