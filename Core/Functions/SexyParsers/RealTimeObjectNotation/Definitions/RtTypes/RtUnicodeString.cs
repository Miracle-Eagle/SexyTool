using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a Unicode String in the RtSystem. </summary>

public class RtUnicodeString : RtString
{
/** <summary> Store Info about the Encoding of a Unicode String. </summary>
<returns> The Unicode String Encoding. </returns> */

private static readonly Encoding unicodeStrEncoding = Encoding.UTF32;

/// <summary> Creates a new Instance of the <c>RtUnicodeString</c>. </summary>

public RtUnicodeString()
{
Identifier = RtTypeIdentifier.Null;
}

/** <summary> Creates a new Instance of the <c>RtUnicodeString</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtUnicodeString(string sourceValue)
{

if(sourceValue == null)
Identifier = RtTypeIdentifier.Null;

else
{
Identifier = RtTypeIdentifier.UnicodeString;
Value = sourceValue;

Length = Value.Length;
LengthAfterEncoding = Length * 2;
}

}

/** <summary> Decodes a Unicode String from a RTON File. </summary>

<param name = "sourceStream"> The Stream which Contains the Data to be Decoded. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param>

<returns> The Decoded String. </returns> */

private static string DecodeString(BinaryStream sourceStream, Endian endianOrder = default)
{
int unicodeStrLength = sourceStream.ReadVarInt();
string unicodeString = sourceStream.ReadStringByVarIntLength(unicodeStrEncoding, endianOrder);

int unicodeStrLengthAfterEncoding = unicodeString.Length;

if(unicodeStrLengthAfterEncoding < unicodeStrLength)
{
string unicodeStrMismatch = GetLengthMismatchMsg(RtTypeIdentifier.UnicodeString, unicodeString, unicodeStrLength, unicodeStrLengthAfterEncoding, sourceStream.Position);
Text.PrintWarning(unicodeStrMismatch);
}

return unicodeString;
}

/** <summary> Reads a Unicode String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Determines if the UnicodeString should be Written as a PropertyName or not. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isPropertyName, Endian endianOrder = default)
{
string unicodeString = DecodeString(inputStream, endianOrder);
WriteJsonString(outputStream, unicodeString, isPropertyName);
}

/** <summary> Reads a Unicode String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isIndexed"> A boolean that Determines if the UnicodeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Determines if the UnicodeString should be Written as a PropertyName or not. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isIndexed, bool isPropertyName)
{
string unicodeString;

if(isIndexed)
{
int unicodeStrIndex = inputStream.ReadVarInt();
unicodeString = ReferenceStringsHandler.GetStringFromUnicodeList(unicodeStrIndex);
}

else
{
unicodeString = DecodeString(inputStream);
ReferenceStringsHandler.AddStringToUnicodeList(unicodeString);
}

WriteJsonString(outputStream, unicodeString, isPropertyName);
}

/** <summary> Reads a Unicode String from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetStr"> The String to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, string targetStr, Endian endianOrder = default)
{

if(ReferenceStringsHandler.ListHasUnicodeString(targetStr) )
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UnicodeString_Index);
int unicodeStrIndex = ReferenceStringsHandler.GetUnicodeStringIndex(targetStr);

outputStream.WriteVarInt(unicodeStrIndex);
}

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.UnicodeString_Value);
outputStream.WriteVarInt(targetStr.Length);

outputStream.WriteStringByVarIntLength(targetStr, unicodeStrEncoding, endianOrder);
ReferenceStringsHandler.AddStringToUnicodeList(targetStr);
}

}

}

}