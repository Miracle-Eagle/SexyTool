using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System.IO.Compression;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>FileCompressors</c> Tasks. </summary>

public class FileCompressorsArgs : ArgumentsSet
{
/** <summary> Gets or Sets the Compression Level to be Used when Compressing or Decompressing Data. </summary>
<returns> The Compression Level to be Used. </returns> */

public CompressionLevel CompressionLvl{ get; set; }

/// <summary> Creates a new Instance of the <c>FileCompressorsArgs</c> Class. </summary>

public FileCompressorsArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

CompressionLvl = CompressionLevel.Optimal;
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

case "CompressionLvl":
CompressionLvl = (CompressionLevel)Interface.GetUserSelection<CompressionLevelSelection>().GetSelectionParam();
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
FileCompressorsArgs defaultArgs = new();

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
}

/// <summary> Edits the Params Group given in the <c>FileCompressorsArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<FileCompressorsArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}