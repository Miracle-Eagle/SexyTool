using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Asks the User if Existing Files should be Replaced or not. </summary>

internal partial class FilesReplacementSelection : UserSelection
{
/// <summary> Creates a new Instance of the FilesReplacementSelection. </summary>

public FilesReplacementSelection()
{
headerText = Text.LocalizedData.PARAM_FILES_REPLACEMENT;
bodyText = Text.LocalizedData.USER_SELECTION_FILES_REPLACEMENT;

adviceText = Text.LocalizedData.ADVICE_CONFIRM_FILES_REPLACEMENT;
}

/** <summary> Displays the FilesReplacementSelection. </summary>
<returns> The Files Replacement set by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Limit<bool> inputRange = Limit<bool>.GetLimitRange();

Text.PrintLine(true, string.Format(bodyText, inputRange.MinValue, inputRange.MaxValue) );
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
bool doReplaceFiles = Input_Manager.FilterBool(Console.ReadLine() );

Text.PrintLine();
return doReplaceFiles;
}

}

}