using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to common Params used for Hashing Data. </summary>

public class GenericDigestInfo : GenericCryptoInfo
{
/** <summary> Gets or Sets a Boolean that Determines if the Strings should be Hashed with HMAC (Hash-based Message Authentication Code) or not. </summary>
<returns> The Auth Mode. </returns> */

public bool UseAuthCode{ get; set; }

/// <summary> Creates a new Instance of the GenericDigestInfo Class. </summary>

public GenericDigestInfo()
{
CipherKey = Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_GENERIC);
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

UseAuthCode = false;
}

/** <summary> Creates a new Instance of the GenericDigestInfo Class with the Specified Key Length. </summary>
<param name = "expectedKeySize"> The Key Size Excepted. </param> */

public GenericDigestInfo(Limit<int> expectedKeySize)
{
CipherKeySize = expectedKeySize;
CipherKey = Console.InputEncoding.GetBytes(string.Format(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_SPECIFIC, CipherKeySize.MinValue, CipherKeySize.MaxValue) );

Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);
UseAuthCode = false;
}

/** <summary> The logic to be Applied when the User selects an Argument from 'GenericDigestInfo'. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected override void ParamSelectionLogic(string paramName)
{

switch(paramName)
{
case "UseAuthCode":
UseAuthCode = (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam();
break;

default:
CipherKey = Interface.GetDialog<CipherKeyDialog>().Popup() as byte[];
break;
}

}

/// <summary> Checks each nullable Field of the 'GenericDigestInfo' Instance and Validates them. </summary>

protected override void CheckGroupInfo() => Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

/// <summary> Edits the Info given in the 'GenericDigestInfo' instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<GenericDigestInfo>(selectionLogic.Init);
}

}

}