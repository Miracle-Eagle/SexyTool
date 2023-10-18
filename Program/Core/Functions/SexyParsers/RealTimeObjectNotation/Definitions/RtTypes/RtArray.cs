using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents an Array in the RtSystem. </summary>

public class RtArray : RtClass<Array>
{
/// <summary> Creates a new Instance of the <c>RtArray</c>. </summary>

public RtArray()
{
Identifier = RtTypeIdentifier.Array;
}

/** <summary> Creates a new Instance of the <c>RtInt64</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtArray(Array sourceValue)
{

if(sourceValue == null)
Identifier = RtTypeIdentifier.Null;

else
{
Identifier = RtTypeIdentifier.Array;
Value = sourceValue;

Size = Value.Length;
}

}

/** <summary> Reads an Array from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, Endian endianOrder = default)
{
inputStream.CompareByte( (byte)RtTypeIdentifier.Array_Start);
outputStream.WriteStartArray();

int elementsCount = inputStream.ReadVarInt();
RtTypeIdentifier elementType;

for(int i = 0; i < elementsCount; i++)
{
elementType = (RtTypeIdentifier)inputStream.ReadByte();
RtObject.Decode(inputStream, outputStream, elementType, endianOrder);
}

inputStream.CompareByte( (byte)RtTypeIdentifier.Array_End);
outputStream.WriteEndArray();
}

/** <summary> Reads an Array from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceData"> The JSON Array to be Read. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, JsonElement sourceArray, Endian endianOrder = default)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Array_Start);
int elementsCount = sourceArray.GetArrayLength();

outputStream.WriteVarInt(elementsCount);

for(int i = 0; i < elementsCount; i++)
RtObject.Encode(outputStream, sourceArray[i], endianOrder);

outputStream.WriteByte( (byte)RtTypeIdentifier.Array_End);
}

}

}