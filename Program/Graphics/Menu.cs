using System;
using System.Collections.Generic;

namespace SexyTool.Program.Graphics
{
/// <summary> Represents a Menu of this Program. </summary>

public class Menu : Graphics
{
/** <summary> Displays the Menu. </summary>
<returns> The Option Selected by the User. </returns> */

public virtual object DynamicSelection() => "Template";

/** <summary> An Action used for Printing the Options available. </summary>
<param name = "sourceItem"> The Item to be Diplayed. </param> */

public virtual void PrintAction<T>(T sourceItem) => Text.Print(true, "{0}", sourceItem);

/** <summary> An Action used for Printing the Option selected by the User. </summary>

<param name = "selectedItem"> The Item selected by the User. </param>
<param name = "elementIndex"> The Index of the Element selected. </param> */

public virtual void SelectiveAction<T>(T selectedItem, int elementIndex) => Text.Print(true, "{0}: {1}", adviceText, elementIndex);

/** <summary> Shows the Options of this Menu. </summary>

<param name = "userOptions"> The Array of Options to be Diplayed. </param>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "displayAction"> An Action that Defines how Options should be Displayed on Screen. </param>
<param name = "onSelectAction"> An Action that Defines how to Display the Option selected by the User. </param> */

protected void ShowOptions<T>(T[] userOptions, ref T expectedValue, Action<T> displayAction = default, Action<T, int> onSelectAction = default) => ShowOptions(new List<T>(userOptions), ref expectedValue, displayAction, onSelectAction);

/** <summary> Shows the Options of this Menu. </summary>

<param name = "userOptions"> The List of Options to be Diplayed. </param>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "displayAction"> An Action that Defines how Options should be Displayed on Screen. </param>
<param name = "onSelectAction"> An Action that Defines how to Display the Option selected by the User. </param> */

protected void ShowOptions<T>(List<T> userOptions, ref T expectedValue, Action<T> displayAction = default, Action<T, int> onSelectAction = default)
{
displayAction ??= PrintAction;
onSelectAction ??= SelectiveAction;

int optionsCount = userOptions.Count;
int selectedOptionIndex = 0;

bool confirmSelection = false;

while(!confirmSelection)
{
Console.Clear();
Text.PrintHeader(headerText);

for(int i = 0; i < optionsCount; i++)
{

if(i == selectedOptionIndex)
{
Console.BackgroundColor = (Console.ForegroundColor == ConsoleColor.Yellow) ? ConsoleColor.Blue : ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.DarkGray;
}

displayAction(userOptions[i]);

if(i == selectedOptionIndex)
Console.ResetColor();

if(i == optionsCount - 1)
Text.PrintLine();

}

Text.PrintDialog(false, Text.LocalizedData.DIALOG_SELECT_OPTION);
ConsoleKeyInfo keyInfo = Console.ReadKey(true);

Text.PrintLine();

switch(keyInfo.Key)
{
case ConsoleKey.UpArrow:
selectedOptionIndex = (selectedOptionIndex - 1 + optionsCount) % optionsCount;
break;

case ConsoleKey.DownArrow:
selectedOptionIndex = (selectedOptionIndex + 1) % optionsCount;
break;

case ConsoleKey.Enter:
confirmSelection = true;
break;
}

expectedValue = userOptions[selectedOptionIndex];
onSelectAction(expectedValue, selectedOptionIndex);

Text.PrintLine();
}

}

}

}