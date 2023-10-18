using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a Salt Value. </summary>

internal partial class SaltValueDialog : Dialog
{
/// <summary> Creates a new Instance of the SaltValueDialog. </summary>

public SaltValueDialog()
{
headerText = Text.LocalizedData.PARAM_SALT_VALUE;
adviceText = Text.LocalizedData.ADVICE_ENTER_SALT_VALUE;
}

/** <summary> Displays the SaltValueDialog. </summary>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Salt Value entered by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

string selectedValue = Input_Manager.CheckEmptyString(Console.ReadLine() );
Text.PrintLine();

byte[] selectedSaltBytes = Console.InputEncoding.GetBytes(selectedValue);

if(inputRange != null)
{

while(selectedSaltBytes.Length < inputRange.MinValue || selectedSaltBytes.Length > inputRange.MaxValue)
selectedSaltBytes = Popup(inputRange) as byte[];

}

return selectedSaltBytes;
}

}

}