using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.Menus;
using System;
using System.Reflection;

namespace SexyTool.Program.Arguments
{
/// <summary> Stores Info related to a specifc Group of Parameters </summary>

public class ParamGroupInfo
{
/** <summary> The logic to be Applied when the User selects an Argument. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected virtual void ParamSelectionLogic(string paramName)
{
return;
}

/// <summary> Checks each nullable Field of this Instance and Validates them. </summary>

protected virtual void CheckGroupInfo()
{
return;
}

/** <summary> Comits changes to the given 'ParamGroupInfo' instance. </summary>
<param name = "selectionLogic"> The Logic to be Applied when the User selects a Param. </param> */

protected void ComitChanges<T>(Action<string> selectionLogic) where T : ParamGroupInfo
{
ParamsMenu paramSelector = Interface.GetMenu<ParamsMenu>();
object paramInstance = Activator.CreateInstance<T>();

paramSelector.UpdateParamInstance(paramInstance);
bool exitMenu = false;

while(!exitMenu)
{
PropertyInfo selectedParam = paramSelector.DynamicSelection() as PropertyInfo;
selectionLogic(selectedParam.Name);

exitMenu = (bool)Interface.GetDialog<ReturnDialog>().Popup();
}

}

/// <summary> Edits the Info given in the 'ParamGroupInfo' instance. </summary>

public virtual void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<ParamGroupInfo>(selectionLogic.Init);
}

}

}