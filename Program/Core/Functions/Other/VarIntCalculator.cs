using System;
using System.IO;

namespace SexyTool.Program.Core.Functions.Other
{
/// <summary> Initializes calculating Functions for VarInt and Integer Values. </summary>

public class VarIntCalculator
{
/** <summary> Displays the Encoding of an Integer as a VarInt Value. </summary>

<param name = "targetValue"> The Integer to Encode. </param>
<param name = "treatValueAsSigned"> Determines if the Integer should be treated as Signed or not. </param> */

internal static void DisplayIntEncoding(int targetValue, bool treatValueAsSigned)
{
int varIntValue = CalculateVarInt(targetValue, treatValueAsSigned);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_VARINT_VALUE, varIntValue) );
}

/** <summary> Displays the Decoding of a VarInt as a Integer Value. </summary>

<param name = "targetValue"> The VarInt to Decode. </param>
<param name = "treatValueAsSigned"> Determines if the Integer calculated should be treated as Signed or not. </param> */

internal static void DisplayIntDecoding(int targetValue, bool treatValueAsSigned)
{
int integerValue = CalculateInteger(targetValue, treatValueAsSigned);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_INTEGER_VALUE, integerValue) );
}


/** <summary> Calculates an Integer from a given VarInt Value. </summary>

<param name = "targetValue"> The VarInt where the Integer will be Calculated from. </param>
<param name = "treatValueAsSigned"> Determines if the Integer calculated should be treated as Signed or not. </param>

<exception cref = "ArgumentOutOfRangeException"></exception>
<exception cref = "EndOfStreamException"></exception>
<exception cref = "InvalidOperationException"></exception>
<exception cref = "OverflowException"></exception>

<returns> The Integer Calculated. </returns> */

public static int CalculateInteger(int targetValue, bool treatValueAsSigned)
{
using MemoryStream memoryBuffers = new();
byte[] varIntBytes = BitConverter.GetBytes(targetValue);

memoryBuffers.Write(varIntBytes, 0, varIntBytes.Length);
memoryBuffers.Position = 0;

using BinaryReader bufferReader = new(memoryBuffers);
int integerValue = bufferReader.Read7BitEncodedInt();

if(treatValueAsSigned)
{
int logicValue = integerValue & 1;
int squareSuplement = integerValue/2;

integerValue = logicValue != 0 ? (squareSuplement + 1) * -1 : squareSuplement;
}

return integerValue;
}

/** <summary> Calculates a VarInt from a given Integer Value. </summary>

<param name = "targetValue"> The Integer where the VarInt will be Calculated from. </param>
<param name = "treatValueAsSigned"> Determines if the Integer entered by the User should be treated as Signed or not. </param>

<exception cref = "OverflowException"></exception>
<returns> The Varint Value Calculated. </returns> */

public static int CalculateVarInt(int targetValue, bool treatValueAsSigned)
{
using MemoryStream memoryBuffers = new();
using BinaryWriter bufferWriter = new(memoryBuffers);

if(treatValueAsSigned)
{
int logicValue = targetValue & 1;
int squareComplement = targetValue * 2;

targetValue = logicValue != 0 ? (squareComplement - 1) : squareComplement;
}

bufferWriter.Write7BitEncodedInt(targetValue);
byte[] integerBytes = memoryBuffers.ToArray();

int expectedLength = 4;

if(integerBytes.Length < expectedLength)
Input_Manager.FillArray(ref integerBytes, expectedLength);

return BitConverter.ToInt32(integerBytes, 0);
}

}

}