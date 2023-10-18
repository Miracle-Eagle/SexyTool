using SexyTool.Program.Core.Functions.StringHashers;
using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.ArchiveHashers
{
/// <summary> Initializes Secure Hash Algorithm-512 (SHA-512) functions for Files. </summary>

public class SHA512_Hasher : Sha512StringDigest
{
/** <summary> Hashes a File by using SHA-512 Digest. </summary>

<param name = "inputPath"> The Path where the File to be Hashed is Located. </param>
<param name = "outputPath"> The Path where the Hashed File will be Saved. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void HashFile(string inputPath, string outputPath, bool useAuthCode, byte[] cipherKey)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_CALCULATE_FILE_HASH, Path.GetFileName(inputPath) );
using FileStream inputFile = File.OpenRead(inputPath);

string hashedString = DigestData(inputFile, useAuthCode, cipherKey);
File.WriteAllText(outputPath, hashedString);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_HASH_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_HASH_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Hashes all the Files of a Folder by using SHA-512 Digest. </summary>

<param name = "inputPath"> ThePath where the Folder to be Hashed is Located. </param>
<param name = "outputPath"> The Location where the Hashed Folder will be Saved. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

protected static void HashFolder(string inputPath, string outputPath, bool useAuthCode, byte[] cipherKey)
{

try
{
Text.PrintLine(true, Text.LocalizedData.ACTION_CALCULATE_FOLDER_HASHES, Directory_Manager.GetFolderName(inputPath) );
var hashAction = GetHashAction(useAuthCode, cipherKey, false);

Task_Manager.BatchTask(inputPath, outputPath, hashAction);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_HASH_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_HASH_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Hashes a FileSystem by using SHA-512 Digest. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Hashed is Located. </param>
<param name = "outputPath"> The Path where the Hashed FileSystem will be Saved. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>*/

public static void HashFileSystem(string inputPath, string outputPath, bool useAuthCode, byte[] cipherKey = null)
{
var singleAction = GetHashAction(useAuthCode, cipherKey, false);
var batchAction = GetHashAction(useAuthCode, cipherKey, true);

Task_Manager.PerformSystemAction(inputPath, outputPath, singleAction, batchAction);
}

/** <summary> Gets a Hash Action by using the Specified Batch Mode. </summary>

<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>
<param name = "isBatchAction"> A boolean that determines if the Action should be Performed in Batches. </param>

<returns> The Hash Action Obtained. </returns> */

private static Action<string, string> GetHashAction(bool useAuthCode, byte[] cipherKey, bool isBatchAction)
{
ActionWrapper<string, string> hashAction;

if(isBatchAction)
{
hashAction = new( (inputPath, outputPath) => HashFolder(inputPath, outputPath, useAuthCode, cipherKey) );
}

else
{
hashAction = new( (inputPath, outputPath) => HashFile(inputPath, outputPath, useAuthCode, cipherKey) );
}

return hashAction.Init;
}

}

}