using System;
using SexyTool.Program.Core.Functions.Other;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs additional Functions to this Program. </summary>

internal class OtherFunctions : Framework
{
/// <summary> Creates a new Instance of the <c>OtherFunctions</c> Framework. </summary>

public OtherFunctions()
{
ID = 99;
DisplayName = Text.LocalizedData.FRAMEWORK_OTHER_FUNCTIONS;

FunctionsList = new()
{
// Function A - Calculate VarInt Value

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_CALCULATE_VARINT_VALUE,
Process = () => VarIntCalculator.DisplayIntEncoding(, (bool)Interface.GetUserSelection<SignModeSelection>().GetSelectionParam() )
},

// Function B - Calculate Integer Value

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_CALCULATE_INTEGER_VALUE,
Process = () => VarIntCalculator.DisplayIntDecoding(Interface.GetUserSelection<GenericSelection>().GetGenericParams<int>(Text.LocalizedData.ADVICE_ENTER_VARINT_VALUE, Text.LocalizedData.PARAM_VARINT_VALUE), (bool)Interface.GetUserSelection<SignModeSelection>().GetSelectionParam() )
},

// Function C - Calculate DateTime

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_CALCULATE_DATE_TIME,
Process = () => TimeCalculator.DateTimeConversion(Interface.GetUserSelection<GenericSelection>().GetGenericParams<double>(Text.LocalizedData.ADVICE_ENTER_TIME_STAMP, Text.LocalizedData.PARAM_TIME_STAMP) )
},

// Function D - Calculate TimeStamp

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_CALCULATE_TIME_STAMP,
Process = () => TimeCalculator.TimeStampConversion( (DateTime)Interface.GetUserSelection<DateTimeSelection>().GetSelectionParam() )
},

// Function E - Encrypt Integer Value

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_INTEGER_VALUE,
Process = () => IntegersGuard.DisplayIntEncryption()
},

// Function F - Decrypt Integer Value

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_INTEGER_VALUE,
Process = () => IntegersGuard.DisplayIntDecryption()
},

// Function G - Encode Base64 String

new Function()
{
ID = ConsoleKey.G,
DisplayName = Text.LocalizedData.FUNCTION_ENCODE_BASE64_STRING,
Process = () => Base64StringParser.DisplayStringEncoding(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_ENCODING) as string, (bool)Interface.GetUserSelection<Base64SecureModeSelection>().GetSelectionParam() )
},

// Function H - Decode Base64 String

new Function()
{
ID = ConsoleKey.H,
DisplayName = Text.LocalizedData.FUNCTION_DECODE_BASE64_STRING,
Process = () => Base64StringParser.DisplayStringDecoding(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DECODING) as string, (bool)Interface.GetUserSelection<Base64SecureModeSelection>().GetSelectionParam() )
},

// Function I - Encrypt XOR String

new Function()
{
ID = ConsoleKey.I,
DisplayName = Text.LocalizedData.FUNCTION_ENCRYPT_XOR_STRING,
Process = () => XorBytesCryptor.DisplayStringEncryption(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_ENCRYPTION) as string, Interface.GetDialog<CipherKeyDialog>().Popup() as byte[] )
},

// Function J - Decrypt XOR String

new Function()
{
ID = ConsoleKey.J,
DisplayName = Text.LocalizedData.FUNCTION_DECRYPT_XOR_STRING,
Process = () => XorBytesCryptor.DisplayStringDecryption(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DECRYPTION) as string, Interface.GetDialog<CipherKeyDialog>().Popup() as byte[] )
}

};

}

}

}