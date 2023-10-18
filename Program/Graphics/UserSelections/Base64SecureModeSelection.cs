using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Base64 Secure Mode. </summary>

internal partial class Base64SecureModeSelection : UserSelection
{
/// <summary> Creates a new Instance of the Base64SecureModeSelection. </summary>

public Base64SecureModeSelection()
{
headerText = Text.LocalizedData.PARAM_BASE64_SECURITY_MODE;
bodyText = Text.LocalizedData.USER_SELECTION_BASE64_SECURITY_MODE;

adviceText = Text.LocalizedData.ADVICE_CONFIRM_BASE64_SECURITY_MODE;
}

/** <summary> Displays the Base64SecureModeSelection. </summary>
<returns> The Base64 Secure Mode selected by the User. </returns> */

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