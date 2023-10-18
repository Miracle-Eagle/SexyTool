using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Several Functions for Archives (such as File Opening, Writing, Closing and Others). </summary> 

internal static class Archive_Manager
{
/** <summary> Changes a Path to a new one. </summary>

<param name = "oldPath"> The Path to be Changed. </param>
<param name = "newPath"> The New Path of the File. </param>

<returns> The Path Changed. </returns> */

public static string ChangePath(string oldPath, string newPath)
{
string absolutePath;

if(newPath == oldPath)
{
string rootPath = Path.GetDirectoryName(newPath);
char namePrefix = Input_Manager.GenerateStringComplement();

string fileName = Path.GetFileNameWithoutExtension(newPath);
string fileExtension = Path.GetExtension(newPath);

absolutePath = rootPath + Path.DirectorySeparatorChar + namePrefix + fileName + fileExtension;
}

else
absolutePath = newPath;

return absolutePath;
}

/** <summary> Checks if a Path is already been Used by an Existing File. </summary>

<param name = "targetPath"> The Path to be Analized. </param>

<returns> The Path Validated. </returns> */

public static void CheckDuplicatedPath(ref string targetPath)
{

if(!File.Exists(targetPath) )
return;

else
{
string duplicatedNameSuffix = Text.LocalizedData.DUPLICATED_NAME_SUFFIX;
string rootPath = Path.GetDirectoryName(targetPath);

string fileName = Path.GetFileNameWithoutExtension(targetPath);

if(fileName.EndsWith(duplicatedNameSuffix) )
{
int nameLengthDiff = fileName.Length - duplicatedNameSuffix.Length;
fileName.Remove(0, nameLengthDiff);
}

string fileExtension = Path.GetExtension(targetPath);
targetPath = rootPath + Path.DirectorySeparatorChar + fileName + fileExtension + duplicatedNameSuffix;

int copyIndex = 2;

while(File.Exists(targetPath) )
{
string displayCopyNumber = string.Format("{0} ({1})", duplicatedNameSuffix, copyIndex);
targetPath = rootPath + Path.DirectorySeparatorChar + fileName + fileExtension + displayCopyNumber;

copyIndex++;
}

}

}

/** <summary> Copies an Archive to a New Location. </summary>

<param name = "sourcePath"> The Path where the Archive to be Copied is Located. </param>
<param name = "destPath"> The Location where the File will be Copied. </param>
<param name = "replaceFile"> A Boolean that Determines if Files should be Replaced or not when Copying they Content. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CopyFile(string sourcePath, string destPath, bool replaceFile)
{

try
{
string sourceFileName = Path.GetFileName(sourcePath);
string destFolderName = Directory_Manager.GetFolderName(destPath);

Text.PrintLine(false, Text.LocalizedData.ACTION_COPY_FILE, sourceFileName, destFolderName);
Directory_Manager.CheckMissingFolder(destPath);

string targetPath = Path.Combine(destPath, sourceFileName);

if(!replaceFile)
CheckDuplicatedPath(ref targetPath);

File.Copy(sourcePath, targetPath, replaceFile);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_COPY_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_COPY_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Creates a Link that Serves as a Direct Access to a Specific Archive. </summary>

<param name = "sourcePath"> The Path of the File where the Direct Access will be Created from. </param>
<param name = "targetPath"> The Path where the Direct Access will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateDirectAccess(string sourcePath, string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_CREATE_DIRECT_ACCESS, Path.GetFileName(sourcePath), Path.GetFileName(targetPath) );
Path_Helper.AddExtension(ref targetPath, ".lnk");

File.CreateSymbolicLink(targetPath, sourcePath);
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

/** <summary> Creates a new Archive on the specific Location. </summary>

<param name = "targetPath"> The Path where the File will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateFile(string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_CREATE_FILE, Path.GetFileName(targetPath) );
File.Create(targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_CREATE_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_CREATE_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}


/** <summary> Deletes an Archive from the Current Device. </summary>

<param name = "targetPath"> The Path where the File to be Deleted is Located. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DeleteFile(string targetPath)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_DELETE_FILE, Path.GetFileName(targetPath) );
File.Delete(targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_DELETE_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_DELETE_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Displays all the Properties of an Archive. </summary>
<param name = "targetPath"> The Path where the File to be Analized is Located. </param> */

public static void DisplayFileProperties(string targetPath)
{
Text.PrintHeader(Text.LocalizedData.HEADER_FILE_PROPERTIES);
Text.PrintJson(GetFileProperties(targetPath) );
}

/** <summary> Checks if a File is Empty or not by Analizing its Content. </summary>

<param name = "targetPath"> The Path where the Archive to be Checked is Located. </param>

<returns> <b>true</b> if the File is Empty; otherwise, returns <b>false</b>. </returns> */

public static bool FileIsEmpty(string targetPath)
{

if(GetFileSize(targetPath) == 0)
return true;

else
return false;

}

/** <summary> Gets the Properties of a File. </summary>

<param name = "targetPath"> The Path to the File where the Properties will be Obtained from. </param>

<returns> The Properties of the Archive. </returns> */

private static JObject GetFileProperties(string targetPath)
{
JObject fileProperties = new();
JProperty fileName = new(Text.LocalizedData.FILE_PROPERTY_NAME, Path.GetFileNameWithoutExtension(targetPath) );

fileProperties.Add(fileName);
JProperty rootPath = new(Text.LocalizedData.FILE_PROPERTY_ROOT_PATH, Path.GetDirectoryName(targetPath) );

fileProperties.Add(rootPath);
JProperty filePath = new(Text.LocalizedData.FILE_PROPERTY_PATH, Path.GetFullPath(targetPath) );

fileProperties.Add(filePath);
JProperty fileExtension = new(Text.LocalizedData.FILE_PROPERTY_EXTENSION, Path.GetExtension(targetPath) );

fileProperties.Add(fileExtension);
JProperty fileAttributes = new(Text.LocalizedData.FILE_PROPERTY_ATTRIBUTES, File.GetAttributes(targetPath) );

fileProperties.Add(fileAttributes);
JProperty fileSize = new(Text.LocalizedData.FILE_PROPERTY_SIZE, Input_Manager.GetDisplaySize(GetFileSize(targetPath) ) );

fileProperties.Add(fileSize);
JProperty fileCreationTime = new(Text.LocalizedData.FILE_PROPERTY_CREATION_TIME, File.GetCreationTime(targetPath) );

fileProperties.Add(fileCreationTime);
JProperty lastFileAccess = new(Text.LocalizedData.FILE_PROPERTY_LAST_ACCESS, File.GetLastAccessTime(targetPath) );

fileProperties.Add(lastFileAccess);
JProperty lastFileModification = new(Text.LocalizedData.FILE_PROPERTY_LAST_MODIFICATION, File.GetLastWriteTime(targetPath) );

fileProperties.Add(lastFileModification);
return fileProperties;
}

/** <summary> Gets the Size in Bytes of an Archive. </summary>

<param name = "targetPath"> The Path to the File where the Properties will be Obtained from. </param>

<returns> The Size of the File. </returns> */

public static long GetFileSize(string targetPath)
{
FileInfo targetFileInfo = new(targetPath);
return targetFileInfo.Length;
}

/** <summary> Moves an Archive to a New Location. </summary>

<param name = "sourcePath"> The Path where the Archive to be Moved is Located. </param>
<param name = "destPath"> The Location where the Archive will be Moved (this must be a Directory). </param>
<param name = "replaceFile"> A Boolean that Determines if Files should be Replaced or not when Moving they Content. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void MoveFile(string sourcePath, string destPath, bool replaceFile)
{

try
{
string sourceFileName = Path.GetFileName(sourcePath);
Text.PrintLine(false, Text.LocalizedData.ACTION_MOVE_FILE, sourceFileName, Directory_Manager.GetFolderName(destPath) );

Directory_Manager.CheckMissingFolder(destPath);
string targetPath = Path.Combine(destPath, sourceFileName);

if(!replaceFile)
CheckDuplicatedPath(ref targetPath);

File.Move(sourcePath, targetPath, replaceFile);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_MOVE_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_MOVE_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

/** <summary> Renames an Archive with the specific Name. </summary>

<param name = "sourcePath"> The Path where the Archive to be Renamed is Located. </param>
<param name = "newName"> The new Name of the File. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void RenameFile(string sourcePath, string newName)
{

try
{
Text.PrintLine(false, Text.LocalizedData.ACTION_RENAME_FILE, Path.GetFileName(sourcePath), newName);
string rootPath = Path.GetDirectoryName(sourcePath);

string targetPath = Path.Combine(rootPath, newName);
CheckDuplicatedPath(ref targetPath);

File.Move(sourcePath, targetPath);
}

catch(Exception error)
{
Text.PrintErrorMsg(Text.LocalizedData.RESULT_RENAME_FILE_FAILED);
Exceptions_Handler.SetErrorCaught(error);
}

finally
{

if(Exceptions_Handler.errorCaught == null)
Text.PrintSuccessMsg(Text.LocalizedData.RESULT_RENAME_FILE_SUCCESSFUL);

else
Exceptions_Handler.DisplayErrorInfo();

}

}

}

}