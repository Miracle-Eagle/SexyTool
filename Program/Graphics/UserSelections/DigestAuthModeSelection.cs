using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Digest Auth Mode. </summary>

internal partial class DigestAuthModeSelection : UserSelection
{
/// <summary> Creates a new Instance of the DigestAuthModeSelection. </summary>

public DigestAuthModeSelection()
{
headerText = Text.LocalizedData.PARAM_DIGEST_AUTH_MODE;
bodyText = Text.LocalizedData.USER_SELECTION_DIGEST_AUTH_MODE;

adviceText = Text.LocalizedData.ADVICE_CONFIRM_DIGEST_AUTH_MODE;
}

/** <summary> Displays the DigestAuthModeSelection. </summary>
<returns> The Digest Auth Mode selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Limit<bool> inputRange = Limit<bool>.GetLimitRange();

Text.PrintLine(true, string.Format(bodyText, inputRange.MinValue, inputRange.MaxValue) );
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
bool authMode = Input_Manager.FilterBool(Console.ReadLine() );

Text.PrintLine();
return authMode;
}

}

}