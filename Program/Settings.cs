using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SexyTool.Program
{
/// <summary> Initializes Saving and Loading Functions for the Settings of this Program. </summary>

[Serializable]

public class Settings
{
/** <summary> Gets or Sets the Background Color of the Console App. </summary>
<returns> The Background Color of the Console App. </returns> */

public ConsoleColor BackgroundColor{ get; set; }

/** <summary> Gets or Sets the Foreground Color of the Console App. </summary>
<returns> The Foreground Color of the Console App. </returns> */

public ConsoleColor ForegroundColor{ get; set; }

/** <summary> Gets or Sets the Size of the Screen. </summary>
<returns> The Size of the Screen. </returns> */

public int[] ScreenSize{ get; set; }

/** <summary> Gets or Sets the Size of the Cursor. </summary>
<returns> The Size of the Cursor. </returns> */

public int CursorSize{ get; set; }

/** <summary> Gets or Sets a Option that determines if the Cursor is visible or not. </summary>
<returns> The Cursor Visualization. </returns> */

public bool CursorVisualization{ get; set; }

/** <summary> Gets or Sets the user's Language. </summary>
<returns> The Language of the user. </returns> */

public Language UserLanguage{ get; set; }

/** <summary> Gets the Path of the Config File. </summary>
<returns> The Path of the Config File. </returns> */

protected static string ConfigFilePath
{

get
{
Type classType = typeof(Settings);
return Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + classType.Name + ".json";
}

}

/// <summary> Creates a new Instance of the Settings Class. </summary>

public Settings()
{
BackgroundColor = ConsoleColor.Black;
ForegroundColor = ConsoleColor.White;

ScreenSize = new int[] { Console.WindowWidth, Console.WindowHeight, Console.BufferWidth, Console.BufferHeight };
CursorSize = Console.CursorSize;

CursorVisualization = Console.CursorVisible;
UserLanguage = Language.English;
}

/** <summary> Checks each nullable Field of the Settings instance given and Validates them. </summary>
<param name = "sourceConfig"> The config to be Analized. </param> */

private static void CheckSettings(ref Settings sourceConfig)
{
Settings defaultConfig = new();
sourceConfig.ScreenSize ??= defaultConfig.ScreenSize;
}

/** <summary> Loads the Settings of this Program. </summary>
<param name = "sourceConfig"> The config to be Writen. </param>*/

private static void Load(Settings sourceConfig)
{
Console.BackgroundColor = sourceConfig.BackgroundColor;
Console.ForegroundColor = sourceConfig.ForegroundColor;

Console.WindowWidth = sourceConfig.ScreenSize[0];
Console.WindowHeight = sourceConfig.ScreenSize[1];

Console.BufferWidth = sourceConfig.ScreenSize[2];
Console.BufferHeight = sourceConfig.ScreenSize[3];

Console.CursorSize = sourceConfig.CursorSize;
Console.CursorVisible = sourceConfig.CursorVisualization;
}

/** <summary> Writes the Settings of this Program. </summary>
<param name = "sourceConfig"> The config to be Writen. </param> */

public static void Write(Settings sourceConfig)
{
CheckSettings(ref sourceConfig);
string jsonText = JSON_Serializer.SerializeObject(sourceConfig);

File.WriteAllText(ConfigFilePath, jsonText);
}

/// <summary> Reads the Settings of this Program. </summary>

public static Settings Read()
{
Settings targetConfig = new();

if(!File.Exists(ConfigFilePath) || Archive_Manager.FileIsEmpty(ConfigFilePath) )
{
Text.PrintWarning(Text.LocalizedData.WARNING_MISSING_SETTINGS_FILE);
Interface.GetDialog<ContinueDialog>().Popup();

Write(targetConfig);
}

else
{
string jsonText = File.ReadAllText(ConfigFilePath);
targetConfig = JSON_Serializer.DeserializeObject<Settings>(jsonText);

CheckSettings(ref targetConfig);
}

Load(targetConfig);
return targetConfig;
}

}

}