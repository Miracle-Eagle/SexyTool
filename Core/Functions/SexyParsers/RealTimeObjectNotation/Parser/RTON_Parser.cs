using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Parser
{
/// <summary> Initializes Encoding and Decoding functions Real Time Object Notation (RTON) Files. </summary>

public class RTON_Parser
{
/** <summary> Sets a Value which Contains Info about the Extension of a Json file. </summary>
<returns> The Json File Extension. </returns> */

private static readonly string jsonFileExt = ".json";

/** <summary> Decodes a RTON File as a JSON File. </summary>

<param name = "inputPath"> The Path where the JSON File to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded RTON File will be Saved. </param>
<param name = "endianOrder" > The endian Encoding of the RTON File. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncodeFile(string inputPath, string outputPath, Endian endianOrder, bool saveRefStrings)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCODE_FILE, Path.GetFileName(inputPath) );
using FileStream inputFile = File.OpenRead(inputPath);

JsonDocumentOptions jsonReadConfig = new()
{
AllowTrailingCommas = true,
CommentHandling = JsonCommentHandling.Skip,
MaxDepth = 64
};

using JsonDocument jsonData = JsonDocument.Parse(inputFile, jsonReadConfig);
Path_Helper.ChangeExtension(ref outputPath, ".rton");

Archive_Manager.CheckDuplicatedPath(ref outputPath);
using BinaryStream outputFile = BinaryStream.OpenWrite(outputPath);

RtonInfo.WriteFileHeader(outputFile);
RtonInfo.WriteVersionInfo(outputFile, endianOrder);

RtObject.Write(outputFile, jsonData.RootElement, endianOrder);
RtonInfo.WriteFileTail(outputFile);

if(saveRefStrings)
ReferenceStringsHandler.WriteStrings(outputPath);

ReferenceStringsHandler.ClearStrings();
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_ENCODE_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_ENCODE_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Encodes all JSON Files from a Folder. </summary>

<param name = "inputPath"> The Path where the Folder to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded Folder will be Saved. </param>
<param name = "endianOrder" > The endian Encoding of the RTON Files. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncodeFolder(string inputPath, string outputPath, Endian endianOrder, bool saveRefStrings)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCODE_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encodeAction = GetEncodeAction(endianOrder, saveRefStrings, false);

Task_Manager.BatchTask(inputPath, outputPath, encodeAction, jsonFileExt);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_ENCODE_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_ENCODE_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Encodes a JSON FileSystem as a RTON FileSystem. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded FileSystem will be Saved. </param>
<param name = "endianOrder" > The endian Encoding of the RTON Files. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param> */

public static void EncodeFileSystem(string inputPath, string outputPath, Endian endianOrder, bool saveRefStrings)
{
var singleAction = GetEncodeAction(endianOrder, saveRefStrings, false);
var batchAction = GetEncodeAction(endianOrder, saveRefStrings, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decodes a RTON File as a JSON File. </summary>

<param name = "inputPath"> The Path where the RTON File to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded JSON File will be Saved. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Loaded or not. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecodeFile(string inputPath, string outputPath, bool loadRefStrings)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECODE_FILE, Path.GetFileName(inputPath) );
using BinaryStream inputFile = BinaryStream.Open(inputPath);

RtonInfo.ReadFileHeader(inputFile);
Endian endianOrder = RtonInfo.ReadVersionInfo(inputFile);

Path_Helper.ChangeExtension(ref outputPath, jsonFileExt);
Archive_Manager.CheckDuplicatedPath(ref outputPath);

using FileStream outputFile = File.OpenWrite(outputPath);

JsonWriterOptions jsonWriteConfig = new()
{
Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
Indented = true
};

using Utf8JsonWriter jsonWriter = new(outputFile, jsonWriteConfig);

if(loadRefStrings)
ReferenceStringsHandler.ReadStrings(inputPath);

RtObject.Read(inputFile, jsonWriter, endianOrder);
RtonInfo.ReadFileTail(inputFile);

ReferenceStringsHandler.ClearStrings();
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECODE_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECODE_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decodes all RTON Files from a Folder. </summary>

<param name = "inputPath"> The Path where the Folder to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded Folder will be Saved. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecodeFolder(string inputPath, string outputPath, bool saveRefStrings)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECODE_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decodeAction = GetDecodeAction(saveRefStrings, false);

Task_Manager.BatchTask(inputPath, outputPath, Path_Helper.specificFileNames["RtObject"], Path_Helper.specificFileExtensions["RtObject"], decodeAction);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECODE_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECODE_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decodes a RTON FileSystem as a JSON FileSystem. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded FileSystem will be Saved. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param> */

public static void DecodeFileSystem(string inputPath, string outputPath, bool saveRefStrings)
{
var singleAction = GetDecodeAction(saveRefStrings, false);
var batchAction = GetDecodeAction(saveRefStrings, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encode Action by using the Specified Endian Encoding, RefString Saving and Batch Mode. </summary>

<param name = "endianOrder"> The endian Order of the RTON Data. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encode Action Obtained. </returns> */

private static Action<string, string> GetEncodeAction(Endian endianOrder, bool saveRefStrings, bool isBatchAction)
{
ActionWrapper<string, string> encodeAction;

if(isBatchAction)
encodeAction = new( (inputPath, outputPath) => EncodeFolder(inputPath, outputPath, endianOrder, saveRefStrings) );

else
encodeAction = new( (inputPath, outputPath) => EncodeFile(inputPath, outputPath, endianOrder, saveRefStrings) );

return encodeAction.Init;
}

/** <summary> Gets a Decode Action by using the Specified Batch Mode. </summary>

<param name = "endianOrder"> The endian Order of the RTON Data. </param>
<param name = "saveRefStrings" > A Boolean that Determines if Reference Strings should be Saved or not. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decode Action Obtained. </returns> */

private static Action<string, string> GetDecodeAction(bool saveRefStrings, bool isBatchAction)
{
ActionWrapper<string, string> decodeAction;

if(isBatchAction)
decodeAction = new( (inputPath, outputPath) => DecodeFolder(inputPath, outputPath, saveRefStrings) );

else
decodeAction = new( (inputPath, outputPath) => DecodeFile(inputPath, outputPath, saveRefStrings) );

return decodeAction.Init;
}

}

}