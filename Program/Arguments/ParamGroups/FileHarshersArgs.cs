using SexyTool.Program.Core;
using SexyTool.Program.Core.Functions.StringHashers;
using SexyTool.Program.Graphics.Dialogs;
using System.IO;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>FileHashers</c> Tasks. </summary>

public class FileHarshersArgs : ArgumentsSet
{
/** <summary> Gets or Sets the Crypto Info related to the MD5 Digest. </summary>
<returns> The MD5 Digest Info. </returns> */

public GenericDigestInfo Md5DigestInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the SHA-1 Digest. </summary>
<returns> The SHA-1 Digest Info. </returns> */

public GenericDigestInfo Sha1DigestInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the SHA-256 Digest. </summary>
<returns> The SHA-256 Digest Info. </returns> */

public GenericDigestInfo Sha256DigestInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the SHA-384 Digest. </summary>
<returns> The SHA-384 Digest Info. </returns> */

public GenericDigestInfo Sha384DigestInfo{ get; set; }

/** <summary> Gets or Sets the Crypto Info related to the SHA-512 Digest. </summary>
<returns> The SHA-512 Digest Info. </returns> */

public GenericDigestInfo Sha512DigestInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>FileHarshersArgs/c> Class. </summary>

public FileHarshersArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

Md5DigestInfo = new GenericDigestInfo(Md5StringDigest.cipherKeySize);
Sha1DigestInfo = new GenericDigestInfo(Sha1StringDigest.cipherKeySize);

Sha256DigestInfo = new GenericDigestInfo(Sha256StringDigest.cipherKeySize);
Sha384DigestInfo = new GenericDigestInfo(Sha384StringDigest.cipherKeySize);

Sha512DigestInfo = new GenericDigestInfo(Sha512StringDigest.cipherKeySize);
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

case "Md5DigestInfo":
Md5DigestInfo.EditGroupInfo();
break;

case "Sha1DigestInfo":
Sha1DigestInfo.EditGroupInfo();
break;

case "Sha256DigestInfo":
Sha256DigestInfo.EditGroupInfo();
break;

case "Sha384DigestInfo":
Sha384DigestInfo.EditGroupInfo();
break;

case "Sha512DigestInfo":
Sha512DigestInfo.EditGroupInfo();
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
FileHarshersArgs defaultArgs = new();

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

Md5DigestInfo ??= defaultArgs.Md5DigestInfo;
Sha1DigestInfo ??= defaultArgs.Sha1DigestInfo;

Sha256DigestInfo ??= defaultArgs.Sha256DigestInfo;
Sha384DigestInfo ??= defaultArgs.Sha384DigestInfo;

Sha512DigestInfo ??= defaultArgs.Sha512DigestInfo;
}

/** <summary> Gets the default Output Path basing on the CurrentAppDirectory. </summary>
<returns> The default Output Path. </returns> */

private static new string GetDefaultOutputPath()
{
string outputFileName = Text.LocalizedData.FILESYSTEM_INDICATOR_OUTPUT;
string defaultOutputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + outputFileName + ".hash";

return defaultOutputPath;
}

/// <summary> Edits the Params Group given in the <c>FileHarshersArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<FileHarshersArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}