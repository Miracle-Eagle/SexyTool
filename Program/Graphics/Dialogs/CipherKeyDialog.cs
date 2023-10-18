using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.Dialogs
{
/// <summary> Ask the User to Enter a Cipher Key. </summary>

internal partial class CipherKeyDialog : Dialog
{
/// <summary> Creates a new Instance of the CipherKeyDialog. </summary>

public CipherKeyDialog()
{
headerText = Text.LocalizedData.PARAM_CIPHER_KEY;
adviceText = Text.LocalizedData.ADVICE_ENTER_CIPHER_KEY;
}

/** <summary> Displays the CipherKeyDialog. </summary>

<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Cipher Key entered by the User. </returns> */

public override object Popup(Limit<int> inputRange = null)
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

string selectedKey = Input_Manager.CheckEmptyString(Console.ReadLine() );
Text.PrintLine();

byte[] selectedKeyBytes = Console.InputEncoding.GetBytes(selectedKey);

if(inputRange != null)
{

while(selectedKeyBytes.Length < inputRange.MinValue || selectedKeyBytes.Length > inputRange.MaxValue)
Text.PrintWarning(Text.LocalizedData.WARNING_INVALID_KEY_LENGTH);

}

return selectedKeyBytes;
}

}

}