namespace SexyTool.Program.Graphics.Menus
{
/// <summary> The Frameworks Menu of this Program. </summary>

internal partial class FrameworksMenu : Menu
{
/// <summary> Creates a new Instance of the FrameworksMenu. </summary>

public FrameworksMenu()
{
headerText = Text.LocalizedData.HEADER_FRAMEWORK_SELECTION;;
adviceText = Text.LocalizedData.ADVICE_SELECT_FRAMEWORK;
}

/** <summary> Displays the FrameworksMenu. </summary>
<returns> The Framework selected by the User. </returns> */

public override object DynamicSelection()
{
ActionWrapper<Framework> displayAction = new( PrintAction );
ActionWrapper<Framework, int> onSelectAction = new( SelectiveAction );

Framework selectedFramework = new();
ShowOptions(Loader.appFrameworks, ref selectedFramework, displayAction.Init, onSelectAction.Init);

return selectedFramework;
}

/** <summary> Prints info related to the Framework given as a Parameter (Name and ID). </summary>
<param name = "sourceItem"> The Framework where the Info will be Obtained from. </param> */

public override void PrintAction<T>(T sourceItem)
{

if(sourceItem is Framework aFramework)
Text.Print(true, "{0}) {1}", aFramework.ID, aFramework.DisplayName);

else
Text.PrintErrorMsg($"The Type \"{typeof(T).Name}\" is not a Framework or a derived Type from Framework.");

}

/** <summary> Displays the Framework selected by the User. </summary>

<param name = "selectedItem"> The Framework selected by the User. </param>
<param name = "elementIndex"> The Index of the Framework selected. </param> */

public override void SelectiveAction<T>(T selectedItem, int elementIndex)
{

if(selectedItem is Framework aFramework)
Text.PrintLine(false, adviceText + aFramework.ID);

else
Text.PrintErrorMsg($"Invalid Framework Type: \"{typeof(T).Name}\".");

}

}

}