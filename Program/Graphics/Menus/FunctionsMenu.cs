namespace SexyTool.Program.Graphics.Menus
{
/// <summary> The Functions Menu of this Program. </summary>

internal partial class FunctionsMenu : Menu
{
/** <summary> Gets or Sets the main Framework where the Program will be working on. </summary>
<returns> The main Framework of the Program. </returns> */

private static Framework mainFramework = Loader.appFrameworks[0];

/// <summary> Creates a new Instance of the FunctionsMenu. </summary>

public FunctionsMenu()
{
headerText = mainFramework.DisplayName;
adviceText = Text.LocalizedData.ADVICE_SELECT_FUNCTION;
}

/** <summary> Displays the FunctionsMenu. </summary>
<returns> The Function selected by the User. </returns> */

public override object DynamicSelection()
{
ActionWrapper<Function> displayAction = new( PrintAction );
ActionWrapper<Function, int> onSelectAction = new( SelectiveAction );

Function selectedFunction = new();
ShowOptions(mainFramework.FunctionsList, ref selectedFunction, displayAction.Init, onSelectAction.Init);

return selectedFunction;
}

/** <summary> Prints info related to the Function given as a Parameter (Name and ID). </summary>
<param name = "sourceItem"> The Function where the Info will be Obtained from. </param> */

public override void PrintAction<T>(T sourceItem)
{

if(sourceItem is Function aFunction)
Text.Print(true, "{0}. {1}", aFunction.ID, aFunction.DisplayName);

else
Text.PrintErrorMsg($"The Type \"{typeof(T).Name}\" is not a Function or a derived Type from Function.");

}

/** <summary> Displays the Function selected by the User. </summary>

<param name = "selectedItem"> The Function selected by the User. </param>
<param name = "elementIndex"> The Index of the Function selected. </param> */

public override void SelectiveAction<T>(T selectedItem, int elementIndex)
{

if(selectedItem is Function aFunction)
Text.PrintLine(false, adviceText + aFunction.ID);

else
Text.PrintErrorMsg($"Invalid Function Type: \"{typeof(T).Name}\".");

}

/** <summary> Updates the main Framework used as a Reference on this Menu. </summary>
<param name = "sourceFramework"> The Framework to be Set. </param> */

public void UpdateMainFramework(ref Framework sourceFramework)
{
mainFramework = sourceFramework;
headerText = mainFramework.DisplayName;
}

}

}