using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>FolderManager</c> of this Program. </summary>

internal class FolderManager : Framework
{
/// <summary> Creates a new Instance of the <c>FolderManager</c>. </summary>

public FolderManager()
{
ID = 2;
DisplayName = Text.LocalizedData.FRAMEWORK_FOLDER_MANAGER;

FunctionsList = new()
{
// Function A - Display Folder Properties

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_DISPLAY_FOLDER_PROPERTIES,
Process = () => Directory_Manager.DisplayFolderProperties(Program.userParams.FolderManagerParams.InputPath)
},

// Function B - Create Direct Access

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_CREATE_DIRECT_ACCESS,
Process = () => Directory_Manager.CreateDirectAccess(Program.userParams.FolderManagerParams.InputPath, Program.userParams.FolderManagerParams.OutputPath)
},

// Function C - Create Folder

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_CREATE_FOLDER,
Process = () => Directory_Manager.CreateFolder(Program.userParams.FolderManagerParams.InputPath)
},

// Function D - Rename Folder

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_RENAME_FOLDER,
Process = () => Directory_Manager.RenameFolder(Program.userParams.FolderManagerParams.InputPath, Program.userParams.FolderManagerParams.NewName)
},

// Function E - Copy Folder

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_COPY_FOLDER,
Process = () => Directory_Manager.CopyFolder(Program.userParams.FolderManagerParams.InputPath, Program.userParams.FolderManagerParams.OutputPath)
},

// Function F - Move Folder

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_MOVE_FOLDER,
Process = () => Directory_Manager.MoveFolder(Program.userParams.FolderManagerParams.InputPath, Program.userParams.FolderManagerParams.OutputPath)
},

// Function G - Delete Folder

new Function()
{
ID = ConsoleKey.G,
DisplayName = Text.LocalizedData.FUNCTION_DELETE_FOLDER,
Process = () => Directory_Manager.DeleteFolder(Program.userParams.FolderManagerParams.InputPath)
}

};

}

}

}