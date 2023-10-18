using SexyTool.Program.Core.Functions.StringHashers;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.SexyCryptors
{
/// <summary> Initializes Ciphering Functions for RTON Files. </summary>

public class RTON_Cryptor : Rijndael_Cryptor
{
/** <summary> Sets a Value which Contains Info about the Header of an Encrypted File. </summary>
<returns> The expected Crypto Header. </returns> */

private static readonly byte[] cryptoHeader = { 0x10, 0x00 };

/** <summary> Sets a Value which Contains Info about the Cipher Key used for ciphering the Files. </summary>
<returns> The Cipher Key. </returns> */

private static byte[] cipherKey = Console.InputEncoding.GetBytes("com_popcap_pvz2_magento_product_2013_05_05");

/** <summary> Generates a derived Key from an Existing one by using MD5 Digest. </summary>

<param name = "cipherKey"> The Cipher Key. </param>

<returns> The MD5 Key. </returns> */

private static byte[] GetMd5Key(byte[] cipherKey)
{
string hashedKeyString = Md5StringDigest.DigestData(cipherKey, false);
return Console.InputEncoding.GetBytes(hashedKeyString);
}

/** <summary> Encrypts a RTON File by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFile(string inputPath, string outputPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCRYPT_FILE, Path.GetFileName(inputPath) );
byte[] inputBytes = File.ReadAllBytes(inputPath);

cipherKey = GetMd5Key(cipherKey);
int vectorIndex = 4;

Limit<int> vectorSize = new(cipherKey.Length - vectorIndex * cryptoHeader.Length);
byte[] IV = Crypto_Parameters.InitVector(cipherKey, vectorSize, vectorIndex);

byte[] encryptedData = CipherData(inputBytes, cipherKey, IV, true);
using BinaryStream outputFile = BinaryStream.OpenWrite(outputPath);

outputFile.Write(cryptoHeader);
outputFile.Write(encryptedData);
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

/** <summary> Encrypts all the RTON Files of a Folder by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted Folder will be Saved. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFolder(string inputPath, string outputPath)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encryptAction = GetEncryptAction(false);

Task_Manager.BatchTask(inputPath, outputPath, Path_Helper.specificFileNames["RtObject"], Path_Helper.specificFileExtensions["RtObject"], encryptAction);
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

/** <summary> Encrypts a FileSystem by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted FileSystem will be Saved. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath)
{
var singleAction = GetEncryptAction(false);
var batchAction = GetEncryptAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts a File by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFile(string inputPath, string outputPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECRYPT_FILE, Path.GetFileName(inputPath) );
using BinaryStream inputFile = BinaryStream.Open(inputPath);

inputFile.CompareBytes(cryptoHeader);
byte[] inputBytes = inputFile.ReadBytes(inputFile.Length);

cipherKey = GetMd5Key(cipherKey);
int vectorIndex = 4;

Limit<int> vectorSize = new(cipherKey.Length - vectorIndex * cryptoHeader.Length);
byte[] IV = Crypto_Parameters.InitVector(cipherKey, vectorSize, vectorIndex);

byte[] decryptedData = CipherData(inputBytes, cipherKey, IV, false);
File.WriteAllBytes(outputPath, decryptedData);
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

/** <summary> Decrypts all the Files of a Folder by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted Folder will be Saved. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFolder(string inputPath, string outputPath)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decryptAction = GetDecryptAction(false);

Task_Manager.BatchTask(inputPath, outputPath, Path_Helper.specificFileNames["RtObject"], Path_Helper.specificFileExtensions["RtObject"], decryptAction);
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

/** <summary> Decrypts a FileSystem by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted FileSystem will be Saved. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath)
{
var singleAction = GetDecryptAction(false);
var batchAction = GetDecryptAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified CBatch Mode. </summary>

<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetEncryptAction(bool isBatchAction)
{
ActionWrapper<string, string> encryptAction;

if(isBatchAction)
{
encryptAction = new( EncryptFolder );
}

else
{
encryptAction = new( EncryptFile );
}

return encryptAction.Init;
}

/** <summary> Gets a Decrypt Action by using the Specified Batch Mode. </summary>

<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decrypt Action Obtained. </returns> */

private static Action<string, string> GetDecryptAction(bool isBatchAction)
{
ActionWrapper<string, string> decryptAction;

if(isBatchAction)
{
decryptAction = new( DecryptFolder );
}

else
{
decryptAction = new( DecryptFile );
}

return decryptAction.Init;
}

}

}