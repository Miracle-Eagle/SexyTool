using SexyTool.Program.Core.Functions.ArchiveSecurity;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>FileSecurity</c> of this Program. </summary>

internal class FileSecurity : Framework
{
/// <summary> Creates a new Instance of the <c>FileSecurity</c>. </summary>

public FileSecurity()
{
ID = 6;
DisplayName = Text.LocalizedData.FRAMEWORK_FILE_SECURITY;

FunctionsList = new()
{
// Function A - Base64 Parser: Encode FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_ENCODE_FILESYSTEM_BASE64,
Process = () => Base64_Parser.EncodeFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.Base64ParseInfo.IsWebSafe)
},

// Function B - Base64 Parser: Decode FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_DECODE_FILESYSTEM_BASE64,
Process = () => Base64_Parser.DecodeFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.Base64ParseInfo.IsWebSafe)
},

// Function C - XOR Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_XOR,
Process = () => XOR_Cryptor.EncryptFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.XorCryptoInfo.CipherKey)
},

// Function D - XOR Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_XOR,
Process = () => XOR_Cryptor.DecryptFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.XorCryptoInfo.CipherKey)
},

// Function E - AES Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_AES,
Process = () => AES_Cryptor.EncryptFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.AesCryptoInfo.CipherKey, Program.userParams.FileSecurityParams.AesCryptoInfo.SaltValue, Program.userParams.FileSecurityParams.AesCryptoInfo.HashType, Program.userParams.FileSecurityParams.AesCryptoInfo.IterationsCount, Program.userParams.FileSecurityParams.AesCryptoInfo.CipheringMode, Program.userParams.FileSecurityParams.AesCryptoInfo.DataPadding)
},

// Function F - AES Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_AES,
Process = () => AES_Cryptor.DecryptFileSystem(Program.userParams.FileSecurityParams.InputPath, Program.userParams.FileSecurityParams.OutputPath, Program.userParams.FileSecurityParams.AesCryptoInfo.CipherKey, Program.userParams.FileSecurityParams.AesCryptoInfo.SaltValue, Program.userParams.FileSecurityParams.AesCryptoInfo.HashType, Program.userParams.FileSecurityParams.AesCryptoInfo.IterationsCount, Program.userParams.FileSecurityParams.AesCryptoInfo.CipheringMode, Program.userParams.FileSecurityParams.AesCryptoInfo.DataPadding)
}

};

}

}

}