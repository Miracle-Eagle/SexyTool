using SexyTool.Program.Graphics.UserSelections;

namespace SexyTool.Program.Core.Functions.Other
{
/// <summary> Initializes ciphering Functions for Integer values. </summary>

public class IntegersGuard
{
/** <summary> Sets a Value which Contains Info about the Cipher Key used for Encrypting/Decrypting Values. </summary>
<returns> The Cipher Key. </returns> */

private const byte cipherKey = 13;

/** <summary> Sets a Value which Contains Info about the Logic Factor used for Encrypting/Decrypting Values. </summary>
<returns> The Logic Factor. </returns> */

private const byte logicFactor = 0x1F;

/** <summary> Sets a Value which Contains Info about the expected Base of an Integer value. </summary>
<returns> The expected Integer Base. </returns> */

private const int expectedBase = 32;

/** <summary> Sets a Value which Contains Info about the BitMask used for Encrypting/Decrypting Values. </summary>
<returns> The BitMask Value. </returns> */

private const byte bitMask = 0xFF;

/** <summary> Initializes a Instance for the <c>GenericSelection</c> of Values. </summary>
<returns> The values Selector. </returns> */

private static readonly GenericSelection valuesSelector = Interface.GetUserSelection<GenericSelection>();

/// <summary> Displays the Encryption of an Integer Value. </summary>

internal static void DisplayIntEncryption(uint targetValue)
{
uint targetValue = (uint)valuesSelector.GetGenericParams<int>(Text.LocalizedData.ADVICE_ENTER_PLAIN_INTEGER, Text.LocalizedData.PARAM_PLAIN_INTEGER);
int encryptedValue = EncryptValue(targetValue);

Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_ENCRYPTED_VALUE, encryptedValue) );
}

/// <summary> Displays the Decryption of an Integer Value. </summary>

internal static void DisplayIntDecryption(uint targetValue)
{
uint targetValue = (uint)valuesSelector.GetGenericParams<int>(Text.LocalizedData.ADVICE_ENTER_ENCRYPTED_INTEGER, Text.LocalizedData.PARAM_ENCRYPTED_INTEGER);
int decryptedValue = DecryptValue(targetValue);

Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_DECRYPTED_VALUE, decryptedValue) );
}

/** <summary> Encrypts an Integer value by Performing some bitwise Operations on it. </summary>
<param name = "targetValue"> The Value to be Encrypted. </param>

<exception cref = "ArithmeticException"></exception>
<exception cref = "OverflowException"></exception>

<returns> The Encrypted Value. </returns> */

public static int EncryptValue(uint targetValue)
{
uint xorValue = targetValue ^ cipherKey;
int highBitsRate = cipherKey & logicFactor;

uint bigInteger = xorValue << highBitsRate;
int baseDifference = expectedBase - highBitsRate;

int lowBitsRate = baseDifference & bitMask;
uint smallInteger = xorValue >> lowBitsRate;

return (int)(bigInteger | smallInteger);
}

/** <summary> Decrypts an Integer value by Performing some bitwise Operations on it. </summary>
<param name = "targetValue"> The Value to be Decrypted. </param>

<exception cref = "ArithmeticException"></exception>
<exception cref = "OverflowException"></exception>

<returns> The Decrypted Value. </returns> */

public static int DecryptValue(uint targetValue)
{
int lowBitsRate = cipherKey & logicFactor;
uint smallInteger = targetValue >> lowBitsRate;

int baseDifference = expectedBase - lowBitsRate;
int highBitsRate = baseDifference & bitMask;

uint bigInteger = targetValue << highBitsRate;
int xnorValue = (int)(smallInteger | bigInteger);

return cipherKey ^ xnorValue;
}

}

}