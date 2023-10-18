using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Endian Encoding. </summary>

internal partial class EndianSelection : UserSelection
{
/// <summary> Creates a new Instance of the EndianSelection. </summary>

public EndianSelection()
{
headerText = "Text.LocalizedData.PARAM_ENDIAN_ENCODING";
}

/** <summary> Displays the EndianSelection. </summary>
<returns> The Endian Encoding selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out Endian selectedEndian);

return selectedEndian;
}

}

}