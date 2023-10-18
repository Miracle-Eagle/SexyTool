using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Cursor Size. </summary>

internal partial class CursorSizeSelection : UserSelection
{
/// <summary> Creates a new Instance of the CursorSizeSelection. </summary>

public CursorSizeSelection()
{
headerText = Text.LocalizedData.CONFIG_CURSOR_SIZE;
bodyText = Text.LocalizedData.USER_SELECTION_ELEMENT_SIZE;

adviceText = Text.LocalizedData.ADVICE_CHOOSE_CURSOR_SIZE;
}

/** <summary> Displays the CursorSizeSelection. </summary>
<returns> The Cursor Size selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Text.PrintLine(true, bodyText);

Text.PrintAdvice(false, adviceText);
int sizeFlags = Input_Manager.FilterNumber<int>(Console.ReadLine() );

Text.PrintLine();

int selectedSize = sizeFlags switch
{
1 => 25,
2 => 50,
3 => 100,
_ => (int)GetSelectionParam()
};

return selectedSize;
}

}

}