using SexyTool.Program.Core;
using SexyTool.Program.Core.Functions.ArchiveSecurity;
using SexyTool.Program.Graphics.Dialogs;
using System.IO;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>FileSecurity</c> Tasks. </summary>

public class FileSecurityArgs : ArgumentsSet
{
/** <summary> Gets or Sets the Info related to the Base64 Parser. </summary>
<returns> The Base64 Parse Info. </returns> */

public Base64Info Base64ParseInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the XOR Algorithm. </summary>
<returns> The XOR Digest Info. </returns> */

public GenericCryptoInfo XorCryptoInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the AES Algorithm. </summary>
<returns> The AES Crypto Info. </returns> */

public SpecificCryptoInfo AesCryptoInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>FileSecurityArgs</c> Class. </summary>

public FileSecurityArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

Base64ParseInfo = new Base64Info();
AesCryptoInfo = new SpecificCryptoInfo(AES_Cryptor.cipherKeySize, AES_Cryptor.iterationsRange);

XorCryptoInfo = new GenericCryptoInfo();
}

/** <summary> The logic to be Applied when the User selects an Argument. </summary>
<param name = "argName"> The Name of the Argument selected. </param> */

protected override void ArgumentSelectionLogic(string argName)
{

switch(argName)
{
case "OutputPath":
OutputPath = Interface.GetDialog<OutputPathDialog>().Popup() as string;
break;

case "Base64ParseInfo":
Base64ParseInfo.EditGroupInfo();
break;

case "XorCryptoInfo":
XorCryptoInfo.EditGroupInfo();
break;

case "AesCryptoInfo":
AesCryptoInfo.EditGroupInfo();
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
FileSecurityArgs defaultArgs = new();

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

Base64ParseInfo ??= defaultArgs.Base64ParseInfo;
XorCryptoInfo ??= defaultArgs.XorCryptoInfo;

AesCryptoInfo ??= defaultArgs.AesCryptoInfo;
}

/// <summary> Edits the Params Group given in the <c>FileSecurityArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<FileSecurityArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}