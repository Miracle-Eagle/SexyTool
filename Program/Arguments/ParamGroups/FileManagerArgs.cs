using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System.IO;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>FileManager</c> Tasks. </summary>

public class FileManagerArgs : ArgumentsSet
{
/** <summary> Gets or Sets the new Name of a File. </summary>
<returns> The new Name of the File. </returns> */

public string NewName{ get; set; }

/** <summary> Gets or Sets a boolean that Determines if existing Files should be Replaced or not. </summary>
<returns> The Files Replacement boolean. </returns> */

public bool ReplaceExistingFiles{ get; set; }

/// <summary> Creates a new Instance of the <c>FileManagerArgs</c> Class. </summary>

public FileManagerArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

NewName = Text.LocalizedData.DEFAULT_PARAMETER_NEW_FILE_NAME;
ReplaceExistingFiles = false;
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

case "ReplaceExistingFiles":
ReplaceExistingFiles = (bool)Interface.GetUserSelection<FilesReplacementSelection>().GetSelectionParam();
break;

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
FileManagerArgs defaultArgs = new();

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

/** <summary> Gets the default Output Path basing on the CurrentAppDirectory. </summary>
<returns> The default Output Path. </returns> */

private static new string GetDefaultOutputPath()
{
string outputFileName = Text.LocalizedData.FILESYSTEM_INDICATOR_OUTPUT;
string defaultOutputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + outputFileName + ".txt";

return defaultOutputPath;
}

/// <summary> Edits the Params Group given in the <c>FileManagerArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<FileManagerArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}