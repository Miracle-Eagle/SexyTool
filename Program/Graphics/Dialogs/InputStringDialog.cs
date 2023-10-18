using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a String. </summary>

internal partial class InputStringDialog : Dialog
{
/// <summary> Creates a new Instance of the InputStringDialog. </summary>

public InputStringDialog()
{
headerText = Text.LocalizedData.PARAM_INPUT_STRING;
adviceText = Text.LocalizedData.ADVICE_ENTER_STRING;
}

/** <summary> Displays the InputStringDialog. </summary>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The String entered by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

string inputString = Input_Manager.CheckEmptyString(Console.ReadLine() );
Text.PrintLine();

if(inputRange != null)
{

while(inputString.Length < inputRange.MinValue || inputString.Length > inputRange.MaxValue)
inputString = Popup(inputRange) as string;

}

return inputString;
}

/** <summary> Displays the InputStringDialog with the Specific Advice. </summary>
<param name = "sourceAdvice"> The Advice to Display on Screen. </param>

<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The String entered by the User. </returns> */

public override object Popup(string sourceAdvice, string sourceBody = default, Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintLine(false, sourceBody);

Text.PrintAdvice(false, sourceAdvice);
string inputString = Input_Manager.CheckEmptyString(Console.ReadLine() );

Text.PrintLine();

if(inputRange != null)
{

while(inputString.Length < inputRange.MinValue || inputString.Length > inputRange.MaxValue)
inputString = Popup(sourceAdvice, sourceBody, inputRange) as string;

}

return inputString;
}

}

}