using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter an Input Path. </summary>

internal partial class InputPathDialog : Dialog
{
/// <summary> Creates a new Instance of the InputPathDialog. </summary>

public InputPathDialog()
{
headerText = Text.LocalizedData.PARAM_INPUT_PATH;
adviceText = Text.LocalizedData.ADVICE_ENTER_INPUT_PATH;
}

/** <summary> Displays the InputPathDialog. </summary>

<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Input Path entered by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

string selectedPath = Path_Helper.FilterPath(Console.ReadLine() );
Text.PrintLine();

if(inputRange != null)
{

while(selectedPath.Length < inputRange.MinValue || selectedPath.Length > inputRange.MaxValue)
selectedPath = Popup(inputRange) as string;

}

return selectedPath;
}

}

}