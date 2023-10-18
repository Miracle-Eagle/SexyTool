using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a Native String in the RtSystem. </summary>

public class RtNativeString : RtString
{
/** <summary> Store Info about the Encoding of a Native String. </summary>
<returns> The Native String Encoding. </returns> */

private static readonly Encoding nativeStrEncoding = Encoding.Default;

/** <summary> Creates a new Instance of the <c>RtNativeString</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtNativeString(string sourceValue)
{

if(sourceValue == null)
Identifier = RtTypeIdentifier.Null;

else
{
Identifier =  RtTypeIdentifier.NativeString;
Value = sourceValue;

Length = Value.Length;
}

}

/** <summary> Reads a Native String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Determines if the NativeString should be Written as a PropertyName or not. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isPropertyName, Endian endianOrder = default)
{
string nativeString = inputStream.ReadStringByVarIntLength(nativeStrEncoding, endianOrder);
WriteJsonString(outputStream, nativeString, isPropertyName);
}

/** <summary> Reads a Native String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isIndexed"> A boolean that Determines if the NativeString is in the Reference List or not. </param>
<param name = "isPropertyName"> Determines if the NativeString should be Written as a PropertyName or not. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isIndexed, bool isPropertyName, Endian endianOrder = default)
{
string nativeString;

if(isIndexed)
{
int nativeStrIndex = inputStream.ReadVarInt();
nativeString = ReferenceStringsHandler.GetStringFromNativeList(nativeStrIndex);
}

else
{
nativeString = inputStream.ReadStringByVarIntLength(nativeStrEncoding, endianOrder);
ReferenceStringsHandler.AddStringToNativeList(nativeString);
}

WriteJsonString(outputStream, nativeString, isPropertyName);
}

/** <summary> Reads a Native String from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetStr"> The String to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, string targetStr, Endian endianOrder = default)
{

if(ReferenceStringsHandler.ListHasNativeString(targetStr) )
{
outputStream.WriteByte( (byte)RtTypeIdentifier.NativeString_Index);
int nativeStrIndex = ReferenceStringsHandler.GetNativeStringIndex(targetStr);

outputStream.WriteVarInt(nativeStrIndex);
}

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.NativeString_Value);
outputStream.WriteStringByVarIntLength(targetStr, nativeStrEncoding, endianOrder);

ReferenceStringsHandler.AddStringToNativeList(targetStr);
}

}

}

}