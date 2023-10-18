using System;
using System.Linq;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Parser
{
/// <summary> The Info of a Real Time Object Notation (RTON) File. </summary>

public static class RtonInfo
{
/** <summary> Sets a Value which Contains Info about the expected Header of a RTON File. </summary>
<returns> The expected RTON Header. </returns> */

private static readonly string expectedHeader = "RTON";

/** <summary> Gets or Sets a Value which Contains Info about the Version of File. </summary>
<returns> The File Version. </returns> */

private static uint versionInfo = (uint)Endian.None;

/** <summary> Sets a Value which Contains Info about the expected Tail of a RTON File. </summary>
<returns> The expected RTON Tail. </returns> */

private static readonly string expectedTail = "DONE";

/** <summary> Reads the Header of a Real Time Object Notation (RTON) File. </summary>
<param name = "targetStream"> The Stream to be Read. </param> */

public static void ReadFileHeader(BinaryStream targetStream) => targetStream.CompareString(expectedHeader);

/** <summary> Reads the Version Info of a Real Time Object Notation (RTON) File. </summary>

<param name = "targetStream"> The Stream to be Read. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param>

<returns> The endian Encoding related to the Version read. </returns> */

public static Endian ReadVersionInfo(BinaryStream targetStream)
{
var validVersions = Enum.GetValues(typeof(Endian) ) as Endian[];
versionInfo = targetStream.ReadUInt();

if(versionInfo < (uint)validVersions.Min() || versionInfo > (uint)validVersions.Max() )
throw new NotSupportedException(string.Format("This File is Encoded with a Unsupported Version (v{0}.0)", versionInfo) );

return (Endian)versionInfo;
}

/** <summary> Reads the Tail of a Real Time Object Notation (RTON) File. </summary>
<param name = "targetStream"> The Stream to be Read. </param> */

public static void ReadFileTail(BinaryStream targetStream) => targetStream.CompareString(expectedTail);

/** <summary> Writes the Header to a Real Time Object Notation (RTON) File. </summary>
<param name = "targetStream"> The Stream where the Data will be Written. </param> */

public static void WriteFileHeader(BinaryStream targetStream) => targetStream.WriteString(expectedHeader);

/** <summary> Writes the Version Info to a Real Time Object Notation (RTON) File. </summary>

<param name = "targetStream"> The Stream where the Data will be Written. </param>
<param name = "endianOrder"> The endian Order of the RTON Data. </param> */

public static void WriteVersionInfo(BinaryStream targetStream, Endian endianOrder)
{
versionInfo = (uint)endianOrder;
targetStream.WriteUInt(versionInfo);
}

/** <summary> Writes the Tail to a Real Time Object Notation (RTON) File. </summary>
<param name = "targetStream"> The Stream where the Data will be Written. </param> */

public static void WriteFileTail(BinaryStream targetStream) => targetStream.WriteString(expectedTail);
}

}