using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>SexyUtils</c> Tasks. </summary>

public class SexyUtilsArgs : ArgumentsSet
{
/// <summary> Creates a new Instance of the <c>SexyUtilsArgs</c> Class. </summary>

public SexyUtilsArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();
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

default:
InputPath = Interface.GetDialog<InputPathDialog>().Popup() as string;
break;
}

}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected override void CheckArgumentsSet()
{
SexyParsersArgs defaultArgs = new();

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

/// <summary> Edits the Params Group given in the <c>SexyUtilsArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<SexyUtilsArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}