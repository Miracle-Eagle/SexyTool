using System.Security.Cryptography;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a PaddingMode. </summary>

internal partial class PaddingModeSelection : UserSelection
{
/// <summary> Creates a new Instance of the PaddingModeSelection. </summary>

public PaddingModeSelection()
{
headerText = Text.LocalizedData.PARAM_DATA_PADDING;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_DATA_PADDING;
}

/** <summary> Displays the PaddingModeSelection. </summary>
<returns> The PaddingMode selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out PaddingMode selectedPadding);

return selectedPadding;
}

}

}