using SexyTool.Program.Core.Functions.ArchiveSecurity;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.SexyCryptors.PopCapCipheredData
{
/// <summary> Initializes Functions for the Ciphered Data (CDAT) from PopCap Files. </summary>

public class PopData_Cryptor : XOR_Cryptor
{
/** <summary> Sets a Value which Contains Info about the Extension of a Ciphered file. </summary>
<returns> The Ciphered File Extension. </returns> */

private static readonly string cdatFileExt = ".cdat";

/** <summary> Sets a Value which Contains Info about the Cipher Key used for Ciphering Image Files. </summary>
<returns> The Cipher Key Obtained. </returns> */

private static readonly byte[] cipherKey = Console.InputEncoding.GetBytes("AS23DSREPLKL335KO4439032N8345NF");

/** <summary> Encrypts the specified File with Xor Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>

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
byte[] inputFileBytes = File.ReadAllBytes(inputPath);

CipheredDataInfo fileCryptoInfo = new(inputFileBytes.Length);
Path_Helper.AddExtension(ref outputPath, cdatFileExt);

Archive_Manager.CheckDuplicatedPath(ref outputPath);
using BinaryStream outputFile = BinaryStream.OpenWrite(outputPath);

fileCryptoInfo.Write(outputFile);
byte[] encryptedFileData = CipherData(inputFileBytes, cipherKey);

outputFile.Write(encryptedFileData);
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
<param name = "outputPath"> The Path where the Encrypted FileSystem will be Saved. </param> */

public static void EncryptFileSystem(string inputPath, string outputPath)
{
var singleAction = GetEncryptAction(false);
var batchAction = GetEncryptAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Decrypts the specified File with Xor Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>

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

CipheredDataInfo fileCryptoInfo = CipheredDataInfo.Read(inputFile);
byte[] inputFileBytes = inputFile.ReadBytes(fileCryptoInfo.SizeBeforeEncryption);

byte[] decryptedData = CipherData(inputFileBytes, cipherKey);
Path_Helper.RemoveExtension(ref outputPath, cdatFileExt);

Archive_Manager.CheckDuplicatedPath(ref outputPath);
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

/** <summary> Decrypts all the Files of a Folder by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the Folder to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted Folder will be Saved. </param>

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

Task_Manager.BatchTask(inputPath, outputPath, decryptAction, cdatFileExt);
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
<param name = "outputPath"> The Path where the Decrypted FileSystem will be Saved. </param> */

public static void DecryptFileSystem(string inputPath, string outputPath)
{
var singleAction = GetDecryptAction(false);
var batchAction = GetDecryptAction(true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets an Encrypt Action by using the Specified Cipher Key and Batch Mode. </summary>

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

<returns> The Encrypt Action Obtained. </returns> */

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