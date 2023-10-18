using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Cursor View Mode. </summary>

internal partial class CursorViewSelection : UserSelection
{
/// <summary> Creates a new Instance of the CursorViewSelection. </summary>

public CursorViewSelection()
{
headerText = Text.LocalizedData.CONFIG_CURSOR_VISUALIZATION;
bodyText = Text.LocalizedData.USER_SELECTION_CURSOR_VISUALIZATION;

adviceText = Text.LocalizedData.ADVICE_CHOOSE_CURSOR_VISUALIZATION;
}

/** <summary> Displays the CursorViewSelection. </summary>
<returns> The Cursor View Mode selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Limit<bool> inputRange = Limit<bool>.GetLimitRange();

Text.PrintLine(true, string.Format(bodyText, inputRange.MinValue, inputRange.MaxValue) );
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
bool isSecureBase = Input_Manager.FilterBool(Console.ReadLine() );

Text.PrintLine();
return isSecureBase;
}

}

}