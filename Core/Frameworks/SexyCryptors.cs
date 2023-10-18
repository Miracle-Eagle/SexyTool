using SexyTool.Program.Core.Functions.SexyCryptors;
using SexyTool.Program.Core.Functions.SexyCryptors.PopCapCipheredData;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>SexyCryptors</c> of this Program. </summary>

internal class SexyCryptors : Framework
{
/// <summary> Creates a new Instance of the <c>SexyCryptors</c>. </summary>

public SexyCryptors()
{
ID = 9;
DisplayName = Text.LocalizedData.FRAMEWORK_SEXY_CRYPTORS;

FunctionsList = new()
{
// Function A - DES Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_DES,
Process = () => DES_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.DesCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.DesCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.DesCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.DesCryptoInfo.CipheringMode, Program.userParams.SexyCryptorsParams.DesCryptoInfo.DataPadding)
},

// Function B - DES Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_DES,
Process = () => DES_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.DesCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.DesCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.DesCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.DesCryptoInfo.CipheringMode, Program.userParams.SexyCryptorsParams.DesCryptoInfo.DataPadding)
},

// Function C - 3-DES Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_3DES,
Process = () => TripleDES_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.CipheringMode, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.DataPadding)
},

// Function D - 3-DES Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_3DES,
Process = () => TripleDES_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.CipheringMode, Program.userParams.SexyCryptorsParams.TripleDesCryptoInfo.DataPadding)
},

// Function E - Rijndael Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_RIJNDAEL,
Process = () => Rijndael_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.IterationsCount, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.BlockCipherName, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.CipherPaddingIndex)
},

// Function F - Rijndael Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_RIJNDAEL,
Process = () => Rijndael_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.IterationsCount, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.BlockCipherName, Program.userParams.SexyCryptorsParams.RijndaelCryptoInfo.CipherPaddingIndex)
},

// Function G - RSA Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.G,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_RSA,
Process = () => RSA_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.IterationsCount)
},

// Function H - RSA Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.H,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_RSA,
Process = () => RSA_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.CipherKey, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.SaltValue, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.HashType, Program.userParams.SexyCryptorsParams.RsaCryptoInfo.IterationsCount)
},

// Function I - PopData Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.I,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_CDAT,
Process = () => PopData_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath)
},

// Function H - PopData Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.J,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_CDAT,
Process = () => PopData_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath)
},

// Function K - RTON Cryptor: Encrypt FileSystem

new Function()
{
ID = ConsoleKey.K,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_FILESYSTEM_RTON,
Process = () => RTON_Cryptor.EncryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath)
},

// Function L - RTON Cryptor: Decrypt FileSystem

new Function()
{
ID = ConsoleKey.L,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_FILESYSTEM_RTON,
Process = () => RTON_Cryptor.DecryptFileSystem(Program.userParams.SexyCryptorsParams.InputPath, Program.userParams.SexyCryptorsParams.OutputPath)
}

};

}

}

}