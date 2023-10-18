using SexyTool.Program.Core.Functions.Other;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.ArchiveSecurity
{
/// <summary> Initializes Base64 Encoding and Decoding functions for Files. </summary>

public class Base64_Parser : Base64StringParser
{
/** <summary> Encodes a File by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the File to be Encoded is Located. </param>
<param name = "outputPath"> The Path where the Encoded File will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncodeFile(string inputPath, string outputPath, bool isWebSafe)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCODE_FILE, Path.GetFileName(inputPath) );
byte[] inputFileBytes = File.ReadAllBytes(inputPath);

string encodedString = EncodeBytes(inputFileBytes, isWebSafe);
File.WriteAllText(outputPath, encodedString);
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

/** <summary> Encodes all the Files of a Folder by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the Folder to be Encoded is Located. </param>
<param name = "outputPath"> The Location where the Encoded Folder will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncodeFolder(string inputPath, string outputPath, bool isWebSafe)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCODE_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encodeAction = GetEncodeAction(isWebSafe, false);

Task_Manager.BatchTask(inputPath, outputPath, encodeAction);
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

/** <summary> Encodes a FileSystem by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encoded is Located. </param>
<param name = "outputPath"> The Path where the Encoded FileSystem will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param> */

public static void EncodeFileSystem(string inputPath, string outputPath, bool isWebSafe)
{
var singleAction = GetEncodeAction(isWebSafe, false);
var batchAction = GetEncodeAction(isWebSafe, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decodes a File by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the File to be Decoded is Located. </param>
<param name = "outputPath"> The Path where the Decoded File will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecodeFile(string inputPath, string outputPath, bool isWebSafe)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECODE_FILE, Path.GetFileName(inputPath) );
string inputFileText = File.ReadAllText(inputPath);

byte[] decodedBytes = DecodeString(inputFileText, isWebSafe);
File.WriteAllBytes(outputPath, decodedBytes);
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

/** <summary> Decodes all the Files of a Folder by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the Folder to be Decoded is Located. </param>
<param name = "outputPath"> The Location where the Decoded Folder will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecodeFolder(string inputPath, string outputPath, bool isWebSafe)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECODE_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decodeAction = GetDecodeAction(isWebSafe, false);

Task_Manager.BatchTask(inputPath, outputPath, decodeAction);
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


/** <summary> Decodes a FileSystem by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decoded is Located. </param>
<param name = "outputPath"> The Path where the Decoded FileSystem will be Saved. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param> */

public static void DecodeFileSystem(string inputPath, string outputPath, bool isWebSafe)
{
var singleAction = GetDecodeAction(isWebSafe, false);
var batchAction = GetDecodeAction(isWebSafe, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encode Action by using the Specified Batch Mode. </summary>

<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encode Action Obtained. </returns> */

private static Action<string, string> GetEncodeAction(bool isWebSafe, bool isBatchAction)
{
ActionWrapper<string, string> encodeAction;

if(isBatchAction)
{
encodeAction = new( (inputPath, outputPath) => EncodeFolder(inputPath, outputPath, isWebSafe) );
}

else
{
encodeAction = new( (inputPath, outputPath) => EncodeFile(inputPath, outputPath, isWebSafe) );
}

return encodeAction.Init;
}

/** <summary> Gets an Decode Action by using the Specified Batch Mode. </summary>

<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decode Action Obtained. </returns> */

private static Action<string, string> GetDecodeAction(bool isWebSafe, bool isBatchAction)
{
ActionWrapper<string, string> decodeAction;

if(isBatchAction)
{
decodeAction = new( (inputPath, outputPath) => DecodeFolder(inputPath, outputPath, isWebSafe) );
}

else
{
decodeAction = new( (inputPath, outputPath) => DecodeFile(inputPath, outputPath, isWebSafe) );
}

return decodeAction.Init;
}

}

}