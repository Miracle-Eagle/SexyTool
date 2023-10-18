using System.Reflection;
using SexyTool.Program.Graphics.Dialogs;

namespace SexyTool.Program.Graphics.Menus
{
/// <summary> Allows the User to comit Changes to the ParamsGroup of this Application. </summary>

internal partial class ParamsEditor : ParamsMenu
{
/// <summary> Creates a new Instance of the ParamsEditor. </summary>

public ParamsEditor()
{
paramInstance = Program.userParams;
}

/** <summary> Displays the ParamsEditor. </summary>
<returns> Info related to Last Parameter selected by the User. </returns> */

public override object DynamicSelection()
{
PropertyInfo selectedParam = default;
bool exitMenu = false;

while(!exitMenu)
{
selectedParam = base.DynamicSelection() as PropertyInfo;

switch(selectedParam.Name)
{
case "FolderManagerParams":
Program.userParams.FolderManagerParams.EditParamsGroup();
break;

case "FileCompressorsParams":
Program.userParams.FileCompressorsParams.EditParamsGroup();
break;

case "FileHashersParams":
Program.userParams.FileHashersParams.EditParamsGroup();
break;

case "FileSecurityParams":
Program.userParams.FileSecurityParams.EditParamsGroup();
break;

case "SexyCryptorsParams":
Program.userParams.SexyCryptorsParams.EditParamsGroup();
break;

default:
Program.userParams.FileManagerParams.EditParamsGroup();
break;
}

exitMenu = (bool)Interface.GetDialog<ReturnDialog>().Popup();
}

return selectedParam;
}

}

}