using System;
using System.Reflection;
using SexyTool.Program.Core;

namespace SexyTool.Program.Graphics.Menus
{
/// <summary> The Params Menu of this Program. </summary>

internal partial class ParamsMenu : Menu
{
/** <summary> Gets or Sets the Instance where the Params should be Obtained from </summary>
<returns> The Param Instance. </returns> */

protected object paramInstance;

/// <summary> Creates a new Instance of the ParamsMenu. </summary>

public ParamsMenu()
{
headerText = Text.LocalizedData.HEADER_PARAMS_EDITOR;
adviceText = Text.LocalizedData.ADVICE_SELECT_PARAM;

paramInstance = this;
}

/** <summary> Displays the Value of a Property according to its Type. </summary>
<param name = "targetPropInfo"> Info related to the Poperty to be Displayed on Screen. </param> */

private void DisplayPropertyValue(PropertyInfo targetPropInfo)
{
object propertyValue = targetPropInfo.GetValue(paramInstance);
Type propertyType = propertyValue.GetType();

string currentValueMsg;

if(propertyType.IsPrimitive)
{
currentValueMsg = string.Format(Text.LocalizedData.DIALOG_CURRENT_VALUE, propertyValue);
Text.Print(true, "{0} ({1})", targetPropInfo.Name, currentValueMsg);
}

else if(propertyType.IsArray)
{
Type elementsType = propertyType.GetElementType();
string arrayElements;

if(elementsType == typeof(byte) )
arrayElements = Console.InputEncoding.GetString(propertyValue as byte[] );

else
arrayElements = string.Join(Input_Manager.strSeparator_Comma, propertyValue);

currentValueMsg = string.Format(Text.LocalizedData.DIALOG_CURRENT_VALUE, arrayElements);
Text.Print(true, "{0} ({1})", targetPropInfo.Name, currentValueMsg);
}

else
{

if(propertyType == typeof(string) || propertyType == typeof(Enum) )
currentValueMsg = string.Format(Text.LocalizedData.DIALOG_CURRENT_VALUE, propertyValue);

else
currentValueMsg = targetPropInfo.Name;

Text.Print(true, currentValueMsg);
}

}

/** <summary> Displays the ParamsMenu. </summary>
<returns> The Parameter selected by the User. </returns> */

public override object DynamicSelection()
{
Type instanceType = paramInstance.GetType();
PropertyInfo[] paramFields = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

PropertyInfo selectedParam = default;
ActionWrapper<PropertyInfo> displayAction = new( PrintAction );

ActionWrapper<PropertyInfo, int> onSelectAction = new( SelectiveAction );
ShowOptions(paramFields, ref selectedParam, displayAction.Init, onSelectAction.Init);

return selectedParam;
}

/** <summary> Prints info related to the Param given (Name, and Value, if specified). </summary>
<param name = "sourceItem"> The Parameter where the Info will be Obtained from. </param> */

public override void PrintAction<T>(T sourceItem)
{

if(sourceItem is PropertyInfo aProperty)
DisplayPropertyValue(aProperty);

else
Text.PrintErrorMsg($"The Type \"{typeof(T).Name}\" is Invalid.");

}

/** <summary> Displays the Parameter selected by the User. </summary>

<param name = "selectedItem"> The Param selected by the User. </param>
<param name = "elementIndex"> The Index of the Param selected. </param> */

public override void SelectiveAction<T>(T selectedItem, int elementIndex)
{

if(selectedItem is PropertyInfo aProperty)
Text.PrintLine(false, adviceText + aProperty.Name);

else
Text.PrintErrorMsg($"Invalid Param Type: \"{typeof(T).Name}\".");

}

/** <summary> Updates the Instance used as a Reference on this Menu. </summary>
<param name = "sourceInstance"> The Instance to be Set. </param> */

public void UpdateParamInstance(object sourceInstance) => paramInstance = sourceInstance;
}

}