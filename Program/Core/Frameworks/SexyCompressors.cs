using SexyTool.Program.Core.Functions.SexyCompressors.StandarMediaFile;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>SexyCompressors</c> of this Program. </summary>

internal class SexyCompressors : Framework
{
/// <summary> Creates a new Instance of the <c>SexyCompressors</c>. </summary>

public SexyCompressors()
{
ID = 8;
DisplayName = "Text.LocalizedData.FRAMEWORK_SEXY_COMPRESSORS";

FunctionsList = new()
{
// Function A - SMF Compressor: Compress FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = "Text.LocalizedData.FUNCTION_COMPRESS_FILESYSTEM_SMF",
Process = () => SMF_Compressor.CompressFileSystem(Program.userParams.SexyCompressorsParams.InputPath, Program.userParams.SexyCompressorsParams.OutputPath, Program.userParams.SexyCompressorsParams.CompressionLvl)
},

// Function B - SMF Compressor: Decompress FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = "Text.LocalizedData.FUNCTION_DECOMPRESS_FILESYSTEM_SMF",
Process = () => SMF_Compressor.DecompressFileSystem(Program.userParams.SexyCompressorsParams.InputPath, Program.userParams.SexyCompressorsParams.OutputPath)
},

// Keep adding funcs here

};

}

}

}