using System;
using System.IO;
using System.IO.Compression;

namespace SexyTool.Program.Core.Functions.SexyCompressors.StandarMediaFile
{
/// <summary> Initializes Compressing and Decompressing Functions for Standar Media Files (SMF). </summary>

public class SMF_Compressor
{
/** <summary> Sets a Value which Contains Info about the Extension of a Standar Media File. </summary>
<returns> The Standar Media File Extension. </returns> */

private static readonly string smfFileExt = ".smf";

/** <summary> Sets a Value which Contains Info about the Size in Bytes of the Buffers used for Reading/Writting Tasks. </summary>
<returns> The Buffer Size. </returns> */

private static readonly long bufferSize = Constants.ONE_KILOBYTE * 4;

/** <summary> Compresses the Data Contained on a Bytes Array by using Deflate Compression. </summary>

<param name = "inputStream"> The Stream which Contains the Data to be Compressed. </param>
<param name = "compressionLvl"> The Compression Level of the File. </param>
<param name = "fileInfo"> Info related to the File. </param>

<returns> The Data Compressed. </returns> */

private static byte[] CompressDataInBlocks(Stream inputStream, CompressionLevel compressionLvl, ref StandarMediaInfo fileInfo)
{
using MemoryStream outputStream = new();
using DeflateStream compressionStream = new(outputStream, compressionLvl);

byte[] bufferData = new byte[bufferSize];
int bytesRead;

while( (bytesRead = inputStream.Read(bufferData) ) != 0)
compressionStream.Write(bufferData, 0, bytesRead);

return Input_Manager.MergeArrays(fileInfo.CompressionFlags, outputStream.ToArray(), fileInfo.Adler32Bytes);
}

/** <summary> Compresses the Contents of a RSB File as a SMF File by using Deflate Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be Used. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFile(string inputPath, string outputPath, CompressionLevel compressionLvl)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_COMPRESS_FILE, Path.GetFileName(inputPath) );
using BinaryStream inputFile = BinaryStream.Open(inputPath);

Path_Helper.AddExtension(ref outputPath, smfFileExt);
Archive_Manager.CheckDuplicatedPath(ref outputPath);

using BinaryStream outputFile = BinaryStream.OpenWrite(outputPath);
SmfHeader.WriteMagicBytes(outputFile);

StandarMediaInfo fileInfo = new(inputFile, compressionLvl);
byte[] compressedData = CompressDataInBlocks(inputFile, compressionLvl, ref fileInfo);

byte[] smfFileBuffers = Input_Manager.MergeArrays(fileInfo.SizeBeforeCompression, compressedData);
outputFile.Write(smfFileBuffers);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_COMPRESS_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_COMPRESS_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Compresses all the RSB Files of a Folder by using Deflate Compression. </summary>

<param name = "inputPath"> The Path where the Folder to be Compresed is Located. </param>
<param name = "outputPath"> The Location where the Compressed Folder will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be used. </param>

<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFolder(string inputPath, string outputPath, CompressionLevel compressionLvl)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_COMPRESS_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var compressAction = GetCompressAction(compressionLvl, false);

Task_Manager.BatchTask(inputPath, outputPath, new(), Path_Helper.specificFileExtensions["ResBundle"], compressAction);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_COMPRESS_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_COMPRESS_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Compresses a FileSystem by using Brotli Compression. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Compresed is Located. </param>
<param name = "outputPath"> The Location where the Compresed FileSystem will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be used. </param> */

public static void CompressFileSystem(string inputPath, string outputPath, CompressionLevel compressionLvl)
{
var singleAction = GetCompressAction(compressionLvl, false);
var batchAction = GetCompressAction(compressionLvl, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decompresses the Data Contained on a Bytes Array by using Deflate Compression. </summary>

<param name = "inputStream"> The Stream which Contains the Data to be Decompressed. </param>

<returns> The Data Decompressed. </returns> */

private static byte[] DecompressDataInBlocks(Stream inputStream)
{
byte[] compressedData = new byte[inputStream.Length - 6];
inputStream.Seek(2, SeekOrigin.Begin);

inputStream.Read(compressedData);
using MemoryStream readingStream = new(compressedData);

using MemoryStream outputStream = new();
using DeflateStream decompressionStream = new(readingStream, CompressionMode.Decompress);

byte[] bufferData = new byte[bufferSize];
int bytesRead;

while( (bytesRead = decompressionStream.Read(bufferData) ) != 0)
outputStream.Write(bufferData, 0, bytesRead);

return outputStream.ToArray();
}

/** <summary> Decompresses the Contents of a SMF File as a RSB File by using Deflate Compression. </summary>

<param name = "inputPath" > The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath" > The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecompressFile(string inputPath, string outputPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECOMPRESS_FILE, Path.GetFileName(inputPath) );
using BinaryStream inputFile = BinaryStream.Open(inputPath);

SmfHeader.ReadMagicBytes(inputFile);
inputFile.Seek(8, SeekOrigin.Begin);

byte[] bufferData = new byte[bufferSize];
using MemoryStream outputStream = new();

int bytesRead;

while( (bytesRead = inputFile.Read(bufferData) ) != 0)
outputStream.Write(bufferData, 0, bytesRead);

byte[] decompressedData = DecompressDataInBlocks(outputStream);
Path_Helper.RemoveExtension(ref outputPath, smfFileExt);

Archive_Manager.CheckDuplicatedPath(ref outputPath);
File.WriteAllBytes(outputPath, decompressedData);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECOMPRESS_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECOMPRESS_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decompresses all the SMF Files store in a SMF Folder by using Deflate Compression. </summary>

<param name = "inputPath"> The Path where the Folder to be Decompresed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed Folder will be Saved. </param>

<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecompressFolder(string inputPath, string outputPath)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECOMPRESS_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decompressAction = GetDecompressAction(false);

Task_Manager.BatchTask(inputPath, outputPath, decompressAction, smfFileExt);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECOMPRESS_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECOMPRESS_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decompresses a SMF FileSystem by using Deflate Compression. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decompresed is Located. </param>
<param name = "outputPath"> The Location where the Decompresed FileSystem will be Saved. </param> */

public static void DecompressFileSystem(string inputPath, string outputPath)
{
var singleAction = GetDecompressAction(false);
var batchAction = GetDecompressAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets a Compress Action by using the Specified Compression Level and Batch Mode. </summary>

<param name = "compressionLvl"> The Compression Level to be used. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Compress Action Obtained. </returns> */

private static Action<string, string> GetCompressAction(CompressionLevel compressionLvl, bool isBatchAction)
{
ActionWrapper<string, string> compressAction;

if(isBatchAction)
{
compressAction = new( (inputPath, outputPath) => CompressFolder(inputPath, outputPath, compressionLvl) );
}

else
{
compressAction = new( (inputPath, outputPath) => CompressFile(inputPath, outputPath, compressionLvl) );
}

return compressAction.Init;
}

/** <summary> Gets a Decompress Action depending on the Specified Batch Mode. </summary>

<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decompress Action Obtained. </returns> */

private static Action<string, string> GetDecompressAction(bool isBatchAction)
{
ActionWrapper<string, string> decompressAction;

if(isBatchAction)
{
decompressAction = new( DecompressFolder );
}

else
{
decompressAction = new( DecompressFile );
}

return decompressAction.Init;
}

}

}