using System.Reflection;
using System.Security.Cryptography;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Hash Type. </summary>

internal partial class HashTypeSelection : UserSelection
{
/// <summary> Creates a new Instance of the HashTypeSelection. </summary>

public HashTypeSelection()
{
headerText = Text.LocalizedData.PARAM_HASH_TYPE;
adviceText = Text.LocalizedData.ADVICE_CHOOSE_HASH_TYPE;
}

/** <summary> Displays the HashTypeSelection. </summary>
<returns> The Hash Type by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
DisplayClassMembers(out HashAlgorithmName selectedHash, BindingFlags.Public | BindingFlags.Static);

return selectedHash.Name;
}

}

}