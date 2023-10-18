using Newtonsoft.Json.Linq;
using SexyTool.Program.Core.Functions.Other;
using System;
using System.IO;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Several Functions for Directories (such as Folder Copying, Moving, Deleting and Others). </summary>

internal static class Directory_Manager
{
/** <summary> Changes a Path to a new one. </summary>

<param name = "oldPath"> The Path to be Changed. </param>
<param name = "newPath"> The New Path of the Folder. </param>

<returns> The Path Changed. </returns> */

public static string ChangePath(string oldPath, string newPath)
{
string absolutePath;

if(newPath == oldPath)
{
string rootPath = Path.GetDirectoryName(newPath);
char namePrefix = Input_Manager.GenerateStringComplement();

string folderName = GetFolderName(newPath);
absolutePath = rootPath + Path.DirectorySeparatorChar + namePrefix + folderName;
}

else
absolutePath = newPath;

return absolutePath;
}

/** <summary> Checks if a Path is already been used by an Existing Folder. </summary>

<param name = "targetPath"> The Path to be Analized. </param>

<returns> The Path Validated. </returns> */

public static void CheckDuplicatedPath(ref string targetPath)
{

if(!Directory.Exists(targetPath) )
return;

else
{
string duplicatedNameSuffix = Text.LocalizedData.DUPLICATED_NAME_SUFFIX;
string rootPath = Path.GetDirectoryName(targetPath);

string folderName = GetFolderName(targetPath);

if(folderName.EndsWith(duplicatedNameSuffix) )
{
int nameLengthDiff = folderName.Length - duplicatedNameSuffix.Length;
folderName.Remove(0, nameLengthDiff);
}

targetPath = rootPath + Path.DirectorySeparatorChar + folderName + duplicatedNameSuffix;
int copyIndex = 2; 

while(Directory.Exists(targetPath) )
{
string displayCopyNumber = string.Format("{0} ({1})", duplicatedNameSuffix, copyIndex);
targetPath = rootPath + Path.DirectorySeparatorChar + folderName + displayCopyNumber;

copyIndex++;
}

}

}

/** <summary> Checks if a Directory is Missing on User's device or not. </summary>
<remarks> In case the Folder does not Exists, it will be Created in order to avoid Issues. </remarks>

<param name = "targetPath"> The Path where the Directory to be Analized is Located. </param>
<returns> Info related to the Directory that was Analized. </returns> */

public static DirectoryInfo CheckMissingFolder(string targetPath)
{
DirectoryInfo folderInfo;

if(!Directory.Exists(targetPath) )
folderInfo = Directory.CreateDirectory(targetPath);

else
folderInfo = new(targetPath);

return folderInfo;
}

/** <summary> Copies a Directory to a New Location. </summary>

<param name = "sourcePath"> The Path where the Directory to be Copied is Located. </param>
<param name = "destPath"> The Location where the Folder will be Copied (this must be a Root Directory). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CopyFolder(string sourcePath, string destPath)
{

try
{
string sourceFolderName = GetFolderName(sourcePath);
Text.PrintLine(true, Text.LocalizedData.ACTION_COPY_FOLDER, sourceFolderName, GetFolderName(destPath) );

string targetPath = Path.Combine(destPath, sourceFolderName);
bool overwriteFiles = Program.userParams.FileManagerParams.ReplaceExistingFiles;

ActionWrapper<string, string> copyAction = new( (srcFile, destFile) => Archive_Manager.CopyFile(srcFile, destFile, overwriteFiles) );
Task_Manager.BatchTask(sourcePath, targetPath, copyAction.Init);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_COPY_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_COPY_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Creates a Link that Serves as a Direct Access to a Specific Directory. </summary>

<param name = "sourcePath"> The Path of the Folder where the Direct Access will be Created from. </param>
<param name = "targetPath"> The Path where the Direct Access will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateDirectAccess(string sourcePath, string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_CREATE_DIRECT_ACCESS, GetFolderName(sourcePath), GetFolderName(targetPath) );
Path_Helper.AddExtension(ref targetPath, ".lnk");

Archive_Manager.CheckDuplicatedPath(ref targetPath);
Directory.CreateSymbolicLink(targetPath, sourcePath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_CREATE_DIRECT_ACCESS_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_CREATE_DIRECT_ACCESS_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Creates a New Directory on the specific Location. </summary>

<param name = "targetPath"> The Path where the Folder will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateFolder(string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_CREATE_FOLDER, GetFolderName(targetPath) );
Directory.CreateDirectory(targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_CREATE_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_CREATE_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Deletes the Directory specific. </summary>

<param name = "targetPath"> The Path where the Folder to be Deleted is Located. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DeleteFolder(string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DELETE_FOLDER, GetFolderName(targetPath) );
Directory.Delete(targetPath, FolderIsEmpty(targetPath) );
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DELETE_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DELETE_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Displays all the Properties of a Directory. </summary>
<param name = "targetPath"> The Path where the Folder to be Analized is Located. </param> */

public static void DisplayFolderProperties(string targetPath)
{
Text.PrintHeader(Text.LocalizedData.HEADER_FOLDER_PROPERTIES);
Text.PrintJson(GetFolderProperties(targetPath) );
}

/** <summary> Gets the Path of a Container. </summary>

<param name = "targetFilePath"> The Location of the File to add in the Container. </param>
<param name = "namePrefix"> The Prefix to Add to the Beginning of the Name (Optional). </param> */

public static string GetContainerPath(string targetFilePath, string namePrefix = "FilesContainer")
{
string rootPath = Path.GetDirectoryName(targetFilePath);
string targetFileName = Path.GetFileNameWithoutExtension(targetFilePath);

double fileCreationTimeStamp = TimeCalculator.CalculateTimeStamp(File.GetCreationTimeUtc(targetFilePath) );
string creationTimeSuffix = string.Format("@({0})", fileCreationTimeStamp);

string containerPath = rootPath + Path.DirectorySeparatorChar + namePrefix + Path.DirectorySeparatorChar + targetFileName  + creationTimeSuffix;
CheckMissingFolder(containerPath);

return containerPath;
}

/** <summary> Gets the Name of a Directory.  </summary>

<param name = "targetPath"> The Path where the Folder to be Analized is Located. </param>

<returns> The Name of the Folder. </returns> */

public static string GetFolderName(string targetPath)
{
DirectoryInfo targetFolderInfo = new(targetPath);
return targetFolderInfo.Name;
}

/** <summary> Gets the Size of a Sub-Folder expressed in Bytes. </summary>

<param name = "targetPath"> The Path where the Sub-Folder to be Analized is Located. </param>

<returns> The Size of the Sub-Folder. </returns> */

private static long GetSubfolderSize(string targetPath)
{
string[] subFolder_filesList = Directory.GetFiles(targetPath, "*.*", SearchOption.TopDirectoryOnly);
long subfolderSize = 0;

foreach(string filePath in subFolder_filesList)
{
long fileSize = Archive_Manager.GetFileSize(filePath);
subfolderSize += fileSize;
}

return subfolderSize;
}

/** <summary> Gets the Size of a Directory expressed in Bytes. </summary>

<param name = "targetPath"> The Path where the Directory to be Analized is Located. </param>

<returns> The Size of the Folder. </returns> */

public static long GetFolderSize(string targetPath)
{
string[] filesList = Directory.GetFiles(targetPath, "*.*", SearchOption.TopDirectoryOnly);
long folderSize = 0;

foreach(string filePath in filesList)
{
long fileSize = Archive_Manager.GetFileSize(filePath);
folderSize += fileSize;
}

string[] subfoldersList = Directory.GetDirectories(targetPath);

foreach(string subfolderPath in subfoldersList)
{
long subfolderSize = GetSubfolderSize(subfolderPath);
folderSize += subfolderSize;
}

return folderSize;
}

/** <summary> Gets the Properties of a Directory. </summary>

<param name = "targetPath"> The Path where the Directory to be Analized is Located. </param>

<returns> The Properties of the Folder. </returns> */

private static JObject GetFolderProperties(string targetPath)
{
JObject folderProperties = new();
JProperty folderName = new(Text.LocalizedData.FOLDER_PROPERTY_NAME, GetFolderName(targetPath) );

folderProperties.Add(folderName);
JProperty rootPath = new(Text.LocalizedData.FOLDER_PROPERTY_ROOT_PATH, Path.GetDirectoryName(targetPath) );

folderProperties.Add(rootPath);
JProperty folderPath = new(Text.LocalizedData.FOLDER_PROPERTY_PATH, Path.GetFullPath(targetPath) );

folderProperties.Add(folderPath);
JProperty filesCount = new(Text.LocalizedData.FOLDER_PROPERTY_FILES_COUNT, Directory.GetFiles(targetPath).Length);

folderProperties.Add(filesCount);
JProperty subfoldersCount = new(Text.LocalizedData.FOLDER_PROPERTY_SUBFOLDERS_COUNT, Directory.GetDirectories(targetPath).Length);

folderProperties.Add(subfoldersCount);
JProperty folderSize = new(Text.LocalizedData.FOLDER_PROPERTY_SIZE, Input_Manager.GetDisplaySize(GetFolderSize(targetPath) ) );

folderProperties.Add(folderSize);
JProperty folderCreationTime = new(Text.LocalizedData.FOLDER_PROPERTY_CREATION_TIME, Directory.GetCreationTime(targetPath) );

folderProperties.Add(folderCreationTime);
JProperty lastFolderAccess = new(Text.LocalizedData.FOLDER_PROPERTY_LAST_ACCESS, Directory.GetLastAccessTime(targetPath) );

folderProperties.Add(lastFolderAccess);
JProperty lastFolderModification = new(Text.LocalizedData.FOLDER_PROPERTY_LAST_MODIFICATION, Directory.GetLastWriteTime(targetPath) );

folderProperties.Add(lastFolderModification);
return folderProperties;
}

/** <summary> Checks if a Folder is Empty or not by Analizing its Content. </summary>

<param name = "targetPath"> The Path where the Directory to be Checked is Located. </param>

<returns> <b>true</b> if the Folder is Empty; otherwise, <b>false</b>. </returns> */

public static bool FolderIsEmpty(string targetPath)
{
string[] filesList = Directory.GetFiles(targetPath);

if(filesList == null || filesList.Length == 0)
return true;

else
return false;

}

/** <summary> Moves a Directory to a New Location. </summary>

<param name = "sourcePath"> The Path where the Directory to be Moved is Located. </param>
<param name = "destPath"> The Location where the Directory will be Moved (this must be a Root Directory Path). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void MoveFolder(string sourcePath, string destPath)
{

try
{
string sourceFolderName = GetFolderName(sourcePath);
Text.PrintLine(false, Text.LocalizedData.ACTION_MOVE_FOLDER, sourceFolderName, GetFolderName(destPath) );

string targetPath = Path.Combine(destPath, sourceFolderName);
CheckDuplicatedPath(ref targetPath);

Directory.Move(sourcePath, targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_MOVE_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_MOVE_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Rename a Directory with the specific Name. </summary>

<param name = "sourcePath"> The Path where the Directory to be Renamed is Located. </param>
<param name = "newName"> The New Name of the Folder. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void RenameFolder(string sourcePath, string newName)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_RENAME_FOLDER, GetFolderName(sourcePath), newName);
string rootPath = Path.GetDirectoryName(sourcePath);

string targetPath = Path.Combine(rootPath, newName);
CheckDuplicatedPath(ref targetPath);

Directory.Move(sourcePath, targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_RENAME_FOLDER_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_RENAME_FOLDER_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

}

}