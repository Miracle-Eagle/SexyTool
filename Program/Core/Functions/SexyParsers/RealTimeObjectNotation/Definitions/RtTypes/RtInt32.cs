using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a 32-bits signed Integer in the RtSystem. </summary>

public class RtInt32 : RtStruct<int>
{
/// <summary> Creates a new Instance of the <c>RtInt32</c>. </summary>

public RtInt32()
{
Identifier = RtTypeIdentifier.Int_0;
}

/** <summary> Creates a new Instance of the <c>RtInt32</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtInt32(int sourceValue)
{

if(sourceValue == 0)
Identifier = RtTypeIdentifier.Int_0;

else
{
Identifier = RtTypeIdentifier.Int;
Value = sourceValue;
}

}

/** <summary> Reads a 32-bits signed Integer from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, ref RtTypeIdentifier sourceID, Endian endianOrder = default)
{
int int32Value = (sourceID == RtTypeIdentifier.Int) ? inputStream.ReadInt(endianOrder) : 0;
outputStream.WriteNumberValue(int32Value);
}

/** <summary> Reads a 32-bits signed Integer from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "targetValue"> The Int32 to be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void Write(BinaryStream outputStream, int targetValue, Endian endianOrder = default)
{

if(targetValue == 0)
outputStream.WriteByte( (byte)RtTypeIdentifier.Int_0);

else
{
outputStream.WriteByte( (byte)RtTypeIdentifier.Int);
outputStream.WriteInt(targetValue, endianOrder);
}

}

}

}