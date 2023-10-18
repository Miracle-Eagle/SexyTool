using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.Menus;
using System;
using System.IO;
using System.Reflection;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups a Set of generic Arguments that are mostly Used on Function Calls. </summary>

public class ArgumentsSet
{
/** <summary> Gets or Sets the Input Path entered by the User. </summary>
<returns> The Input Path entered by the User. </returns> */

public string InputPath{ get; set; }

/** <summary> Gets or Sets the Output Path entered by the User. </summary>
<returns> The Output Path entered by the User. </returns> */

public string OutputPath{ get; set; }

/** <summary> The logic to be Applied when the User selects an Argument. </summary>
<param name = "argName"> The Name of the Argument selected. </param> 

<param name = "sourceArgs"> The ArgumentsSet where the Changes will be Applied after Selection. </param> */

protected virtual void ArgumentSelectionLogic(string argName)
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

protected virtual void CheckArgumentsSet()
{
ArgumentsSet defaultArgs = new();

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

/** <summary> Comits changes to the given 'ArgumentsSet' instance. </summary>
<param name = "selectionLogic"> The Logic to be Applied when the User selects a Param. </param> */

protected void ComitChanges<T>(Action<string> selectionLogic) where T : ArgumentsSet
{
ParamsMenu argsSelector = Interface.GetMenu<ParamsMenu>();
object paramInstance = Activator.CreateInstance<T>();

argsSelector.UpdateParamInstance(paramInstance);
bool exitMenu = false;

while(!exitMenu)
{
PropertyInfo selectedParam = argsSelector.DynamicSelection() as PropertyInfo;
selectionLogic(selectedParam.Name);

exitMenu = (bool)Interface.GetDialog<ReturnDialog>().Popup();
}

}

/// <summary> Edits the Params Group given in the 'ArgumentSet' instance. </summary>

public virtual void EditParamsGroup()
{
ActionWrapper<string> selectionLogic = new( ArgumentSelectionLogic );
ComitChanges<ArgumentsSet>(selectionLogic.Init);
}

/** <summary> Gets the default Input Path basing on the CurrentAppDirectory. </summary>
<returns> The default Input Path. </returns> */

protected static string GetDefaultInputPath()
{
string inputFileName = Text.LocalizedData.FILESYSTEM_INDICATOR_INPUT;
string defaultInputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + inputFileName + ".txt";

return defaultInputPath;
}

/** <summary> Gets the default Output Path basing on the CurrentAppDirectory. </summary>
<returns> The default Output Path. </returns> */

protected static string GetDefaultOutputPath()
{
string outputFileName = Text.LocalizedData.FILESYSTEM_INDICATOR_OUTPUT;
string defaultOutputPath = Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + outputFileName + ".bin";

return defaultOutputPath;
}

}

}