using System;
using System.Security.Cryptography;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Handling Functions for Parameters that are used on Data Encryption or Decryption. </summary>

internal static class Crypto_Parameters
{
/** <summary> Sets a Value which Contains Info about the default Size of an Array. </summary>
<returns> The default Size. </returns> */

private static readonly Limit<int> defaultSize = new(1, Array.MaxLength);

/** <summary> Checks if the Size of the given Data Block meets the expected Range. </summary>

<param name = "blockData"> The Data Block to be Validated. </param>
<param name = "expectedSize"> The Size Excepted. </param> 

<returns> The Block Size. </returns> */

private static int CheckBlockSize(byte[] blockData, Limit<int> expectedSize)
{
expectedSize ??= defaultSize;
int blockSize;

if(blockData.Length < expectedSize.MinValue)
{
Input_Manager.FillArray(ref blockData, expectedSize.MinValue);
blockSize = expectedSize.MinValue;
}

else if(blockData.Length > expectedSize.MaxValue)
blockSize = expectedSize.MaxValue;

else
blockSize = blockData.Length;

return blockSize;
}

/** <summary> Checks if the providen Cipher Key meets the expected Size. </summary>
<remarks> In case the Cipher Key doesn't meet the expected Size, a similar will be Generated instead. </remarks>

<param name = "sourceKey"> The Key to be Validated. </param>
<param name = "expectedSize"> The Key Size Excepted. </param> */

public static void CheckKeySize(byte[] sourceKey, Limit<int> expectedSize)
{
sourceKey ??= Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_GENERIC);
expectedSize ??= defaultSize;

if(sourceKey.Length < expectedSize.MinValue)
Input_Manager.FillArray(ref sourceKey, expectedSize.MinValue);

else if(sourceKey.Length > expectedSize.MaxValue)
Array.Resize(ref sourceKey, expectedSize.MaxValue);
	
else
return;

}

/** <summary> Checks if the Number of Iterations is in the expected Range. </summary>
<remarks> In case the Iterations Count doesn't meet the expected Range, a default Value will be Set. </remarks>

<param name = "sourceValue"> The Value to be Validated. </param>
<param name = "expectedRange"> The expected Range of Iterations. </param> */

public static void CheckIterationsCount(ref int sourceValue, Limit<int> expectedRange)
{
expectedRange ??= new(1);

if(sourceValue < expectedRange.MinValue)
sourceValue = expectedRange.MinValue;

else if(sourceValue > expectedRange.MaxValue)
sourceValue = expectedRange.MaxValue;

else
return;

}

/** <summary> Generates a derived Key from an Existing one, by Performing some Iterations. </summary>

<param name = "cipherKey"> The Cipher Key to Derive. </param>
<param name = "saltValue"> The Salt Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "expectedKeySize"> The Key Size Excepted. </param>
<param name = "expectedIterations"> The expected Number of Iterations. </param>

<returns> The derived Cipher Key. </returns> */

public static byte[] CipherKeySchedule(byte[] cipherKey, byte[] saltValue, string hashType, int iterationsCount, Limit<int> expectedKeySize = null, Limit<int> expectedIterations = null)
{
CheckKeySize(cipherKey, expectedKeySize);
CheckIterationsCount(ref iterationsCount, expectedIterations);

PasswordDeriveBytes derivedCipherKey = new(cipherKey, saltValue, hashType, iterationsCount);
int derivedKeySize = CheckBlockSize(cipherKey, expectedKeySize);

return derivedCipherKey.GetBytes(derivedKeySize);
}

/** <summary> Initializates a Vector from a Cipher Key. </summary>

<param name = "cipherKey"> The Cipher Key to be used. </param>
<param name = "expectedVectorSize"> The Vector Size Excepted. </param>
<param name = "vectorIndex"> Specifies where the Vector should Start Copying the Bytes from the Cipher Key (Default Index is 0). </param>

<returns> The IV that was Generated. </returns> */

public static byte[] InitVector(byte[] cipherKey, Limit<int> expectedVectorSize, int vectorIndex = 0)
{
int vectorSize = CheckBlockSize(cipherKey, expectedVectorSize);
byte[] IV = new byte[vectorSize];

if(cipherKey.Length == vectorSize)
Array.Reverse(IV);

else
Array.Copy(cipherKey, vectorIndex, IV, 0, vectorSize);

return IV;
}

}

}