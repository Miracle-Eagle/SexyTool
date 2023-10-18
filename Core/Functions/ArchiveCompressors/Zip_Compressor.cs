using System;
using System.IO;
using System.IO.Compression;

namespace SexyTool.Program.Core.Functions.ArchiveCompressors
{
/// <summary> Initializes Compressing and Decompressing Functions for Files by using the Zip algorithm. </summary>

public class Zip_Compressor
{
/** <summary> Sets a Value which Contains Info about the Extension of a Zip file. </summary>
<returns> The Zip File Extension. </returns> */

private static readonly string zipFileExt = ".zip";

/** <summary> Compresses the Contents of a File by using Zip Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be used. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFile(string inputPath, string outputPath, CompressionLevel compressionLvl)
{

try
{
string inputFileName = Path.GetFileName(inputPath);
Text.Print(true, Text.LocalizedData.ACTION_COMPRESS_FILE, inputFileName);

using FileStream inputFile = File.OpenRead(inputPath);
Path_Helper.AddExtension(ref outputPath, zipFileExt);

Archive_Manager.CheckDuplicatedPath(ref outputPath);
using FileStream outputFile = File.OpenWrite(outputPath);

using ZipArchive compressionStream = new(outputFile, ZipArchiveMode.Create);
ZipArchiveEntry targetFileEntry = compressionStream.CreateEntry(inputFileName, compressionLvl);

using Stream targetEntryStream = targetFileEntry.Open();
inputFile.CopyTo(targetEntryStream);
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

/** <summary> Compresses the Contents of a Folder by using Zip Compression. </summary>

<param name = "inputPath"> The Path where the Folder to be Compresed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be used. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void CompressFolder(string inputPath, string outputPath, CompressionLevel compressionLvl)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_COMPRESS_FOLDER, Directory_Manager.GetFolderName(inputPath) );
Path_Helper.AddExtension(ref outputPath, zipFileExt);

Archive_Manager.CheckDuplicatedPath(ref outputPath);
string outputFileName = Path.GetFileName(outputPath);

using FileStream outputFile = File.OpenWrite(outputPath);
using ZipArchive compressionStream = new(outputFile, ZipArchiveMode.Create);

string[] inputFilesList = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories);

foreach(string inputFilePath in inputFilesList)
{
using FileStream inputFile = File.OpenRead(inputFilePath);
string relativePath = Path.GetRelativePath(inputPath, inputFilePath);

Text.PrintLine(false, Text.LocalizedData.ACTION_ADD_FILE_ENTRY, Path.GetFileName(relativePath), outputFileName);
ZipArchiveEntry targetFileEntry = compressionStream.CreateEntry(relativePath, compressionLvl);

using Stream targetEntryStream = targetFileEntry.Open();
inputFile.CopyTo(targetEntryStream);
}

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

/** <summary> Compresses a FileSystem by using Zip Compression. </summary>
<param name = "inputPath"> The Path where the FileSystem to be Compresed is Located. </param>

<param name = "outputPath"> The Location where the Compresed FileSystem will be Saved. </param>
<param name = "compressionLvl"> The Compression Level to be used. </param> */

public static void CompressFileSystem(string inputPath, string outputPath, CompressionLevel compressionLvl)
{
var singleAction = GetCompressAction(compressionLvl, false);
var batchAction = GetCompressAction(compressionLvl, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decompresses the Contents of a File by using Zip Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed contents will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecompressFile(string inputPath, string outputPath)
{

try
{
string inputFileName = Path.GetFileName(inputPath);
Text.Print(true, Text.LocalizedData.ACTION_DECOMPRESS_FILE, inputFileName);

using FileStream inputFile = File.OpenRead(inputPath);
using ZipArchive decompressionStream = new ZipArchive(inputFile);

foreach(ZipArchiveEntry targetFileEntry in decompressionStream.Entries)
{
string relativePath = Path.GetRelativePath(outputPath, targetFileEntry.Name);
Text.PrintLine(false, Text.LocalizedData.ACTION_EXTRACT_FILE_ENTRY, Path.GetFileName(relativePath), inputFileName);

string outputFilePath = Path.Combine(outputPath, relativePath);
string parentPath = Path.GetDirectoryName(outputFilePath);

Directory_Manager.CheckMissingFolder(parentPath);
targetFileEntry.ExtractToFile(outputFilePath);
}

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

/** <summary> Decompresses the Contents of a Folder by using Zip Compression. </summary>

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
Action<string, string> decompressAction = GetDecompressAction(false);

Task_Manager.BatchTask(inputPath, outputPath, decompressAction, zipFileExt);
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

/** <summary> Decompresses a FileSystem by using Tar Compression. </summary>

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