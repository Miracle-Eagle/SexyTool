namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions
{
/// <summary> Defines the expected Type of a Value in the RtSystem. </summary>

public enum RtTypeIdentifier : byte
{
/// <summary> The Type is a Boolean that holds <b>false</b> as a Value. </summary>
Bool_false = 0x00,

/// <summary> The Type is a Boolean that holds <b>true</b> as a Value. </summary>
Bool_true = 0x01,

/** <summary> Represents a Object that holds a <b>null</b> Value. <para>
</para>Literal Stars (<b>*</b>) are used to represent null Values in the RtSystem. </summary>

<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

Null = 0x02,

/// <summary> The Type is a 8-bits Integer that holds a Value different from Zero. </summary>
Byte = 0x08,

/// <summary> The Type is a 8-bits Integer that holds <b>Zero</b> as a Value. </summary>
Byte_0 = 0x09,

/// <summary> The Type is a 8-bits signed Integer that holds a Value different from Zero. </summary>
SByte = 0x0A,

/// <summary> The Type is a 8-bits signed Integer that holds <b>Zero</b> as a Value. </summary>
SByte_0 = 0x0B,

/// <summary> The Type is a 16-bits Integer that holds a Value different from Zero. </summary>
Short = 0x10,

/// <summary> The Type is a 16-bits Integer that holds <b>Zero</b> as a Value. </summary>
Short_0 = 0x11,

/// <summary> The Type is an unsigned 16-bits Integer that holds a Value different from Zero. </summary>
UShort = 0x12,

/// <summary> The Type is an unsigned 16-bits Integer that holds <b>Zero</b> as a Value. </summary>
UShort_0 = 0x13,

/// <summary> The Type is a 32-bits Integer that holds a Value different from Zero. </summary>
Int = 0x20,

/// <summary> The Type is a 32-bits Integer that holds <b>Zero</b> as a Value. </summary>
Int_0 = 0x21,

/// <summary> The Type is a 32-bits Floating-point that holds a Value different from Zero. </summary>
Float = 0x22,

/// <summary> The Type is a 32-bits Floating-point that holds <b>Zero</b> as a Value. </summary>
Float_0 = 0x23,

/// <summary> The Type is a 32-bits variant Integer. </summary>
VarInt = 0x24,

/// <summary> The Type is a 32-bits variant Integer that was Encoded with ZigZag. </summary>
ZigZagInt = 0x25,

/// <summary> The Type is an unsigned 32-bits Integer that holds a Value different from Zero. </summary>
UInt = 0x26,

/// <summary> The Type is an unsigned 32-bits Integer that holds <b>Zero</b> as a Value. </summary>
UInt_0 = 0x27,

/// <summary> The Type is an unsigned 32-bits variant Integer. </summary>
UVarInt = 0x28,

/// <summary> The Type is a 64-bits Integer that holds a Value different from Zero. </summary>
Long = 0x40,

/// <summary> The Type is a 64-bits Integer that holds <b>Zero</b> as a Value. </summary>
Long_0 = 0x41,

/// <summary> The Type is a 64-bits Floating-point that holds a Value different from Zero. </summary>
Double = 0x42,

/// <summary> The Type is a 64-bits Floating-point that holds <b>Zero</b> as a Value. </summary>
Double_0 = 0x43,

/// <summary> The Type is a 64-bits variant Integer. </summary>
VarLong = 0x44,

/// <summary> The Type is a 64-bits variant Integer that was Encoded with ZigZag. </summary>
ZigZagLong = 0x45,

/// <summary> The Type is an unsigned 64-bits Integer that holds a Value different from Zero. </summary>
ULong = 0x46,

/// <summary> The Type is an unsigned 64-bits Integer that holds <b>Zero</b> as a Value. </summary>
ULong_0 = 0x47,

/// <summary> The Type is an unsigned 64-bits variant Integer. </summary>
UVarLong = 0x48,

/// <summary> The Type is a Native String. </summary>
NativeString = 0x81,

/// <summary> The Type is a Unicode String. </summary>
UnicodeString = 0x82,

/// <summary> The Type is a ID string that Serves as a Reference to an Object. </summary>

IDString = 0x83,

/// <summary> The Type is a RtID that holds a <b>null</b> Reference. </summary>
IDString_Null = 0x84,

/// <summary> Represents the Beginning of an Object. </summary>
Object_Start = 0x85,

/// <summary> The Type is an Array. </summary>
Array = 0x86,

/** <summary> The Type is a Binary String. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

BinaryString = 0x87,

/// <summary> Represents the Value of a Native String. </summary>
NativeString_Value = 0x90,

/// <summary> Represents the Index of a Native String. </summary>
NativeString_Index = 0x91,

/// <summary> Represents the Value of a Unicode String. </summary>
UnicodeString_Value = 0x92,

/// <summary> Represents the Index of a Unicode String. </summary>
UnicodeString_Index = 0x93,

/** <summary> The Type is an Object that stores one <b>NativeString</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithNativeString = 0xB0,

/** <summary> The Type is an Object that stores two <b>NativeStrings</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithNativeString_x2 = 0xB1,

/** <summary> The Type is an Object that stores one <b>UnicodeString</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithUnicodeString = 0xB2,

/** <summary> The Type is an Object that stores two <b>UnicodeStrings</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithUnicodeString_x2 = 0xB3,

/** <summary> The Type is an Object that stores one <b>String</b> on it. <para>
</para> The Object can either store a NativeString or a UnicodeString. </summary>
 
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithString = 0xB4,

/** <summary> The Type is an Object that stores two <b>Strings</b> on it. </summary> <para>
</para> The Object can either store NativeStrings or UnicodeStrings. </summary>

<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithString_x2 = 0xB5,

/** <summary> The Type is an Object that stores three <b>Strings</b> on it. </summary> <para>
</para> The Object can either store NativeStrings or UnicodeStrings. </summary>

<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithString_x3 = 0xB6,

/** <summary> The Type is an Object that stores four <b>Strings</b> on it. </summary> <para>
</para> The Object can either store NativeStrings or UnicodeStrings. </summary>

<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithString_x4 = 0xB7,

/** <summary> Represents an Objects Collection. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectsCollection = 0xB8,

/** <summary> Represents an Arrays Collection. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ArraysCollection = 0xB9,

/** <summary> The Type is an Object that stores three <b>NativeStrings</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithNativeString_x3 = 0xBA,

/** <summary> The Type is an Object that stores a <b>BinaryString</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithBinaryString = 0xBB,

/** <summary> The Type is an Object that stores a <b>Boolean</b> on it. </summary>
<remarks> Not used on common RTON Files (maybe its Notation is different from the One used at Version 1.0). </remarks> */

ObjectWithBoolean = 0xBC,

/// <summary> Represents the Beginning of an Array. </summary>
Array_Start = 0xFD,

/// <summary> Represents the End of an Array. </summary>
Array_End = 0xFE,

/// <summary> Represents the End of an Object. </summary>
Object_End = 0xFF
}

}