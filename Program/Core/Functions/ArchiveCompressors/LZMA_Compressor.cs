using SevenZip;
using SevenZip.Compression.LZMA;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.ArchiveCompressors
{
/// <summary> Initializes Compressing and Decompressing Functions for Files by using the LZMA algorithm. </summary>

public class LZMA_Compressor
{
/** <summary> Sets a Value which Contains Info about the Extension of a LZMA file. </summary>
<returns> The LZMA File Extension. </returns> */

private static readonly string lzmaFileExt = ".lzma";

/** <summary> Sets a Value which Contains Info about the Length of the Coder Properties. </summary>
<returns> The Length of the Coder Properties. </returns> */

private const int coderPropsLength = 5;

/** <summary> Sets a Value which Contains Info about the Length of the Compression Flags. </summary>
<returns> The Length of the Compression Flags. </returns> */

private const int compressionFlagsLength = 8;

/** <summary> Sets a Value which Contains Info about the Display options of the Progress on Compression and Decompression Tasks. </summary>
<returns> The Display options of the Compression/Decompression Task Progress. </returns> */

private static readonly ICodeProgress processReporter = null;

/** <summary> Compresses the Contents of a File by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "NullReferenceException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFile(string inputPath, string outputPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_COMPRESS_FILE, Path.GetFileName(inputPath) );	
using FileStream inputFile = File.OpenRead(inputPath);

Path_Helper.AddExtension(ref outputPath, lzmaFileExt);
Archive_Manager.CheckDuplicatedPath(ref outputPath);

using FileStream outputFile = File.OpenWrite(outputPath);
Encoder fileCompressor = new();

fileCompressor.WriteCoderProperties(outputFile);
byte[] compressedDataFlags = BitConverter.GetBytes(inputFile.Length);

outputFile.Write(compressedDataFlags, 0, compressionFlagsLength);
fileCompressor.Code(inputFile, outputFile, inputFile.Length, -1, processReporter);
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

/** <summary> Compresses the Contents of a Folder using LZMA Compression. </summary>

<param name = "inputPath"> The Access Path where the Folder to be Compresed is Located. </param>
<param name = "outputPath"> The Location where the Compressed Folder will be Saved. </param>

<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFolder(string inputPath, string outputPath)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_COMPRESS_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var compressAction = GetCompressAction(false);

Task_Manager.BatchTask(inputPath, outputPath, compressAction);
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

/** <summary> Compresses a FileSystem by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Compresed is Located. </param>
<param name = "outputPath"> The Location where the Compresed FileSystem will be Saved. </param> */

public static void CompressFileSystem(string inputPath, string outputPath)
{
var singleAction = GetCompressAction(false);
var batchAction = GetCompressAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decompresses the Contents of a File by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "NullReferenceException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecompressFile(string inputPath, string outputPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_COMPRESS_FILE, Path.GetFileName(inputPath) );
using FileStream inputFile = File.OpenRead(inputPath);

Path_Helper.RemoveExtension(ref outputPath, lzmaFileExt);
Archive_Manager.CheckDuplicatedPath(ref outputPath);

using FileStream outputFile = File.OpenWrite(outputPath);
Decoder fileDecompressor = new();

byte[] coderPropsInfo = new byte[coderPropsLength];
inputFile.Read(coderPropsInfo, 0, coderPropsInfo.Length);

byte[] compressedDataFlags = new byte[compressionFlagsLength];
inputFile.Read(compressedDataFlags, 0, compressedDataFlags.Length);

fileDecompressor.SetDecoderProperties(coderPropsInfo);
long expectedDataSize = BitConverter.ToInt64(compressedDataFlags, 0);

fileDecompressor.Code(inputFile, outputFile, inputFile.Length, expectedDataSize, processReporter);
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

/** <summary> Decompresses the Contents of a Folder by using LZMA Compression. </summary>

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

Task_Manager.BatchTask(inputPath, outputPath, decompressAction, lzmaFileExt);
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

/** <summary> Decompresses a FileSystem by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decompresed is Located. </param>
<param name = "outputPath"> The Location where the Decompresed FileSystem will be Saved. </param> */

public static void DecompressFileSystem(string inputPath, string outputPath)
{
var singleAction = GetDecompressAction(false);
var batchAction = GetDecompressAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets a Compress Action by using the Specified Batch Mode. </summary>

<param name = "compressionLvl"> The Compression Level to be used. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Compress Action Obtained. </returns> */

private static Action<string, string> GetCompressAction(bool isBatchAction)
{
ActionWrapper<string, string> compressAction;

if(isBatchAction)
{
compressAction = new( CompressFolder );
}

else
{
compressAction = new( CompressFile );
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