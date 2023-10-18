using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Background Color. </summary>

internal partial class BackgroundColorSelection : UserSelection
{
/// <summary> Creates a new Instance of the BackgroundColorSelection. </summary>

public BackgroundColorSelection()
{
headerText = Text.LocalizedData.CONFIG_BACKGROUND_COLOR;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_BACKGROUND_COLOR;
}

/** <summary> Displays the BackgroundColorSelection. </summary>
<returns> The Background Color selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out ConsoleColor selectedColor);

return selectedColor;
}

}

}