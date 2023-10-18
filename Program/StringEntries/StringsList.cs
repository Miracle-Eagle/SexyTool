using System.Globalization;

namespace SexyTool.Program.StringEntries
{
/// <summary> Represents a List of Strings that can Vary according to user's Language. </summary>

internal class StringsList
{
/** <summary> Gets a Value that Contains info of the Culture Name of a User. </summary>
<returns> The Culture Name of the User. </returns> */

protected string cultureName;

/** <summary> Gets a Value that Contains info about the Language Culture of a User. </summary>
<returns> The Language Culture of the User. </returns> */

public CultureInfo languageInfo;

/** <summary> Gets or Sets a Value that Contains info about the Strings localized by Language. </summary>
<returns> The Strings localized by Language. </returns> */

public LocalizedStrings stringValues;
}

}