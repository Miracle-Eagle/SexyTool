using System.IO.Compression;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Compression Level. </summary>

internal partial class CompressionLevelSelection : UserSelection
{
/// <summary> Creates a new Instance of the CompressionLevelSelection. </summary>

public CompressionLevelSelection()
{
headerText = Text.LocalizedData.PARAM_COMPRESSION_LEVEL;
}

/** <summary> Displays the Compression Level Selection. </summary>
<returns> The Compression Level selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayEnumOptions(out CompressionLevel selectedLvl);

return selectedLvl;
}

}

}