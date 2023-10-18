using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>FileManager</c> of this Program. </summary>

internal class FileManager : Framework
{
/// <summary> Creates a new Instance of the <c>FileManager</c>. </summary>

public FileManager()
{
ID = 1;
DisplayName = Text.LocalizedData.FRAMEWORK_FILE_MANAGER;

FunctionsList = new()
{
// Function A - Display File Properties

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_DISPLAY_FILE_PROPERTIES,
Process = () => Archive_Manager.DisplayFileProperties(Program.userParams.FileManagerParams.InputPath)
},

// Function B - Create Direct Access

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_CREATE_DIRECT_ACCESS,
Process = () => Archive_Manager.CreateDirectAccess(Program.userParams.FileManagerParams.InputPath, Program.userParams.FileManagerParams.OutputPath)
},

// Function C - Create File

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_CREATE_FILE,
Process = () => Archive_Manager.CreateFile(Program.userParams.FileManagerParams.InputPath)
},

// Function D - Rename File

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_RENAME_FILE,
Process = () => Archive_Manager.RenameFile(Program.userParams.FileManagerParams.InputPath, Program.userParams.FileManagerParams.NewName)
},

// Function E - Copy File

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_COPY_FILE,
Process = () => Archive_Manager.CopyFile(Program.userParams.FileManagerParams.InputPath, Program.userParams.FileManagerParams.OutputPath, Program.userParams.FileManagerParams.ReplaceExistingFiles)
},

// Function F - Move File

new Function()
{
ID = ConsoleKey.F,
DisplayName = Text.LocalizedData.FUNCTION_MOVE_FILE,
Process = () => Archive_Manager.MoveFile(Program.userParams.FileManagerParams.InputPath, Program.userParams.FileManagerParams.OutputPath, Program.userParams.FileManagerParams.ReplaceExistingFiles)
},

// Function G - Delete File

new Function()
{
ID = ConsoleKey.G,
DisplayName = Text.LocalizedData.FUNCTION_DELETE_FILE,
Process = () => Archive_Manager.DeleteFile(Program.userParams.FileManagerParams.InputPath)
}

};

}

}

}