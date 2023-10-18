using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Language. </summary>

internal partial class LanguageSelection : UserSelection
{
/// <summary> Creates a new Instance of the LanguageSelection. </summary>

public LanguageSelection()
{
headerText = Text.LocalizedData.CONFIG_USER_LANGUAGE;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_LANGUAGE;
}

/** <summary> Displays the LanguageSelection. </summary>
<returns> The Language selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out Language selectedLanguage);

return selectedLanguage;
}

}

}