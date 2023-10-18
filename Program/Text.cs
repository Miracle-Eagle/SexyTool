using Newtonsoft.Json.Linq;
using SexyTool.Program.Core;
using SexyTool.Program.StringEntries;
using SexyTool.Program.StringEntries.MultiLanguage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SexyTool.Program
{
/// <summary> The Text that this Program displays. </summary>

internal class Text
{
/** <summary> Gets the Localized Strings of this Program. </summary>
<returns> The Localized Strings of this Program. </returns> */

public static LocalizedStrings LocalizedData
{

get
{
StringsList programText = GetStringsList(Program.config.UserLanguage);
return programText.stringValues;
}

}

/** <summary> Gets a Format that Indicates how an Array of Objects should be Displayed on Screen. </summary>

<param name = "targetObjs"> The Objects to be Displayed. </param>

<returns> The Display Format. </returns> */

private static string GetDisplayFormat(object[] targetObjs)
{
int objsCount = (targetObjs == null) ? 0 : targetObjs.Length;
var lengthRange = Enumerable.Range(0, objsCount);

var displayStr = lengthRange.Select(i => $"{{{i}}}");
return string.Join(Input_Manager.strSeparator_Comma, displayStr);
}

/** <summary> Gets a List of Strings according to the selected Language. </summary>

<param name = "sourceLanguage"> The selected Language. </param>

<returns> The List of Strings. </returns> */

private static StringsList GetStringsList(Language sourceLanguage)
{
#region ====== Localize Strings by Language ======

StringsList textData = sourceLanguage switch
{
Language.中文 => new 中文_中国(),
Language.Deutsch => new Deutsch_Deutschland(),
Language.Português => new Português_Brasil(),
Language.Français => new Français_France(),
Language.Español => new Español_España(),
Language.Italiano => new Italiano_Italia(),
_ => new English_UnitedStates()
};

#endregion

return textData;
}

/** <summary> Locates a Text by searching for its ID. </summary>

<param name = "sourceID"> The ID of the Text. </param>

<returns> The text Localized by ID. </returns> */

public static string LocateByID(string sourceID)
{
string missingTextAdvice = "<Missing Text: {0}>";
Type instanceType = LocalizedData.GetType();

FieldInfo instanceField = instanceType.GetField(sourceID);
string locValue;

if(instanceField == null)
locValue = string.Format(missingTextAdvice, sourceID);

else
locValue = (string)instanceField.GetValue(LocalizedData);

return locValue;
}

/** <summary> Prints an Array of Objects on the Screen with the specified Format. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Object to be Displayed. </param> */

public static void Print(bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
sourceFormat ??= GetDisplayFormat(targetObjs);

if(targetObjs == null)
return;

Console.Write(sourceFormat, targetObjs);

if(addLineBreak)
PrintLine();

}

/** <summary> Prints an Array of Objects on the Screen with the specified Format and Color. </summary>

<param name = "textColor"> The Color of the Text displayed. </param>
<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Object to be Displayed. </param> */

public static void Print(ConsoleColor textColor, bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
Console.ForegroundColor = textColor;
Print(addLineBreak, sourceFormat, targetObjs);

Console.ResetColor();
}

/** <summary> Prints an Advice on the Screen. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "adviceText"> The Text of the Advice. </param> */

public static void PrintAdvice(bool addLineBreak, string adviceText)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Magenta) ? ConsoleColor.Cyan : ConsoleColor.Magenta;
Print(textColor, addLineBreak, "i {0}", adviceText);
}

/** <summary> Prints a Dialog on the Screen. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "dialogText"> The Text of the Dialog. </param> */

public static void PrintDialog(bool addLineBreak, string dialogText)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Cyan) ? ConsoleColor.Magenta : ConsoleColor.Cyan;
Print(textColor, addLineBreak, "* {0}", dialogText);
}

/** <summary> Prints the Contents of a Dictionary. </summary>
<param name = "sourceDictionary"> The Dicitonary to be Printed. </param> */

public static void PrintDictionary(Dictionary<string, object> sourceDictionary)
{

if(sourceDictionary == null)
PrintDialog(true, LocalizedData.DIALOG_NO_DATA_AVAILABLE);

else
{

foreach(KeyValuePair<string, object> singleElement in sourceDictionary)
Print(true, "<{0}, {1}>", singleElement.Key, singleElement.Value);

}

}

/** <summary> Prints an Error Message on the Screen. </summary>
<param name = "sourceMsg"> The Message to Display, indicating a Task was Failed. </param> */

public static void PrintErrorMsg(string sourceMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.DarkRed) ? ConsoleColor.Blue : ConsoleColor.DarkRed;
PrintLine(textColor, true, "X {0}", sourceMsg);
}

/** <summary> Prints a Header on the Screen. </summary>
<param name = "headerText"> The Text of the Header. </param> */

public static void PrintHeader(string headerText) => PrintLine(true, "<--------------------- {0} --------------------->", headerText);

/** <summary> Prints the Contents of a JSON Object on the Screen. </summary>
<param name = "sourceObj"> The JSON Object to be Displayed. </param> */

public static void PrintJson(JObject sourceObj)
{

if(sourceObj == null)
PrintDialog(true, LocalizedData.DIALOG_NO_DATA_AVAILABLE);

else
{

foreach(KeyValuePair<string, JToken> jsonProperty in sourceObj)
Print(true, "{0}: {1}", jsonProperty.Key, jsonProperty.Value);

}

}

/// <summary> Prints a new Line on Screen. </summary>

public static void PrintLine() => Console.WriteLine();

/** <summary> Prints a formatted Line of Text represented by the specified Objects, followed by a LineBreak. </summary>

<param name = "addLineBreak"> A Boolean that Determines if an extra LineBreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Objects to be Displayed. </param> */

public static void PrintLine(bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
sourceFormat ??= GetDisplayFormat(targetObjs);

if(targetObjs == null)
return;

Console.WriteLine(sourceFormat, targetObjs);

if(addLineBreak)
PrintLine();

}

/** <summary> Prints a formatted Line of a Text represented by the specified Objects, followed by a LineBreak. </summary>

<param name = "textColor"> The Color of the Text displayed. </param>
<param name = "addLineBreak"> A Boolean that Determines if an extra LineBreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Objects to be Displayed. </param> */

public static void PrintLine(ConsoleColor textColor, bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
Console.ForegroundColor = textColor;
PrintLine(addLineBreak, sourceFormat, targetObjs);

Console.ResetColor();
}

/** <summary> Prints a Success Message on the Screen. </summary>
<param name = "sourceMsg"> The Message to Display, indicating a Task was Successfully completed. </param> */

public static void PrintSuccessMsg(string sourceMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Green) ? ConsoleColor.Blue : ConsoleColor.Green;
PrintLine(textColor, true, "OK {0}", sourceMsg);
}

/** <summary> Prints a Warning Message on the Screen. </summary>
<param name = "warningMsg"> The Warning Message. </param> */

public static void PrintWarning(string warningMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.DarkYellow) ? ConsoleColor.DarkBlue : ConsoleColor.DarkYellow;
PrintLine(textColor, true, "! {0}", warningMsg);
}

}

}