using SexyTool.Program.Core;
using SexyTool.Program.Core.Functions.SexyCryptors;
using SexyTool.Program.Graphics.Dialogs;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>SexyCryptors</c> Tasks. </summary>

public class SexyCryptorsArgs : ArgumentsSet
{
/** <summary> Gets or Sets the Crypto Info related to the DES Algorithm. </summary>
<returns> The DES Crypto Info. </returns> */

public SpecificCryptoInfo DesCryptoInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the 3-DES Algorithm. </summary>
<returns> The 3-DES Crypto Info. </returns> */

public SpecificCryptoInfo TripleDesCryptoInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the Rijndael Algorithm. </summary>
<returns> The Rijndael Crypto Info. </returns> */

public RijndaelInfo RijndaelCryptoInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the RSA Algorithm. </summary>
<returns> The RSA Crypto Info. </returns> */

public RsaInfo RsaCryptoInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>SexyCryptorsArgs</c> Class. </summary>

public SexyCryptorsArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

DesCryptoInfo = new(DES_Cryptor.cipherKeySize, DES_Cryptor.iterationsRange);
TripleDesCryptoInfo = new(TripleDES_Cryptor.cipherKeySize, TripleDES_Cryptor.iterationsRange);

RijndaelCryptoInfo = new();
RsaCryptoInfo = new();
}

/** <summary> The logic to be Applied when the User selects an Argument. </summary>

<param name = "argName"> The Name of the Argument selected. </param> 
<param name = "sourceArgs"> The ArgumentsSet where the Changes will be Applied after Selection. </param> */

protected override void ArgumentSelectionLogic(string argName)
{

switch(argName)
{
case "OutputPath":
OutputPath = Interface.GetDialog<OutputPathDialog>().Popup() as string;
break;

case "DesCryptoInfo":
DesCryptoInfo.EditGroupInfo();
break;

case "TripleDesCryptoInfo":
TripleDesCryptoInfo.EditGroupInfo();
break;

case "RijndaelCryptoInfo":
RijndaelCryptoInfo.EditGroupInfo();
break;

case "RsaCryptoInfo":
RsaCryptoInfo.EditGroupInfo();
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
SexyCryptorsArgs defaultArgs = new();

#region ====== Action - Set Input Path ======

ActionWrapper<string> inputPathAction = new( (sourcePath) =>
{
InputPath = defaultArgs.InputPath;
} );

#endregion

Input_Manager.CheckEmptyString(InputPath, inputPathAction.Init);
Path_Helper.CheckExistingPath(InputPath, true);

#region ====== Action - Set Output Path ======

ActionWrapper<string> outputPathAction = new( (sourcePath) =>
{
OutputPath = defaultArgs.OutputPath;
} );

#endregion

Input_Manager.CheckEmptyString(OutputPath, outputPathAction.Init);
Path_Helper.CheckExistingPath(OutputPath, false);

DesCryptoInfo ??= defaultArgs.DesCryptoInfo;
TripleDesCryptoInfo ??= defaultArgs.TripleDesCryptoInfo;

RijndaelCryptoInfo ??= defaultArgs.RijndaelCryptoInfo;
RsaCryptoInfo ??= defaultArgs.RsaCryptoInfo;
}

/// <summary> Edits the Params Group given in the <c>SexyCryptorsArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<SexyCryptorsArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}