using SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.RtTypes
{
/// <summary> Represents a ID reference in the RtSystem. </summary>

public class RtIDString : RtString
{
/** <summary> Gets a Value that Contains info about the reference Type found on a RTID. </summary>
<returns> The Reference Type. </returns> */

protected RtIDType ReferenceType;

/// <summary> Creates a new Instance of the <c>RtIDString</c>. </summary>

public RtIDString()
{
Identifier = RtTypeIdentifier.IDString_Null;
ReferenceType = RtIDType.NullReference;
}

/** <summary> Creates a new Instance of the <c>RtIDString</c> with the specific Value. </summary>
<param name = "sourceValue"> The Value this instance should hold. </param> */

public RtIDString(string sourceValue)
{

if(sourceValue == null)
{
Identifier = RtTypeIdentifier.IDString_Null;
ReferenceType = RtIDType.NullReference;
}

else
{
Identifier = RtTypeIdentifier.IDString;
Value = sourceValue;

Length = Value.Length;
LengthAfterEncoding = Length * 2;
}

}

/** <summary> Checks if the providen String is in the expected Pattern (RTID). </summary>

<param name = "targetStr"> The String to be Analized. </param>

<returns> The result from the expression Match. </returns> */

public static Match PerformRegex(string targetStr) => Regex.Match(targetStr, "^RTID\\((.*)@(.*)\\)$");

/** <summary> Reads a RtID String from a RTON File and Writes its Representation to a JSON File. </summary>

<param name = "inputStream"> The Stream which Contains the RTON Data to be Read. </param>
<param name = "outputStream"> The Stream where the JSON Data will be Written. </param>
<param name = "isPropertyName"> Determines if the UnicodeString should be Written as a PropertyName or not. </param>
<param name = "sourceID"> The Identifier of the RTON Value. </param> */


public static void Read(BinaryStream inputStream, Utf8JsonWriter outputStream, bool isPropertyName, ref RtTypeIdentifier sourceID)
{
string rtIDStrMismatch;
string idString;

if(sourceID == RtTypeIdentifier.IDString)
{
RtIDType idFlags = (RtIDType)inputStream.ReadByte();

switch(idFlags)
{
case RtIDType.NullReference:
idString = RtIDFormat.nullReferenceStr;
break;

case RtIDType.UidReference:
int uidRefStringLength = inputStream.ReadVarInt();

string uidRefString = inputStream.ReadStringByVarIntLength();
int uidRefStringLengthAfterEncoding = uidRefString.Length;

if(uidRefStringLengthAfterEncoding < uidRefStringLength)
{
rtIDStrMismatch = GetRtIDLengthMismatchMsg(idFlags, uidRefString, uidRefStringLength, uidRefStringLengthAfterEncoding, inputStream.Position);
Text.PrintWarning(rtIDStrMismatch);
}

int firstUidDigits = inputStream.ReadVarInt();
int secondUidDigits = inputStream.ReadVarInt();

uint thirdUidDigits = inputStream.ReadUInt();
idString = string.Format(RtIDFormat.uidReferenceStr, secondUidDigits, firstUidDigits, thirdUidDigits, uidRefString);
break;

case RtIDType.AliasReference:
int aliasRefStringLength = inputStream.ReadVarInt();

string aliasRefString = inputStream.ReadStringByVarIntLength();
int aliasRefStringLengthAfterEncoding = aliasRefString.Length;

if(aliasRefStringLengthAfterEncoding < aliasRefStringLength)
{
rtIDStrMismatch = GetRtIDLengthMismatchMsg(idFlags, aliasRefString, aliasRefStringLength, aliasRefStringLengthAfterEncoding, inputStream.Position);
Text.PrintWarning(rtIDStrMismatch);
}

int aliasStringLength = inputStream.ReadVarInt();
string aliasString = inputStream.ReadStringByVarIntLength();

int aliasStringLengthAfterEncoding = aliasString.Length;

if(aliasStringLengthAfterEncoding < aliasStringLength)
{
rtIDStrMismatch = GetRtIDLengthMismatchMsg(idFlags, aliasString, aliasStringLength, aliasStringLengthAfterEncoding, inputStream.Position);
Text.PrintWarning(rtIDStrMismatch);
}

idString = string.Format(RtIDFormat.aliasReferenceStr, aliasString, aliasRefString);
break;

default:
throw new Exception(string.Format("Unknown RtID Type: \"{0}\" at Address: {1:x8}", (byte)idFlags, inputStream.Position) );
}

}

else
idString = RtIDFormat.nullReferenceStr;

WriteJsonString(outputStream, idString, isPropertyName);
}

/** <summary> Reads a RtID String from a JSON File and Writes its Representation to a RTON File. </summary>

<param name = "outputStream"> The Stream where the RTON Data will be Written. </param>
<param name = "sourceRegex"> The Regex of the RtID String. </param>
<param name = "targetStr"> The RtID String to be Written. </param> */

public static void Write(BinaryStream outputStream, Match sourceRegex)
{
outputStream.WriteByte( (byte)RtTypeIdentifier.IDString);
string aliasString = sourceRegex.Groups[1].ToString();

string uidString = sourceRegex.Groups[2].ToString();

if(aliasString == string.Empty && uidString == string.Empty)
{
outputStream.WriteByte( (byte)RtIDType.NullReference);
return;
}

Match uidMatch = Regex.Match(aliasString, "^([0-9|a-f]+)\\.([0-9|a-f]+)\\.([0-9|a-f]{8})$");
int uidFlags = uidMatch.Success ? 1 : 0;

if(uidFlags != 0)
{
outputStream.WriteByte( (byte)RtIDType.UidReference);
outputStream.WriteVarInt(uidString.Length);

outputStream.WriteStringByVarIntLength(uidString);
int firstUidDigits = Convert.ToInt32(uidMatch.Groups[1].ToString(), 16);

outputStream.WriteVarInt(firstUidDigits);
int secondUidDigits = Convert.ToInt32(uidMatch.Groups[2].ToString(), 16);

outputStream.WriteVarInt(secondUidDigits);
uint thirdUidDigits = Convert.ToUInt32(uidMatch.Groups[3].ToString(), 16);

outputStream.WriteUInt(thirdUidDigits);
}

else
{
outputStream.WriteByte( (byte)RtIDType.AliasReference);
outputStream.WriteVarInt(uidString.Length);

outputStream.WriteStringByVarIntLength(uidString);
outputStream.WriteVarInt(aliasString.Length);

outputStream.WriteStringByVarIntLength(aliasString);
}

}

}

/// <summary> Defines the expected Reference Type in a RtIDString. </summary>

public enum RtIDType : byte
{
/// <summary> The Reference pointed is <b>null</b>. </summary>
NullReference = 0x00,

/// <summary> The Reference pointed is a <b>UID</b>. </summary>
UidReference = 0x02,

/// <summary> The Reference pointed is an <b>Alias</b>. </summary>
AliasReference = 0x03
}

/// <summary> The formats used for Parsing RtIDStrings. </summary>

public readonly struct RtIDFormat
{
/// <summary> Used when the Reference pointed is <b>null</b>. </summary>
public static readonly string nullReferenceStr = "RTID()";

/// <summary> Used when the Reference pointed is a <b>UID</b>. </summary>
public static readonly string uidReferenceStr = "RTID({0:d}.{1:d}.{2:X8}@{3})";

/// <summary> Used when the Reference pointed is an <b>Alias</b>. </summary>
public static readonly string aliasReferenceStr = "RTID({0}@{1})";
}

}