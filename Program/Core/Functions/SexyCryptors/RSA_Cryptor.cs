using System;
using System.IO;
using System.Security.Cryptography;

namespace SexyTool.Program.Core.Functions.SexyCryptors
{
/// <summary> Initializes RSA (Rivest, Shamir and Adleman) Ciphering Functions for Files. </summary>

public class RSA_Cryptor
{
/** <summary> Creates a new Instance of the RSA Algorithm. </summary>
<returns> The RSA Algorithm. </returns> */

private static RSACryptoServiceProvider cipherAlgorithm;

/** <summary> Sets a Value which Contains Info about the RSA Name Prefix. </summary>
<returns> The RSA Name Prefix. </returns> */

private static readonly string rsaNamePrefix = "RsaCryptoInfo";

/** <summary> Sets a Value which Contains Info about the Range of a Cipher Key Size. </summary>
<returns> The Cipher Key Size. </returns> */

public static readonly Limit<int> cipherKeySize = new(128, 512);

/** <summary> Sets a Value which Contains Info about the Range of Iterations used for Generating derived Keys. </summary>
<returns> The Range of Iterations. </returns> */

public static readonly Limit<int> iterationsRange = new(1000, 100000);

/** <summary> Saves the Public and the Private Key from the given RSACryptoServiceProvider. </summary>

<param name = "targetFilePath"> The Location of the File that was Ciphered with RSA. </param>
<param name = "serviceProvider"> The RSACryptoServiceProvider where the Keys will be Retrieved from. </param> */

private static void SaveRsaKeys(string targetFilePath, ref RSACryptoServiceProvider serviceProvider)
{
string keyContainerPath = Directory_Manager.GetContainerPath(targetFilePath, rsaNamePrefix);
string publicKeyPath = keyContainerPath + Path.DirectorySeparatorChar + "PublicKey.xml";

File.WriteAllText(publicKeyPath, serviceProvider.ToXmlString(false) );
string privateKeyPath = keyContainerPath + Path.DirectorySeparatorChar + "PrivateKey.xml";

File.WriteAllText(privateKeyPath, serviceProvider.ToXmlString(true) );
}

/** <summary> Loads the RSA Key to the given RSACryptoServiceProvider. </summary>

<param name = "targetFilePath"> The Location of the File that was Ciphered with RSA. </param>
<param name = "serviceProvider"> The RSACryptoServiceProvider where the Keys will be Retrieved. </param>
<param name = "usePrivateKey"> A boolean that Determines if the Public or the Private Key should be used. </param> */

private static void LoadRsaKey(string targetFilePath, ref RSACryptoServiceProvider serviceProvider, bool usePrivateKey)
{
string keyContainerPath = Directory_Manager.GetContainerPath(targetFilePath, rsaNamePrefix);
string rsaKeyFlags = usePrivateKey ? "PrivateKey.xml" : "PublicKey.xml";

string rsaKeyPath = keyContainerPath + Path.DirectorySeparatorChar + rsaKeyFlags;

if(!File.Exists(rsaKeyPath) )
throw new FileNotFoundException("The file which Contains the Key is Missing.");

serviceProvider.FromXmlString(File.ReadAllText(rsaKeyPath) );
}

/** <summary> Encrypts an Array of Bytes in Blocks. </summary>
<param name = "inputData"> The Data to be Encrypted. </param>

<param name = "serviceProvider"> The RSACryptoServiceProvider to be used. </param>

<param name = "useOAEP"> A Boolean that Determines if OAEP should be Used when Encrypting the Data.<para>
</para> OAEP stands for Optimal Asynmetric Encryption Padding. </param>

<returns> The Data Encrypted. </returns> */

private static byte[] EncryptDataInBlocks(byte[] inputData, RSACryptoServiceProvider serviceProvider, bool useOAEP)
{
int paddingSize = useOAEP ? 42 : 11;
int maxBlockSize = (serviceProvider.KeySize/8) - paddingSize;

using MemoryStream outputStream = new();

for(int offset = 0; offset < inputData.Length; offset += maxBlockSize)
{
int blockSize = Math.Min(maxBlockSize, inputData.Length - offset);
byte[] blockData = new byte[blockSize];

Buffer.BlockCopy(inputData, offset, blockData, 0, blockSize);
byte[] encryptedBlock = serviceProvider.Encrypt(blockData, useOAEP);

outputStream.Write(encryptedBlock, 0, encryptedBlock.Length);
}

return outputStream.ToArray(); 
}

/** <summary> Encrypts a File by using RSA Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCRYPT_FILE, Path.GetFileName(inputPath) );
cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);

CspParameters serviceParams = new()
{
KeyContainerName = Input_Manager.ConvertHexString(cipherKey),
KeyNumber = (int)KeyNumber.Exchange
};

cipherAlgorithm = new(cipherKey.Length * 8, serviceParams);
SaveRsaKeys(inputPath, ref cipherAlgorithm);

byte[] inputBytes = File.ReadAllBytes(inputPath);
byte[] encryptedData = EncryptDataInBlocks(inputBytes, cipherAlgorithm, false);

File.WriteAllBytes(outputPath, encryptedData);
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

cipherAlgorithm.PersistKeyInCsp = false;
cipherAlgorithm.Clear();
}

}

/** <summary> Encrypts all the Files of a Folder by using RSA Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the Folder. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{
	
try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encryptAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, false);

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

/** <summary> Encrypts a FileSystem by using RSA Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the System. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{
var singleAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, false);
var batchAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts an Array of Bytes in Blocks. </summary>
<param name = "inputData"> The Data to be Decrypted. </param>

<param name = "serviceProvider"> The RSACryptoServiceProvider to be used. </param>

<param name = "useOAEP"> A Boolean that Determines if OAEP should be Used when Decrypting the Data.<para>
</para> OAEP stands for Optimal Asynmetric Encryption Padding. </param>

<returns> The Data Decrypted. </returns> */

private static byte[] DecryptDataInBlocks(byte[] inputData, RSACryptoServiceProvider serviceProvider, bool useOAEP)
{
int paddingSize = useOAEP ? 42 : 11;
int maxBlockSize = (serviceProvider.KeySize/8) - paddingSize;

using MemoryStream outputStream = new();

for(int offset = 0; offset < inputData.Length; offset += maxBlockSize)
{
int blockSize = Math.Min(maxBlockSize, inputData.Length - offset);
byte[] blockData = new byte[blockSize];

Buffer.BlockCopy(inputData, offset, blockData, 0, blockSize);
byte[] decryptedBlock = serviceProvider.Decrypt(blockData, useOAEP);

outputStream.Write(decryptedBlock, 0, decryptedBlock.Length);
}

return outputStream.ToArray(); 
}

/** <summary> Decrypts a File that was Encrypted with RSA Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Value Used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECRYPT_FILE, Path.GetFileName(inputPath) );
cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);

cipherAlgorithm = new();
LoadRsaKey(inputPath, ref cipherAlgorithm, true);

byte[] inputBytes = File.ReadAllBytes(inputPath);
byte[] decryptedData = DecryptDataInBlocks(inputBytes, cipherAlgorithm, false);

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

cipherAlgorithm.PersistKeyInCsp = false;
cipherAlgorithm.Clear();
}

}

/** <summary> Decrypts all the Files of a Folder by using RSA Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the Folder. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decryptAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, false);

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
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount)
{
var singleAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, false);
var batchAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified Key, Salt, Hash, Iterations and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Encrypting the Data. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetEncryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, bool isBatchAction)
{
ActionWrapper<string, string> encryptAction;

if(isBatchAction)
{
encryptAction = new( (inputPath, outputPath) => EncryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount) );
}

else
{
encryptAction = new( (inputPath, outputPath) => EncryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount) );
}

return encryptAction.Init;
}

/** <summary> Gets a Decrypt Action by using the Specified Key, Salt, Hash, Iterations and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Decrypting the Data. </param>
<param name = "saltValue"> The Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decrypt Action Obtained. </returns> */

private static Action<string, string> GetDecryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, bool isBatchAction)
{
ActionWrapper<string, string> decryptAction;

if(isBatchAction)
{
decryptAction = new( (inputPath, outputPath) => DecryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount) );
}

else
{
decryptAction = new( (inputPath, outputPath) => DecryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount) );
}

return decryptAction.Init;
}

}

}