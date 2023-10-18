namespace SexyTool.Program.Core.Functions.SexyCryptors.PopCapCipheredData
{
/// <summary> Initializes Analisis Functions about the Info Contained on Ciphered Files. </summary>

public struct CipheredDataInfo
{
/** <summary> Sets a Value which Contains Info about the Expected Header of a Ciphered File. </summary>
<returns> The expected Crypto Header. </returns> */

private static readonly string cryptoHeader = "CRYPT_RES\x0A\x00";

/** <summary> Gets or Sets a Value which Contains Info about the Size of a File before Encryption. </summary>
<returns> The Size of the File before Encryption. </returns> */

public long SizeBeforeEncryption{ get; set; }

/// <summary> Creates a new Instance of the CipheredDataInfo. </summary>

public CipheredDataInfo()
{
SizeBeforeEncryption = default;
}

/** <summary> Creates a new Instance of the CipheredDataInfo with the specific Size. </summary>
<param name = "SizeBeforeEncryption"> The Size this Instance should hold as a 'SizeBeforeEncryption' Field. </param> */

public CipheredDataInfo(long SizeBeforeEncryption)
{
this.SizeBeforeEncryption = SizeBeforeEncryption;
}

/** <summary> Reads the Info of a Ciphered File. </summary>

<param name = "targetStream"> The Stream to be Read. </param>

<returns> The Info Read. </returns> */

public static CipheredDataInfo Read(BinaryStream targetStream)
{
CipheredDataInfo cipheredData = new();
targetStream.CompareString(cryptoHeader);

cipheredData.SizeBeforeEncryption = targetStream.ReadLong();
return cipheredData;
}

/** <summary> Writes the Info of a Ciphered File. </summary>
<param name = "targetStream"> The Stream where the Info will be Written. </param> */

public readonly void Write(BinaryStream targetStream)
{
targetStream.WriteString(cryptoHeader);
targetStream.WriteLong(SizeBeforeEncryption);
}

}

}