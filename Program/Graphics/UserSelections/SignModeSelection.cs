using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Sign Mode. </summary>

internal partial class SignModeSelection : UserSelection
{
/// <summary> Creates a new Instance of the SignModeSelection. </summary>

public SignModeSelection()
{
headerText = Text.LocalizedData.PARAM_SIGN_MODE;
bodyText = Text.LocalizedData.USER_SELECTION_SIGN_MODE;

adviceText = Text.LocalizedData.ADVICE_CONFIRM_SIGN_MODE;
}

/** <summary> Displays the SignModeSelection. </summary>
<returns> The Sign Mode selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Limit<bool> inputRange = Limit<bool>.GetLimitRange();

Text.PrintLine(true, string.Format(bodyText, inputRange.MinValue, inputRange.MaxValue) );
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
bool signMode = Input_Manager.FilterBool(Console.ReadLine() );

Text.PrintLine();
return signMode;
}

}

}