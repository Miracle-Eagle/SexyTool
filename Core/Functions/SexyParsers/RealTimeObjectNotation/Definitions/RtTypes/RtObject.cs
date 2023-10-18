using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an Object in the RtSystem. </summary>

public class RtObject : RtBaseType<object>
{
/// <summary> Creates a new Instance of the <c>RtObject</c>. </summary>

public RtObject()
{
Identifier = RtTypeIdentifier.Null;
}

/** <summary> Creates a new Instance of the <c>RtObject</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtObject(object sourceValue)
{

if(sourceValue == null)
Identifier = RtTypeIdentifier.Null;

else
{
Identifier = RtTypeIdentifier.Object_Start;
Value = sourceValue;
}

}

/** <summary> Encodes a Object from a JSON File and writes its Contents to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceData"> The JSON Data to be Read. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Encode(BinaryStream outputStream, JsonElement sourceData, Endian endianOrder = default)
{
JsonValueKind valueKind = sourceData.ValueKind;

switch(valueKind)
{
case JsonValueKind.Undefined:
Text.PrintWarning($"{valueKind} values are Skipped when Encoding JSON Files.");
break;

case JsonValueKind.Object:
outputStream.WriteByte( (byte)RtTypeIdentifier.Object_Start);

Write(outputStream, sourceData, endianOrder);
break;

case JsonValueKind.Array:
outputStream.WriteByte( (byte)RtTypeIdentifier.Array);

RtArray.Write(outputStream, sourceData, endianOrder);
break;

case JsonValueKind.String:
string jsonString = sourceData.GetString();

RtString.EvaluateStringType(outputStream, jsonString, endianOrder);
break;

case JsonValueKind.Number:
RtStruct<double>.EvaluateNumericValue(outputStream, sourceData, endianOrder);
break;

case JsonValueKind.True:

case JsonValueKind.False:
RtBoolean.Write(outputStream, valueKind);
break;

case JsonValueKind.Null:
outputStream.WriteByte( (byte)RtTypeIdentifier.Null);
break;

default:
throw new DataMisalignedException($"Unknown Json Token: \"{valueKind}\"");
}

}

/** <summary> Decodes a Object from a RTON File and writes its Contents to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Decode(BinaryStream inputStream, Utf8JsonWriter outputStream, RtTypeIdentifier sourceID,  Endian endianOrder = default)
{

switch(sourceID)
{
case RtTypeIdentifier.Bool_false:

case RtTypeIdentifier.Bool_true:
RtBoolean.Read(outputStream, sourceID);
break;

case RtTypeIdentifier.Null:
outputStream.WriteStringValue("\\*");
break;

case RtTypeIdentifier.Byte:

case RtTypeIdentifier.Byte_0:
RtUInt8.Read(inputStream, outputStream, ref sourceID);
break;

case RtTypeIdentifier.SByte:

case RtTypeIdentifier.SByte_0:
RtInt8.Read(inputStream, outputStream, ref sourceID);
break;

case RtTypeIdentifier.Short:

case RtTypeIdentifier.Short_0:
RtInt16.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.UShort:

case RtTypeIdentifier.UShort_0:
RtUInt16.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.Int:

case RtTypeIdentifier.Int_0:
RtInt32.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.Float:

case RtTypeIdentifier.Float_0:
RtFloat32.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.VarInt:
RtVarInt32.Read(inputStream, outputStream);
break;

case RtTypeIdentifier.ZigZagInt:
RtZigZagInt32.Read(inputStream, outputStream);;
break;

case RtTypeIdentifier.UInt:

case RtTypeIdentifier.UInt_0:
RtUInt32.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.UVarInt:
RtUVarInt32.Read(inputStream, outputStream);
break;

case RtTypeIdentifier.Long:

case RtTypeIdentifier.Long_0:
RtInt64.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.Double:

case RtTypeIdentifier.Double_0:
RtFloat64.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.VarLong:
RtVarInt64.Read(inputStream, outputStream);
break;

case RtTypeIdentifier.ZigZagLong:
RtZigZagInt64.Read(inputStream, outputStream);
break;

case RtTypeIdentifier.ULong:

case RtTypeIdentifier.ULong_0:
RtUInt64.Read(inputStream, outputStream, ref sourceID, endianOrder);
break;

case RtTypeIdentifier.UVarLong:
RtUVarInt64.Read(inputStream, outputStream);
break;

case RtTypeIdentifier.NativeString:
RtNativeString.Read(inputStream, outputStream, false, endianOrder);
break;

case RtTypeIdentifier.UnicodeString:
RtUnicodeString.Read(inputStream, outputStream, false, endianOrder);
break;

case RtTypeIdentifier.IDString:

case RtTypeIdentifier.IDString_Null:
RtIDString.Read(inputStream, outputStream, false, ref sourceID);
break;

case RtTypeIdentifier.Object_Start:
Read(inputStream, outputStream, endianOrder);
break;

case RtTypeIdentifier.Array:
RtArray.Read(inputStream, outputStream, endianOrder);
break;

case RtTypeIdentifier.BinaryString:
RtBinaryString.Read(inputStream, outputStream, false);
break;

case RtTypeIdentifier.NativeString_Value:
RtNativeString.Read(inputStream, outputStream, false, false);
break;

case RtTypeIdentifier.NativeString_Index:
RtNativeString.Read(inputStream, outputStream, true, false);
break;

case RtTypeIdentifier.UnicodeString_Value:
RtUnicodeString.Read(inputStream, outputStream, false, false);
break;

case RtTypeIdentifier.UnicodeString_Index:
RtUnicodeString.Read(inputStream, outputStream, true, false);
break;

case RtTypeIdentifier.ObjectWithBoolean:
ReadBoolean(inputStream, outputStream);
break;

default:
throw new DataMisalignedException($"Unknown Value Type: \"{(byte)sourceID}\" at Address: {inputStream.Position}");
}

}

/** <summary> Reads an Object from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, Endian endianOrder = default)
{
outputStream.WriteStartObject();

while(true)
{
var objIdentifier = (RtTypeIdentifier)inputStream.ReadByte();

if(objIdentifier == RtTypeIdentifier.Object_End)
break;

switch(objIdentifier)
{
case RtTypeIdentifier.Null:
outputStream.WritePropertyName("\\*");
break;

case RtTypeIdentifier.NativeString:
RtNativeString.Read(inputStream, outputStream, true, endianOrder);
break;

case RtTypeIdentifier.UnicodeString:
RtUnicodeString.Read(inputStream, outputStream, true, endianOrder);
break;

case RtTypeIdentifier.IDString:

case RtTypeIdentifier.IDString_Null:
RtIDString.Read(inputStream, outputStream, true, ref objIdentifier);
break;

case RtTypeIdentifier.BinaryString:
RtBinaryString.Read(inputStream, outputStream, true);
break;

case RtTypeIdentifier.NativeString_Value:
RtNativeString.Read(inputStream, outputStream, false, true);
break;

case RtTypeIdentifier.NativeString_Index:
RtNativeString.Read(inputStream, outputStream, true, true);
break;

case RtTypeIdentifier.UnicodeString_Value:
RtUnicodeString.Read(inputStream, outputStream, false, true);
break;

case RtTypeIdentifier.UnicodeString_Index:
RtUnicodeString.Read(inputStream, outputStream, true, true);
break;

default:
throw new DataMisalignedException($"Unknown Object Type: \"{(byte)objIdentifier}\" at Address: {inputStream.Position}");
}

objIdentifier = (RtTypeIdentifier)inputStream.ReadByte();
Decode(inputStream, outputStream, objIdentifier, endianOrder);
}

outputStream.WriteEndObject();
}

/** <summary> Reads a Boolean from a RtObject and Writes its Representation to a JsonObject. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param> */

private static void ReadBoolean(BinaryStream inputStream, Utf8JsonWriter outputStream)
{
bool boolValue = inputStream.ReadBool();
outputStream.WriteBooleanValue(boolValue);
}

/** <summary> Reads an Object from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "jsonData"> The JSON Data to be Read. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, JsonElement jsonData, Endian endianOrder = default)
{
var jsonPropList = jsonData.EnumerateObject();

foreach(JsonProperty singleProperty in jsonPropList)
{
RtString.EvaluateStringType(outputStream, singleProperty.Name, endianOrder);
Encode(outputStream, singleProperty.Value, endianOrder);
}

outputStream.WriteByte( (byte)RtTypeIdentifier.Object_End);
}

}

}