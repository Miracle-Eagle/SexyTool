using SexyTool.Program.Core.Functions.ArchiveCompressors;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>FileCompressors</c> of this Program. </summary>

internal class FileCompressors : Framework
{
/// <summary> Creates a new Instance of the <c>FileCompressors</c>. </summary>

public FileCompressors()
{
ID = 4;
DisplayName = Text.LocalizedData.FRAMEWORK_FILE_COMPRESSORS;

FunctionsList = new()
{
// Function A - Brotli Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_BROTLI,
Process = () => Brotli_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath, Program.userParams.FileCompressorsParams.CompressionLvl)
},

// Function B - Brotli Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_BROTLI,
Process = () => Brotli_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function C - BZip2 Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_BZIP2,
Process = () => BZip2_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function D - BZip2 Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_BZIP2,
Process = () => BZip2_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function E - Deflate Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_DEFLATE,
Process = () => Deflate_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath, Program.userParams.FileCompressorsParams.CompressionLvl)
},

// Function F - Deflate Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_DEFLATE,
Process = () => Deflate_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function G - GZip Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.G,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_GZIP,
Process = () => GZip_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath, Program.userParams.FileCompressorsParams.CompressionLvl)
},

// Function H - GZip Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.H,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_GZIP,
Process = () => GZip_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function I - LZ4 Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.I,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_LZ4,
Process = () => LZ4_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function J - LZ4 Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.J,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_LZ4,
Process = () => LZ4_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function K - LZMA Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.K,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_LZMA,
Process = () => LZMA_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function L - LZMA Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.L,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_LZMA,
Process = () => LZMA_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function M - Tar Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.M,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_TAR,
Process = () => Tar_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function N - Tar Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.N,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_TAR,
Process = () => Tar_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function O - Zip Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.O,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_ZIP,
Process = () => Zip_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath, Program.userParams.FileCompressorsParams.CompressionLvl)
},

// Function P - Zip Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.P,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_ZIP,
Process = () => Zip_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
},

// Function Q - ZLib Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.Q,
DisplayName = Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_ZLIB,
Process = () => ZLib_Compressor.CompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath, Program.userParams.FileCompressorsParams.CompressionLvl)
},

// Function R - ZLib Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.R,
DisplayName = Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_ZLIB,
Process = () => ZLib_Compressor.DecompressFileSystem(Program.userParams.FileCompressorsParams.InputPath, Program.userParams.FileCompressorsParams.OutputPath)
}

};

}

}

}