using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Closes the Program when the User presses Enter or any Key. </summary>

internal partial class CloseProgramDialog : Dialog
{
/// <summary> Creates a new Instance of the <c>CloseProgramDialog</c>. </summary>

public CloseProgramDialog()
{
headerText = Text.LocalizedData.HEADER_EXECUTION_COMPLETE;
bodyText = string.Format(Text.LocalizedData.DIALOG_PROGRAM_TERMINATION, Termination.returnKey);
}

/** <summary> Displays the <c>CloseProgramDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> The Key Pressed by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintDialog(false, bodyText);

ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);
Text.PrintLine();

Interface.ShowKeyPressed(inputKeyInfo);
return inputKeyInfo;
}

}

}