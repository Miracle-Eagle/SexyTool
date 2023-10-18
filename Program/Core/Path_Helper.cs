using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SexyTool.Program.Core
{
/// <summary> Initializes some Functions for Building or Editing access Paths. </summary>

internal static class Path_Helper
{
/** <summary> Sets a Value which Contains Info about the specific File Names. </summary>
<returns> The specific File Names. </returns> */

public static Dictionary<string, List<string>> specificFileNames = new()
{

{
"RtObject",
new List<string>() { "drapper_", "local_profiles", "loot", "_saveheader_rton" }
}

};

/** <summary> Sets a Value which Contains Info about the specific File Extensions. </summary>
<returns> The specific File Extensions. </returns> */

public static Dictionary<string, List<string>> specificFileExtensions = new()
{

{
"ResBundle",
new List<string>() { ".1bsr", ".rsb", ".rsb1", ".obb" }
},

{
"ResGroup",
new List<string>() { ".pgsr", ".rsg", ".rsgp" }
},

{
"RtObject",
new List<string>() { ".bak", ".bin", ".dat", ".rton", ".section", ".txt" }
}

};

/** <summary> Adds an Extension to the End of a Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Added. </param>

<returns> A Path with the new Extension. </returns> */

public static void AddExtension(ref string sourcePath, string targetExtension)
{

if(Path.GetExtension(sourcePath) == targetExtension)
return;

else
sourcePath += targetExtension;

}

/** <summary> Changes the Extension from a given Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Changed. </param>

<returns> A Path with the new Extension. </returns> */

public static void ChangeExtension(ref string sourcePath, string targetExtension)
{

if(Path.GetExtension(sourcePath) == targetExtension)
return;

else
sourcePath = Path.ChangeExtension(sourcePath, targetExtension);

}

/** <summary> Checks if the provided Path refers to an Existing File or Folder. </summary>
<param name = "sourcePath"> The Path to be Analized. </param> */

private static void CheckDuplicatedPath(string targetPath)
{
FileAttributes pathType = CheckPathType(targetPath);

if(pathType == FileAttributes.Directory)
Directory_Manager.CheckDuplicatedPath(ref targetPath);

else
Archive_Manager.CheckDuplicatedPath(ref targetPath);

}

/** <summary> Checks if the Path provided refers to an Existing FileSystem or not. </summary>

<param name = "sourcePath"> The Path to be Analized. </param>
<param name = "createFileSystem"> A boolean that Determines if a FileSystem should be Created in case the Path does not exists. </param> */

public static void CheckExistingPath(string sourcePath, bool createFileSystem)
{

if(!Path.Exists(sourcePath) )
{

if(createFileSystem)
CreateFileSystem(sourcePath);

}

else
CheckDuplicatedPath(sourcePath);

}

/** <summary> Checks if a Path is Actually an Existing File or a Folder. </summary>

<param name = "sourcePath"> The Path to be Analized. </param>

<returns> The Attributes that Specificates the Path Type. </returns> */

public static FileAttributes CheckPathType(string sourcePath)
{

if(!Path.Exists(sourcePath) )
CreateFileSystem(sourcePath);

FileAttributes pathAttributes = File.GetAttributes(sourcePath);
FileAttributes pathType;

if(pathAttributes.HasFlag(FileAttributes.Directory) )
pathType = FileAttributes.Directory;

else
pathType = FileAttributes.Archive;

return pathType;
}

/** <summary> Creates a FileSystem (a File or a Folder) according to the given Path Type. </summary>
<param name = "targetPath"> The Path where the FileSystem will be Created. </param> */

public static void CreateFileSystem(string targetPath)
{

if(Path.HasExtension(targetPath) )
{
using FileStream inputFile = File.Create(targetPath);
inputFile.Write(Console.InputEncoding.GetBytes(Text.LocalizedData.FILESYSTEM_INDICATOR_INPUT) );
}

else
_ = Directory.CreateDirectory(targetPath);

}

/** <summary> Creates a Extensions Filter from a Specific Extensions List. </summary>

<param name = "specificExtensions"> The Extensions used for Creating the Filter. </param>
<param name = "useUppercase"> A boolean that Determines if extensions should be Filtered in UpperCase or not. </param>

<returns> The Extensions Filter. </returns> */

private static Func<string, bool> CreateExtensionsFilter(List<string> specificExtensions, bool useUpperCase)
{

return extensionsFilter => 
{

for(int i = 0; i < specificExtensions.Count; i++)
{
string fileExtension = useUpperCase ? specificExtensions[i].ToUpper() : specificExtensions[i].ToLower();

if(extensionsFilter.EndsWith(fileExtension) )
return true;

}

return false;
};

}

/** <summary> Creates a Filter from a Specific Names List. </summary>

<param name = "specificNames"> The Names used for Creating the Filter. </param>

<returns> The Names Filter. </returns> */

private static Func<string, bool> CreateNamesFilter(List<string> specificNames)
{

return namesFilter => 
{

for(int i = 0; i < specificNames.Count; i++)
{

if(namesFilter.StartsWith(specificNames[i] ) )
return true;

}

return false;
};

}

/** <summary> Filters a List of Files by a Specific Name and a Specific Extension wich can be in Lowercase or in Uppercase. </summary>

<param name = "sourceList"> The Files List to be Filtered. </param>
<param name = "specificNames"> A List of Specific Names used for Filtering the Files. </param>
<param name = "specificExtensions"> A List of Specific Extensions used for Filtering the Files. </param>

<returns> The Filtered Files List. </returns> */

public static void FilterFilesList(ref string[] sourceList, List<string> specificNames, List<string> specificExtensions)
{
#region ====== Filter Criteria ======

var namesFilter = CreateNamesFilter(specificNames);
var lowerExtensionsFilter = CreateExtensionsFilter(specificExtensions, false);
var upperExtensionsFilter = CreateExtensionsFilter(specificExtensions, true);

#endregion

if(sourceList == null || sourceList.Length == 0)
throw new ArgumentException("Files List can't be Null or Empty.");

string[] specificFilesList;
string[] lowerFilesList;
string[] upperFilesList;

if(specificNames == null || specificExtensions == null)
throw new ArgumentNullException("Filter Criteria can't be Null.");

else if(specificNames.Count == 0 && specificExtensions.Count == 0)
{
Text.PrintWarning("No params Found in the List for the Filter Criteria.");
return;
}

else if(specificNames.Count == 0)
{
lowerFilesList = sourceList.Where(lowerExtensionsFilter).ToArray();
upperFilesList = sourceList.Where(upperExtensionsFilter).ToArray();

sourceList = Input_Manager.MergeArrays(lowerFilesList, upperFilesList);
}

else if (specificExtensions.Count == 0)
{
specificFilesList = sourceList.Where(namesFilter).ToArray();
sourceList = specificFilesList;
}

else
{
specificFilesList = sourceList.Where(namesFilter).ToArray();
lowerFilesList = sourceList.Where(lowerExtensionsFilter).ToArray();

upperFilesList = sourceList.Where(upperExtensionsFilter).ToArray();
sourceList = Input_Manager.MergeArrays(specificFilesList, lowerFilesList, upperFilesList);
}

}

/** <summary> Filters a Path from User's Input. </summary>

<param name = "targetPath"> The Path to be Filtered. </param>

<returns> The Filtered Path. </returns> */

public static string FilterPath(string targetPath)
{
string validStr = Input_Manager.CheckEmptyString(targetPath);
string filteredPath = string.Empty;

char[] invalidPathChars = Input_Manager.GetInvalidChars(false);

for(int i = 0; i < invalidPathChars.Length; i++)
{

if(validStr.Contains(invalidPathChars[i] ) )
{
filteredPath = validStr.Replace(invalidPathChars[i].ToString(), string.Empty);
validStr = filteredPath;
}

filteredPath = validStr;
}

return filteredPath.Replace("\"", string.Empty);
}

/** <summary> Removes the Extension from a given Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Removed. </param>

<returns> A Path with the Extension removed. </returns> */

public static void RemoveExtension(ref string sourcePath, string targetExtension)
{

if(Path.GetExtension(sourcePath) != targetExtension)
return;

else
{
int lengthDiff = sourcePath.Length - targetExtension.Length;
sourcePath = sourcePath[..lengthDiff];
}

}

}

}