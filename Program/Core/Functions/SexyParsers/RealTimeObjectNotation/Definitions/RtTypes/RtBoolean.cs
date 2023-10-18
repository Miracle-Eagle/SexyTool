using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System.Text.Json;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a Boolean in the RtSystem. </summary>

public class RtBoolean : RtStruct<bool>
{
/// <summary> Creates a new Instance of the <c>RtBoolean</c>. </summary>

public RtBoolean()
{
Identifier = RtTypeIdentifier.ObjectWithBoolean;
Value = default;
}

/** <summary> Creates a new Instance of the <c>RtBoolean</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtBoolean(bool sourceValue)
{
Identifier = sourceValue ? RtTypeIdentifier.Bool_true : RtTypeIdentifier.Bool_false;
}

/** <summary> Reads a Boolean from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param> */

public static void Read(Utf8JsonWriter outputStream, RtTypeIdentifier sourceID)
{
bool boolValue = sourceID.Equals(RtTypeIdentifier.Bool_true);
outputStream.WriteBooleanValue(boolValue);
}

/** <summary> Reads a Boolean from a JSON File and writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceType"> The Type of the JSON Value. </param> */

public static void Write(BinaryStream outputStream, JsonValueKind sourceType)
{
var valueIdentifier = (sourceType == JsonValueKind.True) ? RtTypeIdentifier.Bool_true : RtTypeIdentifier.Bool_false;
outputStream.WriteByte( (byte)valueIdentifier);
}

}

}