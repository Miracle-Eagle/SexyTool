using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Screen Size. </summary>

internal partial class ScreenSizeSelection : UserSelection
{
/// <summary> Creates a new Instance of the ScreenSizeSelection. </summary>

public ScreenSizeSelection()
{
headerText = Text.LocalizedData.CONFIG_SCREEN_SIZE;
bodyText = Text.LocalizedData.USER_SELECTION_ELEMENT_SIZE;

adviceText = Text.LocalizedData.ADVICE_CHOOSE_SCREEN_SIZE;
}

/** <summary> Displays the ScreenSizeSelection. </summary>
<returns> The Screen Size selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Text.PrintLine(true, bodyText);

Text.PrintAdvice(false, adviceText);
int sizeFlags = Input_Manager.FilterNumber<int>(Console.ReadLine() );

Text.PrintLine();

int[] selectedSize = sizeFlags switch
{
1 => new int[] { 60, 15, 60, 9001 },
2 => new int[] { 120, 30, 120, 9001 },
3 => new int[] { 159, 43, 159, 9001 },
_ => GetSelectionParam() as int[]
};

return selectedSize;
}

}

}