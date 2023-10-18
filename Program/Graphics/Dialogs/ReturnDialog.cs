using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Press the Specified key for Returning back. </summary>

internal partial class ReturnDialog : Dialog
{
/// <summary> Creates a new Instance of the <c>ReturnDialog</c>. </summary>

public ReturnDialog()
{
headerText = Text.LocalizedData.HEADER_RETURN_BACK;
adviceText = string.Format(Text.LocalizedData.ADVICE_PRESS_RETURN_KEY, Termination.returnKey);
}

/** <summary> Displays the <c>ReturnDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> <b>false</b> if the User didn't Press the Returning Key; otherwise, returns <b>true</b>. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);
Text.PrintLine();

Interface.ShowKeyPressed(inputKeyInfo);
return inputKeyInfo.Key.Equals(Termination.returnKey);
}

/** <summary> Displays the <c>ReturnDialog</c> with the Specified Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>
<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> <b>false</b> if the User didn't Press the Returning Key; otherwise, returns <b>true</b>. </returns> */

public override object Popup(string sourceAdvice, string sourceBody = default, Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintDialog(true, sourceBody);

Text.PrintAdvice(false, sourceAdvice);
ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);

Text.PrintLine();
Interface.ShowKeyPressed(inputKeyInfo);

return inputKeyInfo.Key.Equals(Termination.returnKey);
}

}

}