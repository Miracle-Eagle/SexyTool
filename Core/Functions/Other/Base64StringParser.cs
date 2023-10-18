using System;

namespace SexyTool.Program.Core.Functions.Other
{
/// <summary> Initializes Base64 Encoding and Decoding functions for Strings. </summary>

public class Base64StringParser
{
/** <summary> Displays the Encoding of a String by using Base64. </summary>

<param name = "inputString"> The String to Encode. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param> */

internal static void DisplayStringEncoding(string inputString, bool isWebSafe)
{
string encodedString = EncodeBytes(Console.InputEncoding.GetBytes(inputString), isWebSafe);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_ENCODED_STRING, encodedString) );
}

/** <summary> Displays the Decoding of a String by using Base64. </summary>

<param name = "inputBytes"> The String to Decode. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param> */

internal static void DisplayStringDecoding(string inputString, bool isWebSafe)
{
byte[] decodedBytes = DecodeString(inputString, isWebSafe);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_DECODED_STRING, Console.OutputEncoding.GetString(decodedBytes) ) );
}

/** <summary> Encodes an Array of Bytes as a Base64 String. </summary>

<param name = "inputBytes"> The Bytes to be Encoded. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FormatException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The Base64 String. </returns> */

protected static string EncodeBytes(byte[] inputBytes, bool isWebSafe)
{
string encodedString = Convert.ToBase64String(inputBytes);

if(isWebSafe)
{
encodedString.TrimEnd('=').Replace('+', '-').Replace('/', '_'); 
}

return encodedString;
}

/** <summary> Decodes a Base64 String as an Array of Bytes. </summary>

<param name = "inputString"> The String to be Decoded. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FormatException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The Bytes Decoded. </returns> */

protected static byte[] DecodeString(string inputString, bool isWebSafe)
{
	
if(isWebSafe)
{
inputString.Replace('_', '/').Replace('-', '+');
int trailingSum = inputString.Length % 4;

switch(trailingSum)
{
case 2:
inputString += '=' + '=';
break;

case 3:
inputString += '=';
break;
}

}

return Convert.FromBase64String(inputString);
}

}

}