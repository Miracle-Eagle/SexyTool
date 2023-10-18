using ServiceStack;
using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;
using System.Security.Cryptography;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to specific Params used for Ciphering Data. </summary>

public class SpecificCryptoInfo : GenericCryptoInfo
{
/** <summary> Sets a Value which Contains Info about the Range of Iterations. </summary>
<returns> The Range of Iterations. </returns> */

protected Limit<int> IterationsRange{ get; set; } = new Limit<int>(1);

/** <summary> Gets or Sets the Salt Value used for Reinforcing the Cipher Key. </summary>
<returns> The Salt Value Obtained. </returns> */

public byte[] SaltValue{ get; set; }

/** <summary> Gets or Sets the Hash Type used for Protecting the Cipher Key. </summary>
<returns> The Hash Type Obtained. </returns> */

public string HashType{ get; set; }

/** <summary> Gets or Sets the number of Iterations perfomed when Generating strongest Keys. </summary>
<returns> The number of Iterations. </returns> */

public int IterationsCount{ get; set; }

/** <summary> Gets or Sets the Ciphering Mode used for Encrypting or Decrypting Data. </summary>
<returns> The Ciphering Mode Obtained. </returns> */

public CipherMode CipheringMode{ get; set; }

/** <summary> Gets or Sets the Padding Mode used for Encrypting or Decrypting Data. </summary>
<returns> The Padding Mode Obtained. </returns> */

public PaddingMode DataPadding{ get; set; }

/// <summary> Creates a new Instance of the SpecificCryptoInfo Class. </summary>

public SpecificCryptoInfo()
{
CipherKey = Console.InputEncoding.GetBytes(string.Format(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_SPECIFIC, CipherKeySize.MinValue, CipherKeySize.MaxValue) );
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

SaltValue = Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_SALT_VALUE);
HashType = "MD5";

IterationsCount = IterationsRange.MinValue;
CipheringMode = CipherMode.CBC;

DataPadding = PaddingMode.Zeros;
}

/** <summary> Creates a new Instance of the SpecificCryptoInfo Class with the Specified Key Length and Iterations Range. </summary>
<param name = "expectedKeySize"> The Key Size Excepted. </param>

<param name = "expectedIterationsRange"> The Iterations Range Excepted. </param> */

public SpecificCryptoInfo(Limit<int> expectedKeySize, Limit<int> expectedIterationsRange)
{
CipherKeySize = expectedKeySize;
IterationsRange = expectedIterationsRange;

CipherKey = Console.InputEncoding.GetBytes(string.Format(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_SPECIFIC, CipherKeySize.MinValue, CipherKeySize.MaxValue) );
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

SaltValue = Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_SALT_VALUE);
HashType = "MD5";

IterationsCount = IterationsRange.MinValue;
CipheringMode = CipherMode.CBC;

DataPadding = PaddingMode.Zeros;
}

/** <summary> The logic to be Applied when the User selects an Argument from 'SpecificCryptoInfo'. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected override void ParamSelectionLogic(string paramName)
{

switch(paramName)
{
case "SaltValue":
SaltValue = Interface.GetDialog<SaltValueDialog>().Popup() as byte[];
break;

case "HashType":
HashType = Interface.GetUserSelection<HashTypeSelection>().GetSelectionParam() as string;
break;

case "IterationsCount":
IterationsCount = (int)Interface.GetUserSelection<IterationsCountSelection>().GetSelectionParam(IterationsRange);
break;

case "CipheringMode":
CipheringMode = (CipherMode)Interface.GetUserSelection<CipherModeSelection>().GetSelectionParam();
break;

case "DataPadding":
DataPadding = (PaddingMode)Interface.GetUserSelection<PaddingModeSelection>().GetSelectionParam();
break;

default:
CipherKey = Interface.GetDialog<CipherKeyDialog>().Popup(CipherKeySize) as byte[];
break;
}

}

/// <summary> Checks each nullable Field of the 'SpecificCryptoInfo' Instance and Validates them. </summary>

protected override void CheckGroupInfo()
{
int iterationsCheck = IterationsCount;
Crypto_Parameters.CheckIterationsCount(ref iterationsCheck, IterationsRange);

IterationsCount = iterationsCheck;
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

SpecificCryptoInfo defaultInfo = new SpecificCryptoInfo();
SaltValue ??= defaultInfo.SaltValue;

HashType ??= defaultInfo.HashType;
}

/// <summary> Edits the Info given in the 'SpecificCryptoInfo' instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<SpecificCryptoInfo>(selectionLogic.Init);
}

}

}