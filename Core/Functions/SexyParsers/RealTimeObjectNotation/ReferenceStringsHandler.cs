using System.IO;

namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation
{
/// <summary> Handles the ReferenceStrings Cached on Parsing Tasks. </summary>

public static class ReferenceStringsHandler
{
/** <summary> Sets a Value which Contains Info about the Reference Strings contained on Parsed Files. </summary>
<returns> The Reference Strings. </returns> */

public static ReferenceStrings refStringsContainer = new();

/** <summary> Adds a String to the Native Strings List. </summary> 
<param name = "targetStr"> The String to be Added. </param> */

public static void AddStringToNativeList(string targetStr) => refStringsContainer.AddNativeString(targetStr);

/** <summary> Adds a String to the Unicode Strings List. </summary> 
<param name = "targetStr"> The String to be Added. </param> */

public static void AddStringToUnicodeList(string targetStr) => refStringsContainer.AddUnicodeString(targetStr);

/// <summary> Removes all the Strings stored in the Container. </summary>

public static void ClearStrings()
{
refStringsContainer.ClearNativeStrings();
refStringsContainer.ClearUnicodeStrings();
}

/** <summary> Gets the Index of a String found in the Native Strings List. </summary>

<param name = "targetStr"> The String to Locate in the List. </param>

<returns> The Index of the String. </returns> */

public static int GetNativeStringIndex(string targetStr) => refStringsContainer.GetIndexOfNativeString(targetStr);

/** <summary> Gets the Index of a String found in the Unicode Strings List. </summary>

<param name = "targetStr"> The String to Locate in the List. </param>

<returns> The Index of the String. </returns> */

public static int GetUnicodeStringIndex(string targetStr) => refStringsContainer.GetIndexOfUnicodeString(targetStr);

/** <summary> Gets a String from the Native Strings List by using a Index. </summary>

<param name = "stringIndex"> The Index of the String to Obtain. </param>

<returns> The Native String Obtained. </returns> */

public static string GetStringFromNativeList(int stringIndex) => refStringsContainer.GetNativeString(stringIndex);

/** <summary> Gets a String from the Unicode Strings List by using a Index. </summary>

<param name = "stringIndex"> The Index of the String to Obtain. </param>

<returns> The Unicode String Obtained. </returns> */

public static string GetStringFromUnicodeList(int stringIndex) => refStringsContainer.GetUnicodeString(stringIndex);

/** <summary> Checks if a String is Contained in the Native Strings List. </summary> 

<param name = "targetStr"> The String to be Checked. </param>

<returns> <b>true</b> if the String exists in the List; otherwise, <b>false</b> </returns> */

public static bool ListHasNativeString(string targetStr) => refStringsContainer.HasNativeString(targetStr);

/** <summary> Checks if a String is Contained in the Unicode Strings List. </summary> 

<param name = "targetStr"> The String to be Checked. </param>

<returns> <b>true</b> if the String exists in the List; otherwise, <b>false</b> </returns> */

public static bool ListHasUnicodeString(string targetStr) => refStringsContainer.HasUnicodeString(targetStr);

/** <summary> Gets the Path of a Reference Strings File. </summary>

<param name = "targetFilePath"> The Location of the File to add in the Container. </param>
<param name = "namePrefix"> The Prefix to Add to the Beginning of the Name (Optional). </param>

<returns> The Path to the Reference Strings File. </returns> */

private static string GetRefFilePath(string targetFilePath)
{
string refContainerPath = Directory_Manager.GetContainerPath(targetFilePath, "ReferenceStrings");
int refStringsCount = refStringsContainer.GetNativeStringsCount() + refStringsContainer.GetUnicodeStringsCount();

string contentSummary = string.Format("{0} x{1}", typeof(string).Name, refStringsCount);
return refContainerPath + Path.DirectorySeparatorChar + contentSummary + ".json";
}

/** <summary> Reads and Deserializes the Contents of the <c>UserParams</c> file. </summary>
<param name = "inputPath"> The Path where the Reference Strings will be Read from. </param> */

public static ReferenceStrings ReadStrings(string inputPath)
{
ReferenceStrings refStrings;
string refFilePath = GetRefFilePath(inputPath);

if(!File.Exists(refFilePath) || Archive_Manager.FileIsEmpty(refFilePath) )
{
Text.PrintWarning("The Reference Strings Container is Empty or does not Exists.");
refStrings = new();
}

else
{
string jsonText = File.ReadAllText(refFilePath);
refStrings = JSON_Serializer.DeserializeObject<ReferenceStrings>(jsonText);
}

return refStrings;
}

/** <summary> Writes all the Reference Strings of the Container from this Instance. </summary> 
<param name = "outputPath"> The Path where the Reference Strings will be Saved. </param> */

public static void WriteStrings(string outputPath)
{
string refFilePath = GetRefFilePath(outputPath);
string jsonText = JSON_Serializer.SerializeObject(refStringsContainer);

File.WriteAllText(refFilePath, jsonText);
}

}

}