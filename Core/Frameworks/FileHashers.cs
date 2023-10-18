using SexyTool.Program.Core.Functions.ArchiveHashers;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>FileHashers</c> of this Program. </summary>

internal class FileHashers : Framework
{
/// <summary> Creates a new Instance of the <c>FileHashers</c>. </summary>

public FileHashers()
{
ID = 5;
DisplayName = Text.LocalizedData.FRAMEWORK_FILE_HASHERS;

FunctionsList = new()
{
// Function A - MD5 Hasher: Hash FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_HASH_FILESYSTEM_MD5,
Process = () => MD5_Hasher.HashFileSystem(Program.userParams.FileHashersParams.InputPath, Program.userParams.FileHashersParams.OutputPath, Program.userParams.FileHashersParams.Md5DigestInfo.UseAuthCode, Program.userParams.FileHashersParams.Md5DigestInfo.CipherKey)
},

// Function B - SHA-1 Hasher: Hash FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_HASH_FILESYSTEM_SHA1,
Process = () => SHA1_Hasher.HashFileSystem(Program.userParams.FileHashersParams.InputPath, Program.userParams.FileHashersParams.OutputPath, Program.userParams.FileHashersParams.Sha1DigestInfo.UseAuthCode, Program.userParams.FileHashersParams.Sha1DigestInfo.CipherKey)
},

// Function C - SHA-256 Hasher: Hash FileSystem

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_HASH_FILESYSTEM_SHA256,
Process = () => SHA256_Hasher.HashFileSystem(Program.userParams.FileHashersParams.InputPath, Program.userParams.FileHashersParams.OutputPath, Program.userParams.FileHashersParams.Sha256DigestInfo.UseAuthCode, Program.userParams.FileHashersParams.Sha256DigestInfo.CipherKey)
},

// Function D - SHA-384 Hasher: Hash FileSystem

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_HASH_FILESYSTEM_SHA384,
Process = () => SHA384_Hasher.HashFileSystem(Program.userParams.FileHashersParams.InputPath, Program.userParams.FileHashersParams.OutputPath, Program.userParams.FileHashersParams.Sha384DigestInfo.UseAuthCode, Program.userParams.FileHashersParams.Sha384DigestInfo.CipherKey)
},

// Function E - SHA-512 Hasher: Hash FileSystem

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_HASH_FILESYSTEM_SHA512,
Process = () => SHA512_Hasher.HashFileSystem(Program.userParams.FileHashersParams.InputPath, Program.userParams.FileHashersParams.OutputPath, Program.userParams.FileHashersParams.Sha512DigestInfo.UseAuthCode, Program.userParams.FileHashersParams.Sha512DigestInfo.CipherKey)
}

};

}

}

}