using SexyTool.Program.Core;
using SexyTool.Program.Graphics;
using SexyTool.Program.Graphics.Dialogs;
using System;
using System.Collections.Generic;

namespace SexyTool.Program
{
/// <summary> The Interface of this Program. </summary>

internal static class Interface
{
/** <summary> Gets a Collections of Dialogs from this Program. </summary>
<returns> The Collection of Dialogs Obtained. </returns> */

private static readonly Dictionary<string, Dialog> dialogs = Types_Handler.GetClassMap<Dialog>("SexyTool.Program.Graphics.Dialogs");

/** <summary> Gets a Collections of Menus from this Program. </summary>
<returns> The Collection of Menus Obtained. </returns> */

private static readonly Dictionary<string, Menu> menus = Types_Handler.GetClassMap<Menu>("SexyTool.Program.Graphics.Menus");

/** <summary> Gets a Collections of UserSelections from this Program. </summary>
<returns> The Collection of UserSelections Obtained. </returns> */

private static readonly Dictionary<string, UserSelection> userSelections = Types_Handler.GetClassMap<UserSelection>("SexyTool.Program.Graphics.UserSelections");


/// <summary> Displays all the Visual Elements of this Program. </summary>

public static void DisplayElements()
{
string consoleTitle;

if(Info.BuildConfig == "Debug")
{
consoleTitle = Info.ProgramTitle + Input_Manager.strSeparator_Blankspace + 'v' + Info.ProgramVersion + Input_Manager.strSeparator_Blankspace + '(' + Info.BuildConfig + ')';
}

else
{
consoleTitle = Info.ProgramTitle;
}

Console.Title = consoleTitle;
Text.PrintHeader(string.Format(Text.LocalizedData.PROGRAM_WELCOME_MESSAGE, Info.ProgramTitle) );

Text.PrintLine(true, Info.ProgramDescription);
GetDialog<ContinueDialog>().Popup();
}

/** <summary> Gets a Instance from a Dialog Type. </summary>
<returns> The Dialog Instance. </returns> */

public static T GetDialog<T>() where T : Dialog => (T)dialogs[typeof(T).Name];

/** <summary> Gets a Instance from a Menu Type. </summary>
<returns> The Menu Instance. </returns> */

public static T GetMenu<T>() where T : Menu => (T)menus[typeof(T).Name];

/** <summary> Gets a Instance from a UserSelection Type. </summary>
<returns> The UserSelection Instance. </returns> */

public static T GetUserSelection<T>() where T : UserSelection => (T)userSelections[typeof(T).Name];

/** <summary> Shows the Key Pressed by the User. </summary>
<param name = "keyInfo"> The Info about the Key Pressed. </param> */

public static void ShowKeyPressed(ConsoleKeyInfo keyInfo)
{
Text.PrintAdvice(false, Text.LocalizedData.ADVICE_KEYS_PRESSED);
ConsoleModifiers keyModifier = keyInfo.Modifiers;

string modifierType;

if( (keyModifier & ConsoleModifiers.Alt) != 0)
{
modifierType = "Alt";
Text.Print(false, modifierType);
}

if( (keyModifier & ConsoleModifiers.Shift) != 0)
{
modifierType = "Shift";
Text.Print(false, modifierType);
}

if( (keyModifier & ConsoleModifiers.Control) != 0)
{
modifierType = "Ctrl";
Text.Print(false, modifierType);
}

Text.Print(false, keyInfo.Key.ToString() );
Text.PrintLine();
}

}

}