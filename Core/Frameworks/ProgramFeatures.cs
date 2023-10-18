using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the Features of this Program. </summary>

internal class ProgramFeatures : Framework
{
/// <summary> Creates a new Instance of the <c>ProgramFeatures</c>. </summary>

public ProgramFeatures()
{
ID = 0;
DisplayName = Text.LocalizedData.FRAMEWORK_PROGRAM_FEATURES;

FunctionsList = new()
{
// Function A - Save Environment Info

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_GET_ENVIRONMENT_INFO,
Process = () => Features.SaveEnvironmentInfo(Program.userParams.FileManagerParams.OutputPath)
},

// Function B - Display User Info

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_GET_USER_INFO,
Process = Features.DisplayUserInfo
},

// Function C - Display Program Info

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_GET_PROGRAM_INFO,
Process = Features.DisplayProgramInfo
},

// Function D - Display App Settings

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_DISPLAY_APP_SETTINGS,
Process = Features.DisplayAppSettings
},

// Function E - Edit Params Group

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_EDIT_PARAMS_GROUP,
Process = Features.EditParamsGroup
}

};

}

}

}