using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to common Params used for Ciphering Data. </summary>

public class GenericCryptoInfo : ParamGroupInfo
{
/** <summary> Sets a Value which Contains Info about the Range of a Cipher Key Size. </summary>
<returns> The Cipher Key Size. </returns> */

protected Limit<int> CipherKeySize{ get; set; } = new(1, Array.MaxLength);

/** <summary> Gets or Sets the Cipher Key used for Encrypting or Decrypting Data. </summary>
<returns> The Cipher Key Obtained. </returns> */

public byte[] CipherKey{ get; set; }

/// <summary> Creates a new Instance of the GenericCryptoInfo Class. </summary>

public GenericCryptoInfo()
{
CipherKey = Console.InputEncoding.GetBytes(Text.LocalizedData.DEFAULT_PARAMETER_CIPHER_KEY_GENERIC);
Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);
}

/** <summary> The logic to be Applied when the User selects an Argument from 'GenericCryptoInfo'. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected override void ParamSelectionLogic(string paramName)
{

CipherKey = paramName switch
{
_ => Interface.GetDialog<CipherKeyDialog>().Popup() as byte[]
};

}

///<summary> Checks each nullable Field of the 'GenericCryptoInfo' Instance and Validates them. </summary>

protected override void CheckGroupInfo() => Crypto_Parameters.CheckKeySize(CipherKey, CipherKeySize);

/// <summary> Edits the Info given in the 'GenericCryptoInfo' instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<GenericCryptoInfo>(selectionLogic.Init);
}

}

}