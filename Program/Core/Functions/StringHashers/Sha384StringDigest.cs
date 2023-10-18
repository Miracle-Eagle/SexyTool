using SexyTool.Program.Graphics.Dialogs;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SexyTool.Program.Core.Functions.StringHashers
{
/// <summary> Initializes Secure Hash Algorithm (SHA-384) functions for Strings. </summary>

public class Sha384StringDigest
{
/** <summary> Creates a new Instance of the SHA-384 Algorithm. </summary>
<returns> The SHA-384 Algorithm. </returns> */

private static HashAlgorithm digestAlgorithm = SHA384.Create();

/** <summary> Sets a Value which Contains Info about the Size a Cipher Key must Have. </summary>
<returns> The Cipher Key Size. </returns> */

public static readonly Limit<int> cipherKeySize = new(48, 64);

/** <summary> Displays the SHA-384 Digest of a String. </summary>

<param name = "inputString"> The String to be Hashed. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param> */

internal static void DisplayStringDigest(string inputString, bool useAuthCode, byte[] cipherKey = null)
{

if(useAuthCode)
cipherKey = Interface.GetDialog<CipherKeyDialog>().Popup() as byte[];

string hashedString = DigestData(Console.InputEncoding.GetBytes(inputString), useAuthCode, cipherKey);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_HASHED_STRING, hashedString) );
}

/** <summary> Hashes an Array of Bytes by using SHA-384 Digest. </summary>

<param name = "inputBytes"> The Bytes to be Hashed. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The SHA-384 Digest. </returns> */

public static string DigestData(byte[] inputBytes, bool useAuthCode, byte[] cipherKey = null)
{
byte[] hashedBytes;

if(useAuthCode)
{
Crypto_Parameters.CheckKeySize(cipherKey, cipherKeySize);
digestAlgorithm = new HMACSHA384(cipherKey);

hashedBytes = digestAlgorithm.ComputeHash(inputBytes);
}

else
hashedBytes = digestAlgorithm.ComputeHash(inputBytes);

return Input_Manager.ConvertHexString(hashedBytes);
}

/** <summary> Hashes a Stream by using SHA-384 Digest. </summary>

<param name = "inputStream"> The Stream which contains the Data to be Hashed. </param>
<param name = "useAuthCode"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "ObjectDisposedException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The SHA-384 Digest. </returns> */

protected static string DigestData(Stream inputStream, bool useAuthCode, byte[] cipherKey = null)
{
byte[] hashedBytes;

if(useAuthCode)
{
Crypto_Parameters.CheckKeySize(cipherKey, cipherKeySize);
digestAlgorithm = new HMACSHA384(cipherKey);

hashedBytes = digestAlgorithm.ComputeHash(inputStream);
}

else
hashedBytes = digestAlgorithm.ComputeHash(inputStream);

return Input_Manager.ConvertHexString(hashedBytes);
}

}

}