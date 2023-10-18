using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;

namespace SexyTool.Program.Arguments.ParamGroups
{
/// <summary> The Arguments used in the <c>SexyParsers</c> Tasks. </summary>

public class SexyParsersArgs : ArgumentsSet
{
/** <summary> Gets or Sets the Endian Encoding used when Parsing Files. </summary>
<returns> The Endian Encoding. </returns> */

public Endian EndianEncoding{ get; set; }

/** <summary> Gets or Sets the Info related to the RTON Parser. </summary>
<returns> The RTON Parse Info. </returns> */

public RtonInfo RtonParseInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>SexyParsersArgs</c> Class. </summary>

public SexyParsersArgs()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();

EndianEncoding = Endian.None;
RtonParseInfo = new();
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

case "EndianEncoding":
EndianEncoding = (Endian)Interface.GetUserSelection<EndianSelection>().GetSelectionParam();
break;

case "RtonParseInfo":
RtonParseInfo.EditGroupInfo();
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

RtonParseInfo ??= defaultArgs.RtonParseInfo;
}

/// <summary> Edits the Params Group given in the <c>SexyParsersArgs</c> instance. </summary>

public override void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<SexyCryptorsArgs>(selectionLogic.Init);

CheckArgumentsSet();
}

}

}