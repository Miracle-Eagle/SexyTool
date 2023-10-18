using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a DateTime. </summary>

internal partial class DateTimeSelection : UserSelection
{
/// <summary> Creates a new Instance of the DateTimeSelection. </summary>

public DateTimeSelection()
{
headerText = Text.LocalizedData.PARAM_DATE_TIME;
bodyText = Text.LocalizedData.USER_SELECTION_DATE_TIME;

adviceText = Text.LocalizedData.ADVICE_SELECT_DATE_TIME;
}

/** <summary> Displays the DateTimeSelection. </summary>
<returns> The DateTime selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Text.PrintLine(true, bodyText);

Limit<DateTime> inputRange = Limit<DateTime>.GetLimitRange();
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
DateTime selectedValue = Input_Manager.FilterDateTime(Console.ReadLine() );

Text.PrintLine();
return selectedValue;
}

}

}