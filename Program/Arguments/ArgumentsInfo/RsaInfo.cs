using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;
using SexyTool.Program.Core.Functions.SexyCryptors;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to specific Params used for Ciphering Data with RSA. </summary>

public class RsaInfo : GenericCryptoInfo
{
/** <summary> Sets a Value which Contains Info about the Range of Iterations. </summary>
<returns> The Range of Iterations. </returns> */

private static readonly Limit<int> IterationsRange = RSA_Cryptor.iterationsRange;

/** <summary> Gets or Sets the Salt Value used for Reinforcing the Cipher Key. </summary>
<returns> The Salt Value Obtained. </returns> */

public byte[] SaltValue{ get; set; }

/** <summary> Gets or Sets the Hash Type used for Protecting the Cipher Key. </summary>
<returns> The Hash Type Obtained. </returns> */

public string HashType{ get; set; }

/** <summary> Gets or Sets the number of Iterations perfomed when Generating strongest Keys. </summary>
<returns> The number of Iterations. </returns> */

public int IterationsCount{ get; set; }

/// <summary> Creates a new Instance of the RsaInfo Class. </summary>

public RsaInfo()
{
CipherKeySize = RSA_Cryptor.cipherKeySize;
CipherKey = Console.InputEncoding.GetBytes(string.Format(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_SPECIFIC, CipherKeySize.MinValue, CipherKeySize.MaxValue) );

Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);
SaltValue = Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_SALT_VALUE);

HashType = "SHA1";
IterationsCount = IterationsRange.MinValue;
}

/** <summary> The logic to be Applied when the User selects an Argument. </summary>
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

default:
CipherKey = Interface.GetDialog<CipherKeyDialog>().Popup(CipherKeySize) as byte[];
break;
}

}

/// <summary> Checks each nullable Field of the 'RsaInfo' Instance and Validates them. </summary>

protected override void CheckGroupInfo()
{
int iterationsCheck = IterationsCount;
Crypto_Parameters.CheckIterationsCount(ref iterationsCheck, IterationsRange);

IterationsCount = iterationsCheck;
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

RsaInfo defaultInfo = new();
SaltValue ??= defaultInfo.SaltValue;

HashType ??= defaultInfo.HashType;
}

/// <summary> Edits the Info given in the 'RsaInfo' instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<RsaInfo>(selectionLogic.Init);
}

}

}