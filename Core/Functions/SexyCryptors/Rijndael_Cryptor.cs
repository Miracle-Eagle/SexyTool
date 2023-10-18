using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.SexyCryptors
{
/// <summary> Initializes Rijndael (Vincent, Rijmen and Joan Daemen) Ciphering Functions for Files. </summary>

public class Rijndael_Cryptor
{
/** <summary> Sets a Value which Contains Info about the Range of a Cipher Key Size. </summary>
<returns> The Cipher Key Size. </returns> */

public static readonly Limit<int> cipherKeySize = new(16, 32);

/** <summary> Sets a Value which Contains Info about the Range of Iterations used for Generating derived Cipher Keys. </summary>
<returns> The Range of Iterations. </returns> */

public static readonly Limit<int> iterationsRange = new(10, 14);

/** <summary> Ciphers the Data specified with the provided Key. </summary>

<param name = "inputBytes"> The Bytes to be Ciphered. </param>
<param name = "cipherKey"> The Cipher Key to be Used. </param>
<param name = "IV"> The Cipher Key to be Used. </param>
<param name = "isForEncryption"> A Boolean that determines if the Data should be Encrypted or not. </param>
<param name = "blockCipherName"> The expected BlockCipher Name (Default is CBC). </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding (Default is ZeroPadding). </param>

<returns> The Data Ciphered. </returns> */

protected static byte[] CipherData(byte[] inputBytes, byte[] cipherKey, byte[] IV, bool isForEncryption, string blockCipherName = "CBC", int cipherPaddingIndex = 0)
{
RijndaelEngine cryptoEngine = new(IV.Length * 8);
IBlockCipher blockCipher = GetBlockCipher(blockCipherName, cryptoEngine);

IBlockCipherPadding blockCipherPadding = GetBlockCipherPadding(cipherPaddingIndex);
PaddedBufferedBlockCipher cipherAlgorithm = new(blockCipher, blockCipherPadding);

ParametersWithIV cryptoParams = new( new KeyParameter(cipherKey), IV);
cipherAlgorithm.Init(isForEncryption, cryptoParams);

int minBlockSize = cipherAlgorithm.GetOutputSize(inputBytes.Length);
byte[] outputBuffers = new byte[minBlockSize];

int processedBytesCount = cipherAlgorithm.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBuffers, 0);
int finalBuffersLength = cipherAlgorithm.DoFinal(outputBuffers, processedBytesCount);

byte[] cipheredData = new byte[processedBytesCount + finalBuffersLength];
Array.Copy(outputBuffers, cipheredData, cipheredData.Length);

return cipheredData;
}

/** <summary> Gets a Instance from the IBlockCipher Interface basing on the given Cipher Name. </summary>

<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cryptoEngine"> An Instance from the RijndalEngine where the Block Cipher will be Obtained. </param>

<returns> The Block Cipher Obtained. </returns> */

private static IBlockCipher GetBlockCipher(string blockCipherName, RijndaelEngine cryptoEngine)
{

IBlockCipher blockCipher = blockCipherName switch
{
"CFB" => new CfbBlockCipher(cryptoEngine, cipherKeySize.MinValue),
"OFB" => new OfbBlockCipher(cryptoEngine, cipherKeySize.MinValue),
"SIC" => new SicBlockCipher(cryptoEngine),
_ => new CbcBlockCipher(cryptoEngine)
};

return blockCipher;
}

/** <summary> Gets a Instance from the IBlockCipherPadding Interface basing on the given Padding Index. </summary>

<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>

<returns> The BlockCipherPadding Obtained. </returns> */

private static IBlockCipherPadding GetBlockCipherPadding(int cipherPaddingIndex)
{

IBlockCipherPadding blockCipherPadding = cipherPaddingIndex switch
{
1 => new ISO7816d4Padding(),
2 => new Pkcs7Padding(),
3 => new TbcPadding(),
4 => new X923Padding(),
_ => new ZeroBytePadding()
};

return blockCipherPadding;
}

/** <summary> Encrypts a File by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

private static void EncryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCRYPT_FILE, Path.GetFileName(inputPath) );
byte[] inputBytes = File.ReadAllBytes(inputPath);

cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);
byte[] IV = Crypto_Parameters.InitVector(cipherKey, cipherKeySize);

byte[] encryptedData = CipherData(inputBytes, cipherKey, IV, true, blockCipherName, cipherPaddingIndex);
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

}

}

/** <summary> Encrypts all the Files of a Folder by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

private static void EncryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encryptAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, false);

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

/** <summary> Encrypts a FileSystem by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{
var singleAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, false);
var batchAction = GetEncryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts a File by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

private static void DecryptFile(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECRYPT_FILE, Path.GetFileName(inputPath) );
byte[] inputBytes = File.ReadAllBytes(inputPath);

cipherKey = Crypto_Parameters.CipherKeySchedule(cipherKey, saltValue, hashType, iterationsCount, cipherKeySize, iterationsRange);
byte[] IV = Crypto_Parameters.InitVector(cipherKey, cipherKeySize);

byte[] decryptedData = CipherData(inputBytes, cipherKey, IV, false, blockCipherName, cipherPaddingIndex);
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
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>

<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

private static void DecryptFolder(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decryptAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, false);

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

/** <summary> Decrypts a FileSystem by using Rijndael Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted FileSystem will be Saved. </param> 
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath, byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex)
{
var singleAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, false);
var batchAction = GetDecryptAction(cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified Key, Salt, Hash, Iterations and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Encrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetEncryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex, bool isBatchAction)
{
ActionWrapper<string, string> encryptAction;

if(isBatchAction)
{
encryptAction = new( (inputPath, outputPath) => EncryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex) );
}

else
{
encryptAction = new( (inputPath, outputPath) => EncryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex) );
}

return encryptAction.Init;
}

/** <summary> Gets a Decrypt Action by using the Specified Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>
<param name = "saltValue"> The Salt value used for Reinforcing the Cipher Key. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "blockCipherName"> The expected BlockCipher Name. </param>
<param name = "cipherPaddingIndex"> The Index of the BlockCipherPadding. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Decrypt Action Obtained. </returns> */

private static Action<string, string> GetDecryptAction(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, string blockCipherName, int cipherPaddingIndex, bool isBatchAction)
{
ActionWrapper<string, string> decryptAction;

if(isBatchAction)
{
decryptAction = new( (inputPath, outputPath) => DecryptFolder(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex) );
}

else
{
decryptAction = new( (inputPath, outputPath) => DecryptFile(inputPath, outputPath, cipherKey, saltValue, hashType, iterationsCount, blockCipherName, cipherPaddingIndex) );
}

return decryptAction.Init;
}

}

}