using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Alerts the User that the Program started in the Quick Mode. </summary>

internal partial class QuickModeDialog : Dialog
{
/// <summary> Creates a new Instance of the <c>QuickModeDialog</c>. </summary>

public QuickModeDialog()
{
headerText = Text.LocalizedData.HEADER_QUICK_MODE;
bodyText = Text.LocalizedData.DIALOG_QUICK_MODE;
}

/** <summary> Displays the <c>QuickModeDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> The ID of the Quick Function selected by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintDialog(true, bodyText);

ConsoleKeyInfo inputKeyInfo = (ConsoleKeyInfo)Interface.GetDialog<ContinueDialog>().Popup();
return inputKeyInfo.KeyChar;
}

/** <summary> Displays the <c>QuickModeDialog</c> with the Specified Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>
<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> The ID of the Quick Function selected by the User. </returns> */

public override object Popup(string sourceAdvice, string sourceBody = default, Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintDialog(false, sourceBody);

Text.PrintAdvice(false, sourceAdvice);
ConsoleKeyInfo inputKeyInfo = (ConsoleKeyInfo)Interface.GetDialog<ContinueDialog>().Popup();

return inputKeyInfo.KeyChar;
}

}

}