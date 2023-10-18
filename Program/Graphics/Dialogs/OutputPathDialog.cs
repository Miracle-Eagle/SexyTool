using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter an Output Path. </summary>

internal partial class OutputPathDialog : Dialog
{
/// <summary> Creates a new Instance of the OutputPathDialog. </summary>

public OutputPathDialog()
{
headerText = Text.LocalizedData.PARAM_OUTPUT_PATH;
adviceText = Text.LocalizedData.ADVICE_ENTER_OUTPUT_PATH;
}

/** <summary> Displays the OutputPathDialog. </summary>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Output Path entered by the User. </returns> */

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

/** <summary> Displays the OutputPathDialog with the Specific Advice. </summary>
<param name = "sourceAdvice"> The Advice to Display on Screen. </param>

<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Output Path entered by the User. </returns> */

public override object Popup(string sourceAdvice, string sourceBody = default, Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintLine(false, sourceBody);

Text.PrintAdvice(false, sourceAdvice);
string selectedPath = Path_Helper.FilterPath(Console.ReadLine() );

Text.PrintLine();

if(inputRange != null)
{

while(selectedPath.Length < inputRange.MinValue || selectedPath.Length > inputRange.MaxValue)
selectedPath = Popup(sourceAdvice, sourceBody, inputRange) as string;

}

return selectedPath;
}

}

}