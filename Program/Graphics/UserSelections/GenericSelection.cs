using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a generic Value. </summary>

internal partial class GenericSelection : UserSelection
{
/// <summary> Creates a new Instance of the IterationsCountSelection. </summary>

public GenericSelection()
{
adviceText = Text.LocalizedData.ADVICE_CHOOSE_VALUE;
}

/** <summary> Displays the GenericSelection. </summary>
<param name = "sourceHeader"> The Header to be Displayed on Screen (Optional). </param>

<returns> The generic Value entered by the User. </returns> */

public T GetGenericParams<T>(string sourceHeader = default) where T : struct
{
sourceHeader ??= typeof(T).Name;
Text.PrintHeader(sourceHeader);

Limit<T> inputRange = Limit<T>.GetLimitRange();
inputRange.ShowInputRange();

Text.PrintAdvice(false, adviceText);
T selectedValue = Input_Manager.FilterNumber<T>(Console.ReadLine() );

Text.PrintLine();
return selectedValue;
}

/** <summary> Displays the GenericSelection with the Specific Advice. </summary>
<param name = "sourceAdvice"> The Advice to Display on Screen. </param>

<param name = "sourceHeader"> The Header to be Displayed on Screen (Optional). </param>
<returns> The generic Value entered by the User. </returns> */

public T GetGenericParams<T>(string sourceAdvice, string sourceHeader = default) where T : struct
{
sourceHeader ??= typeof(T).Name;
Text.PrintHeader(sourceHeader);

Limit<T> inputRange = Limit<T>.GetLimitRange();
inputRange.ShowInputRange();

Text.PrintAdvice(false, sourceAdvice);
T selectedValue = Input_Manager.FilterNumber<T>(Console.ReadLine() );

Text.PrintLine();
return selectedValue;
}

}

}