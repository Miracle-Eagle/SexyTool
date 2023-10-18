using System.Security.Cryptography;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a CipherMode. </summary>

internal partial class CipherModeSelection : UserSelection
{
/// <summary> Creates a new Instance of the CipherModeSelection. </summary>

public CipherModeSelection()
{
headerText = Text.LocalizedData.PARAM_CIPHERING_MODE;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_CIPHERING_MODE;
}

/** <summary> Displays the CipherModeSelection. </summary>
<returns> The CipherMode selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out CipherMode selectedCipher);

return selectedCipher;
}

}

}