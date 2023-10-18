using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a number of Iterations. </summary>

internal partial class IterationsCountSelection : UserSelection
{
/// <summary> Creates a new Instance of the IterationsCountSelection. </summary>

public IterationsCountSelection()
{
headerText = Text.LocalizedData.PARAM_ITERATIONS_COUNT;
adviceText = Text.LocalizedData.ADVICE_ENTER_ITERATIONS_COUNT;
}

/** <summary> Displays the IterationsCountSelection. </summary>
<returns> The number of Iterations selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);
Text.PrintAdvice(false, adviceText);

int selectedCount = Input_Manager.FilterNumber<int>(Console.ReadLine() );
Text.PrintLine();

return selectedCount;
}

/** <summary> Displays the IterationsCountSelection with the Specific Limit. </summary>
<param name = "selectionRange"> A limit that represents the Minimun and Maximum Count of Iterations allowed. </param>

<returns> The number of Iterations selected by the User. </returns> */

public override object GetSelectionParam(Limit<int> selectionRange)
{
Text.PrintHeader(headerText);
selectionRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
int selectedCount = Input_Manager.FilterNumber<int>(Console.ReadLine() );

Text.PrintLine();
Crypto_Parameters.CheckIterationsCount(ref selectedCount, selectionRange);

return selectedCount;
}

}

}