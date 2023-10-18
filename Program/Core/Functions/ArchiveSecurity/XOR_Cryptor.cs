using SexyTool.Program.Core.Functions.Other;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.ArchiveSecurity
{
/// <summary> Initializes Exclusive-OR (XOR) Ciphering Functions for Files. </summary>

public class XOR_Cryptor : XorBytesCryptor
{
/** <summary> Encrypts a File by using XOR Ciphering. </summary>

<param name = "inputPath"> Th Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key Used for Encrypting the File. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFile(string inputPath, string outputPath, byte[] cipherKey)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_ENCRYPT_FILE, Path.GetFileName(inputPath) );
byte[] inputFileBytes = File.ReadAllBytes(inputPath);

byte[] encryptedFileData = CipherData(inputFileBytes, cipherKey);
File.WriteAllBytes(outputPath, encryptedFileData);
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

/** <summary> Encrypts all the Files of a Folder by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Encrypting the Folder. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void EncryptFolder(string inputPath, string outputPath, byte[] cipherKey)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_ENCRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var encryptAction = GetEncryptAction(cipherKey, false);

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

/** <summary> Encrypts a FileSystem by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Encrypted is Located. </param>
<param name = "outputPath"> The Path where the Encrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key Used for Encrypting the File. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath, byte[] cipherKey)
{
var singleAction = GetEncryptAction(cipherKey, false);
var batchAction = GetEncryptAction(cipherKey, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts a File that was Encrypted with XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the File. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFile(string inputPath, string outputPath, byte[] cipherKey)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DECRYPT_FILE, Path.GetFileName(inputPath) );
byte[] inputFileBytes = File.ReadAllBytes(inputPath);

byte[] decryptedFileData = CipherData(inputFileBytes, cipherKey);
File.WriteAllBytes(outputPath, decryptedFileData);
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

/** <summary> Decrypts all the Files of a Folder by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted Folder will be Saved. </param>
<param name = "cipherKey"> The Cipher Key used for Decrypting the Folder. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void DecryptFolder(string inputPath, string outputPath, byte[] cipherKey)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_DECRYPT_FOLDER, Directory_Manager.GetFolderName(inputPath) );
var decryptAction = GetDecryptAction(cipherKey, false);

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

/** <summary> Decrypts a FileSystem by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Decrypted is Located. </param>
<param name = "outputPath"> The Path where the Decrypted FileSystem will be Saved. </param>
<param name = "cipherKey"> The Cipher Key Used for Decrypting the File. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath, byte[] cipherKey)
{
var singleAction = GetDecryptAction(cipherKey, false);
var batchAction = GetDecryptAction(cipherKey, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified Cipher Key and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Encrypting the Data. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetEncryptAction(byte[] cipherKey, bool isBatchAction)
{
ActionWrapper<string, string> encryptAction;

if(isBatchAction)
{
encryptAction = new( (inputPath, outputPath) => EncryptFolder(inputPath, outputPath, cipherKey) );
}

else
{
encryptAction = new( (inputPath, outputPath) => EncryptFile(inputPath, outputPath, cipherKey) );
}

return encryptAction.Init;
}

/** <summary> Gets a Decrypt Action by using the Specified Cipher Key and Batch Mode. </summary>

<param name = "cipherKey"> The Cipher Key used for Decrypting the Data. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Encrypt Action Obtained. </returns> */

private static Action<string, string> GetDecryptAction(byte[] cipherKey, bool isBatchAction)
{
ActionWrapper<string, string> decryptAction;

if(isBatchAction)
{
decryptAction = new( (inputPath, outputPath) => DecryptFolder(inputPath, outputPath, cipherKey) );
}

else
{
decryptAction = new( (inputPath, outputPath) => DecryptFile(inputPath, outputPath, cipherKey) );
}

return decryptAction.Init;
}

}

}