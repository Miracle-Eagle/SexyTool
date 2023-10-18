namespace SexyTool.Program.Core.Functions.SexyCompressors.StandarMediaFile
{
/// <summary> The Header of a Standar Media File (SMF). </summary>

public static class SmfHeader
{
/** <summary> Sets a Value which Contains Info about the magic Bytes of a Standar Media File (SMF). </summary>
<returns> The SMF magic Bytes. </returns> */

private static readonly byte[] magicBytes = { 0xD4, 0xFE, 0xAD, 0xDE };

/** <summary> Reads the Magic Bytes of a Standar Media File (SMF). </summary>
<param name = "targetStream"> The Stream to be Read. </param> */

public static void ReadMagicBytes(BinaryStream targetStream) => targetStream.CompareBytes(magicBytes);

/** <summary> Writes the Magic Bytes to a Standar Media File (SMF). </summary>
<param name = "targetStream"> The Stream where the Info will be Written. </param> */

public static void WriteMagicBytes(BinaryStream targetStream) => targetStream.Write(magicBytes);
}

}