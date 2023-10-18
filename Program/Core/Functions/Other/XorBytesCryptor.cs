using System;

namespace SexyTool.Program.Core.Functions.Other
{
/// <summary> Initializes Xor Ciphering functions for Bytes. </summary>

public class XorBytesCryptor
{
/** <summary> Displays the Encryption of a String by using XOR Ciphering. </summary>

<param name = "inputString"> The String to be Encrypted. </param>
<param name = "cipherKey"> The Cipher Key to be Used. </param> */

internal static void DisplayStringEncryption(string inputString, byte[] cipherKey)
{
byte[] encryptedBytes = CipherData(Console.InputEncoding.GetBytes(inputString), cipherKey);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_ENCRYPTED_STRING, Input_Manager.ConvertHexString(encryptedBytes) ) );
}

/**<summary> Displays the Decryption of a String by using XOR Ciphering. </summary>

<param name = "inputString"> The String to be Decrypted. </param>
<param name = "cipherKey"> The Cipher Key to be Used. </param> */

internal static void DisplayStringDecryption(string inputString, byte[] cipherKey)
{
byte[] decryptedBytes = CipherData(Console.InputEncoding.GetBytes(inputString), cipherKey);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_DECRYPTED_STRING, Console.OutputEncoding.GetString(decryptedBytes) ) );
}

/** <summary> Ciphers an Array of Bytes by using the XOR Algorithm. </summary>
<remarks> Passing an Array of plain Bytes to this Method, will Output the encrypted Bytes; otherwise, the decrypted Bytes. </remarks>

<param name = "inputBytes"> The Bytes to be Ciphered. </param>
<param name = "cipherKey"> The Cipher Key to be Used. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "NullReferenceException"></exception>

<returns> The Ciphered Data. </returns> */

protected static byte[] CipherData(byte[] inputBytes, byte[] cipherKey)
{
byte[] cipheredData = new byte[inputBytes.Length];

for(int i = 0; i < inputBytes.Length; i++)
cipheredData[i] = (byte)(inputBytes[i] ^ cipherKey[i % cipherKey.Length] );

return cipheredData;
}

}

}