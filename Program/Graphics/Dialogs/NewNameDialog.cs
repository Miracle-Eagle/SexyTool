using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a new Name. </summary>

internal partial class NewNameDialog : Dialog
{
/// <summary> Creates a new Instance of the NewNameDialog. </summary>

public NewNameDialog()
{
headerText = Text.LocalizedData.PARAM_NEW_NAME;
adviceText = Text.LocalizedData.ADVICE_ENTER_NEW_NAME;
}

/** <summary> Displays the NewNameDialog. </summary>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The new Name entered by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

string selectedName = Input_Manager.FilterName(Console.ReadLine() );
Text.PrintLine();

if(inputRange != null)
{

while(selectedName.Length < inputRange.MinValue || selectedName.Length > inputRange.MaxValue)
selectedName = Popup(inputRange) as string;

}

return selectedName;
}

}

}