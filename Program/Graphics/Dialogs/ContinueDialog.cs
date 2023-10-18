using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Suspends every Task that is being Executed by this Program until the User presses Enter or Any Key. </summary>

internal partial class ContinueDialog : Dialog
{
/// <summary> Creates a new Instance of the <c>ContinueDialog</c>. </summary>

public ContinueDialog()
{
bodyText = Text.LocalizedData.DIALOG_SYSTEM_PAUSE;
}

/** <summary> Displays the <c>ContinueDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow (Not used on this Dialog). </param>

<returns> Info related to the Key pressed by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Console.TreatControlCAsInput = true;
Text.PrintDialog(false, bodyText);

ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);
Text.PrintLine();

Interface.ShowKeyPressed(inputKeyInfo);
return inputKeyInfo;
}

}

}