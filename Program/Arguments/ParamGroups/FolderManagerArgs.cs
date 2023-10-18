using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using System.IO;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>FolderManager</c> Tasks. </summary>

public class FolderManagerArgs : ArgumentsSet
{
/** <summary> Gets or Sets the new Name of a Folder. </summary>
<returns> The new Name Folder of the Folder. </returns> */

public string NewName{ get; set; }

/// <summary> Creates a new Instance of the <c>FolderManagerArgs</c> Class. </summary>

public FolderManagerArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

NewName = Text.LocalizedData.DEFAULT_PARAMETER_NEW_FOLDER_NAME;
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

case "NewName":
NewName = Interface.GetDialog<NewNameDialog>().Popup() as string;
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
FolderManagerArgs defaultArgs = new FolderManagerArgs();

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

#region ====== Action - Set New Name ======

ActionWrapper<string> newNameAction = new ( (sourceName) =>
{
NewName = defaultArgs.NewName;
} );

#endregion

Input_Manager.CheckEmptyString(NewName, newNameAction.Init);
}

/** <summary> Gets the default Input Path basing on the CurrentAppDirectory. </summary>
<returns> The default Input Path. </returns> */

private static new string GetDefaultInputPath()
{
string inputFolderName = Text.LocalizedData.FILESYSTEM_INDICATOR_INPUT;
string defaultInputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + inputFolderName;

return defaultInputPath;
}

/** <summary> Gets the default Output Path basing on the CurrentAppDirectory. </summary>
<returns> The default Output Path. </returns> */

private static new string GetDefaultOutputPath()
{
string outputFolderName = Text.LocalizedData.FILESYSTEM_INDICATOR_OUTPUT;
string defaultOutputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + outputFolderName;

return defaultOutputPath;
}

///<summary> Edits the Params Group given in the <c>FolderManagerArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<FolderManagerArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}