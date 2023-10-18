using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Foreground Color. </summary>

internal partial class ForegroundColorSelection : UserSelection
{
/// <summary> Creates a new Instance of the ForegroundColorSelection. </summary>

public ForegroundColorSelection()
{
headerText = Text.LocalizedData.CONFIG_FOREGROUND_COLOR;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_FOREGROUND_COLOR;
}

/** <summary> Displays the ForegroundColorSelection. </summary>
<returns> The Foreground Color selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out ConsoleColor selectedColor);

return selectedColor;
}

}

}