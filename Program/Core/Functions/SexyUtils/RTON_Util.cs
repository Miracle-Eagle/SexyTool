using SexyTool.Program.Core.Functions.SexyCryptors;
using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Parser;

namespace SexyTool.Program.Core.Functions.SexyUtils
{
/// <summary> Initializes Parsing + Ciphering functions for RTON Files. </summary>

public static class RTON_Util
{
/** <summary> Sets a Value which Contains Info about the Reference Strings usage. </summary>
<returns> The Reference Strings Usage. </returns> */

private static readonly bool useRefStrings = Program.userParams.SexyParsersParams.RtonParseInfo.UseReferenceStrings;

/** <summary> Encodes a JSON FileSystem as a RTON FileSystem, and then, Encrypts it by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path to the FileSystem to be Processed. </param>
<param name = "outputPath"> The Location where the Resulting FileSystem will be Saved. </param> */

public static void EncodeAndEncryptFileSystem(string inputPath, string outputPath)
{
Endian endianOrder = Program.userParams.SexyParsersParams.EndianEncoding;
RTON_Parser.EncodeFileSystem(inputPath, outputPath, endianOrder, useRefStrings);

RTON_Cryptor.EncryptFileSystem(outputPath, outputPath);
}

/** <summary> Decrypts a RTON FileSystem by using Rijndael Ciphering, and then, Decodes it as a JSON FileSystem. </summary>

<param name = "inputPath"> The Path to the FileSystem to be Processed. </param>
<param name = "outputPath"> The Location where the Resulting FileSystem will be Saved. </param> */

public static void DecryptAndDecodeFileSystem(string inputPath, string outputPath)
{
RTON_Cryptor.DecryptFileSystem(inputPath, outputPath);
RTON_Parser.DecodeFileSystem(outputPath, outputPath, useRefStrings);
}

}

}