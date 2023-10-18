using SexyTool.Program.Core.Functions.SexyCryptors;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SexyTool.Program.Core.Functions.ArchiveSecurity
{
/// <summary> Initializes Advanced Encryption Standard (AES) Ciphering Functions for Files. </summary>

public class AES_Cryptor : Rijndael_Cryptor
{
/** <summary> Creates a new Instance of the AES Algorithm. </summary>
<returns> The AES Algorithm. </returns> */

private static readonly Aes cipherAlgorithm = Aes.Create();

/** <summary> Encrypts a File by using AES Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCRYPT_FILE, Path.GetFileName(inputPath) );
cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);

byte[] IV = Crypto_Parameters.InitVector(cipherKey, cipherKeySize);
cipherAlgorithm.Mode = cipheringMode;

cipherAlgorithm.Padding = dataPadding;
using FileStream inputFile = File.OpenRead(inputPath);

using FileStream outputFile = File.OpenWrite(outputPath);
ICryptoTransform fileEncryptor = cipherAlgorithm.CreateEncryptor(cipherKey, IV);

using CryptoStream encryptionStream = new(outputFile, fileEncryptor, CryptoStreamMode.Write);
inputFile.CopyTo(encryptionStream);

encryptionStream.FlushFinalBlock();
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_ENCRYPT_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_ENCRYPT_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Encrypts all the Files of a Folder by using AES Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the Folder. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encryptAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, false);

Task_Manager.BatchTask(inputPath, outputPath, encryptAction);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_ENCRYPT_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_ENCRYPT_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Encrypts a FileSystem by using AES Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the System. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{
var singleAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, false);
var batchAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts a File that was Encrypted with AES Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Value Used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECRYPT_FILE, Path.GetFileName(inputPath) );
cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);

byte[] IV = Crypto_Parameters.InitVector(cipherKey, cipherKeySize);
cipherAlgorithm.Mode = cipheringMode;

cipherAlgorithm.Padding = dataPadding;
using FileStream inputFile = File.OpenRead(inputPath);

using FileStream outputFile = File.OpenWrite(outputPath);
ICryptoTransform fileDecryptor = cipherAlgorithm.CreateDecryptor(cipherKey, IV);

using CryptoStream decryptionStream = new(outputFile, fileDecryptor, CryptoStreamMode.Write);
inputFile.CopyTo(decryptionStream);

decryptionStream.FlushFinalBlock();
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECRYPT_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECRYPT_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decrypts all the Files of a Folder by using AES Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the Folder. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decryptAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, false);

Task_Manager.BatchTask(inputPath, outputPath, decryptAction);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DECRYPT_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DECRYPT_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Decrypts a FileSystem by using AES Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the System. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding)
{
var singleAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, false);
var batchAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified Key, Salt, Hash, Iterations, Cipher and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Encrypting the Data. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param> 
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetEncryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding, bool isBatchAction)
{
ActionWrapper<string, string> encryptAction;

if(isBatchAction)
{
encryptAction = new( (inputPath, outputPath) => EncryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding) );
}

else
{
encryptAction = new( (inputPath, outputPath) => EncryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding) );
}

return encryptAction.Init;
}

/** <summary> Gets a Decrypt Action by using the Specified Key, Salt, Hash, Iterations, Cipher and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Decrypting the Data. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "cipheringMode"> The ciphering Mode to be Used. </param> 
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>
<param name = "dataPadding"> The Padding Mode to be Used. </param>

<returns> The Decrypt Action Obtained. </returns> */

private static Action<string, string> GetDecryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, CipherMode cipheringMode, PaddingMode dataPadding, bool isBatchAction)
{
ActionWrapper<string, string> decryptAction;

if(isBatchAction)
{
decryptAction = new( (inputPath, outputPath) => DecryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding) );
}

else
{
decryptAction = new( (inputPath, outputPath) => DecryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, cipheringMode, dataPadding) );
}

return decryptAction.Init;
}

}

}