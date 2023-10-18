using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;
using System.Reflection;

namespace SexyTool.Program.Graphics.Menus
{
/// <summary> Allows the User to comit Changes to the Settings of this Application. </summary>

internal partial class AppSettings : SettingsMenu
{
/// <summary> Creates a new Instance of the AppSettings. </summary>

public AppSettings()
{
headerText = Text.LocalizedData.HEADER_APP_SETTINGS;
adviceText = Text.LocalizedData.ADVICE_SELECT_SETTING;
}

/** <summary> Displays the AppSettings. </summary>
<returns> Info related to the Last Config selected by the User. </returns> */

public override object DynamicSelection()
{
PropertyInfo selectedConfig = default;
bool exitMenu = false;

while(!exitMenu)
{
selectedConfig = base.DynamicSelection() as PropertyInfo;

switch(selectedConfig.Name)
{
case "ForegroundColor":
Program.config.ForegroundColor = (ConsoleColor)Interface.GetUserSelection<ForegroundColorSelection>().GetSelectionParam();
break;

case "ScreenSize":
Program.config.ScreenSize = Interface.GetUserSelection<ScreenSizeSelection>().GetSelectionParam() as int[];
break;

case "CursorSize":
Program.config.CursorSize = (int)Interface.GetUserSelection<CursorSizeSelection>().GetSelectionParam();
break;

case "CursorVisualization":
Program.config.CursorVisualization = (bool)Interface.GetUserSelection<CursorViewSelection>().GetSelectionParam();
break;

case "UserLanguage":
Program.config.UserLanguage = (Language)Interface.GetUserSelection<LanguageSelection>().GetSelectionParam();
break;

default:
Program.config.BackgroundColor = (ConsoleColor)Interface.GetUserSelection<BackgroundColorSelection>().GetSelectionParam();
break;
}

exitMenu = (bool)Interface.GetDialog<ReturnDialog>().Popup();
}

return selectedConfig;
}

}

}