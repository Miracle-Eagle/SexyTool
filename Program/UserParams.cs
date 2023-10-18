using SexyTool.Program.Arguments.ParamGroups;
using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using System;
using System.IO;

namespace SexyTool.Program
{
/// <summary> Stores groups of Params which the User can Set in order to Perform a specific Task. </summary>

[Serializable]

public class UserParams
{
/** <summary> Gets or Sets some Params for the <c>FileManager</c> Tasks. </summary>
<returns> The <c>FileManager</c> Params. </returns> */

public FileManagerArgs FileManagerParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>FolderManager</c> Tasks. </summary>
<returns> The <c>FolderManager</c> Params. </returns> */

public FolderManagerArgs FolderManagerParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>FileCompressors</c> Tasks. </summary>
<returns> The <c>FileCompressors</c> Params. </returns> */

public FileCompressorsArgs FileCompressorsParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>FileHashers</c> Tasks. </summary>
<returns> The <c>FileHashers</c> Params. </returns> */

public FileHarshersArgs FileHashersParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>FileSecurity</c> Tasks. </summary>
<returns> The <c>FileSecurity</c> Params. </returns> */

public FileSecurityArgs FileSecurityParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>SexyParsers</c> Tasks. </summary>
<returns> The <c>SexyParsers</c> Params. </returns> */

public SexyParsersArgs SexyParsersParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>SexyCompressors</c> Tasks. </summary>
<returns> The <c>SexyCompressors</c> Params. </returns> */

public SexyCompressorsArgs SexyCompressorsParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>SexyCryptors</c> Tasks. </summary>
<returns> The <c>SexyCryptors</c> Params. </returns> */

public SexyCryptorsArgs SexyCryptorsParams{ get; set; }

/** <summary> Gets or Sets some Params for the <c>SexyUtils</c> Tasks. </summary>
<returns> The <c>SexyUtils</c> Params. </returns> */

public SexyUtilsArgs SexyUtilsParams{ get; set; }

// Add more args here...

/** <summary> Gets the Path of the Params File. </summary>
<returns> The Path of the Params File. </returns> */

protected static string ParamsFilePath
{

get
{
Type classType = typeof(UserParams);
return Environment_Info.CurrentAppDirectory + Path.DirectorySeparatorChar + classType.Name + ".json";
}

}

/// <summary> Creates a new Instance of the <c>UserParams</c> Class. </summary>

public UserParams()
{
FileManagerParams = new FileManagerArgs();
FolderManagerParams = new FolderManagerArgs();

FileCompressorsParams = new FileCompressorsArgs();
FileHashersParams = new FileHarshersArgs();

FileSecurityParams = new FileSecurityArgs();
SexyParsersParams = new SexyParsersArgs();

SexyCompressorsParams  = new SexyCompressorsArgs();
SexyCryptorsParams = new SexyCryptorsArgs();

SexyUtilsParams = new SexyUtilsArgs();
}

/** <summary> Checks each nullable Field of the <c>UserParams</c> instance given and Validates them. </summary>
<param name = "targetParams"> The params to be Analized. </param> */

private static void CheckUserParams(ref UserParams targetParams)
{
UserParams defaultParams = new();
targetParams.FileManagerParams ??= defaultParams.FileManagerParams;

targetParams.FolderManagerParams ??= defaultParams.FolderManagerParams;
targetParams.FileCompressorsParams ??= defaultParams.FileCompressorsParams;

targetParams.FileSecurityParams ??= defaultParams.FileSecurityParams;
targetParams.SexyParsersParams ??= defaultParams.SexyParsersParams;

targetParams.SexyCompressorsParams ??= defaultParams.SexyCompressorsParams;
targetParams.SexyCryptorsParams ??= defaultParams.SexyCryptorsParams;
}

/// <summary> Reads and Deserializes the Contents of the <c>UserParams</c> file. </summary>

public static UserParams Read()
{
UserParams targetParams = new();

if(!File.Exists(ParamsFilePath) || Archive_Manager.FileIsEmpty(ParamsFilePath) )
{
Text.PrintWarning(Text.LocalizedData.WARNING_MISSING_PARAMS_FILE);
Interface.GetDialog<ContinueDialog>().Popup();

Write(targetParams);
}

else
{
string jsonText = File.ReadAllText(ParamsFilePath);
targetParams = JSON_Serializer.DeserializeObject<UserParams>(jsonText);

CheckUserParams(ref targetParams);
}

return targetParams;
}

/// <summary> Writes and Serializes the Contents from the specified <c>UserParams</c> Instance to a File. </summary>

public static void Write(UserParams sourceParams)
{
CheckUserParams(ref sourceParams);
string jsonText = JSON_Serializer.SerializeObject(sourceParams);

File.WriteAllText(ParamsFilePath, jsonText);
}

}

}