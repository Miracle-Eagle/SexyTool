using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Filtering Functions for Input Values. </summary>

internal static class Input_Manager
{
/** <summary> Sets a Value which Contains Info about the Blankspace Separator. </summary>
<returns> The Blankspace Separator. </returns> */

public static readonly char strSeparator_Blankspace = ' ';

/** <summary> Sets a Value which Contains Info about the Comma Separator. </summary>
<returns> The Comma Separator. </returns> */

public static readonly string strSeparator_Comma = ", ";

/** <summary> Sets a Value which Contains Info about the Conjunction Separator. </summary>
<returns> The Conjunction Separator. </returns> */

public static readonly string strSeparator_Conjunction = Text.LocalizedData.STRING_SEPARATOR_CONJUNCTION;

/** <summary> Sets a Value which Contains Info about the Hyphen Separator. </summary>
<returns> The Hyphen Separator. </returns> */

public static readonly string strSeparator_Hyphen = " - ";

/** <summary> Sets a Value which Contains Info about the Preposition Separator. </summary>
<returns> The Preposition Separator. </returns> */

public static readonly string strSeparator_Preposition = Text.LocalizedData.STRING_SEPARATOR_PREPOSITION;

/** <summary> Creates a List of Alphabetical Characters. </summary>
<returns> The Alphabetical Characters List. </returns> */

private static char[] AlphabeticalChars
{

get
{
string theAlphabet_Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
char[] upperLetters = theAlphabet_Uppercase.ToCharArray();

int upperLettersCount = upperLetters.Length;
string theAlphabet_Lowercase = theAlphabet_Uppercase.ToLower();

char[] lowerLetters = theAlphabet_Lowercase.ToCharArray();
int lowerLettersCount = lowerLetters.Length;

List<char> ABC = new();
int maxLettersCount = upperLettersCount | lowerLettersCount;

for(int i = 0; i < maxLettersCount; i++)
{
ABC.Add(upperLetters[i] );
ABC.Add(lowerLetters[i] );
}

return ABC.ToArray();
}

}

/** <summary> Creates a List of Numerical Characters. </summary>
<returns> The Numerical Characters List. </returns> */

private static readonly char[] numericalChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

/** <summary> Creates a List of Characters used for Filling File Names or Folder Names. </summary>
<returns> The Filling Characters List. </returns> */

private static readonly char[] fillingChars = { '°', '¬', '(', ')', '¿', '¡', '¨', '~', '[', ']', '^', '`', ',', ';', '.', '-', '_' };

/** <summary> Checks if a String is Empty or not. </summary>

<param name = "sourceStr"> The String to be Analized. </param>
<param name = "targetAction"> The Action to Perform in case the String is Null or Empty. </param> */

public static string CheckEmptyString(string sourceStr, Action<string> targetAction = default)
{
#region ====== Check Filling Action =======

targetAction ??= (fillStr) =>
{
Text.PrintAdvice(false, "\r" + Text.LocalizedData.ADVICE_REDIRECT_INPUT);
sourceStr = Console.ReadLine();
};

#endregion

while(string.IsNullOrEmpty(sourceStr) )
targetAction(sourceStr);

return sourceStr;
}

/** <summary> Checks if the extension Filter provided is Valid or not </summary>
<param name = "sourceFilter"> The Filter to be Analized. </param> */

public static void CheckExtensionFilter(ref string sourceFilter)
{
#region ====== Action - Set Filter ======

ActionWrapper<string> setFilterAction = new( (defaultFilter) =>
{
sourceFilter = "*.*";
} );

#endregion

string validFilter = CheckEmptyString(sourceFilter, setFilterAction.Init);
string defaultName = "*";	

if(!sourceFilter.StartsWith(defaultName) )
{
char extSeparator = '.';

if(sourceFilter.Contains(extSeparator) )
{
int dotIndex = sourceFilter.LastIndexOf(extSeparator) + 1;
string fileExtension = sourceFilter.Substring(dotIndex);

validFilter = defaultName + fileExtension;
}
	
else
validFilter = defaultName + sourceFilter;

}

sourceFilter = validFilter;
}

/** <summary> Converts a Bytes Array into a Hexadecimal String. </summary>

<param name = "inputBytes"> The Bytes to be Converted. </param>

<returns> The Hexadecimal String Converted. </returns> */

public static string ConvertHexString(byte[] inputBytes)
{
StringBuilder hexBuilder = new();
int inputBytesCount = inputBytes.Length;

for(int i = 0; i < inputBytesCount; i++)
{
byte singleByte = inputBytes[i];
string singleStr = singleByte.ToString("x2");

hexBuilder.Append(singleStr);
}

return hexBuilder.ToString();
}

/** <summary> Filters a Byte Value from User's Input. </summary>

<param name = "userInput"> The User's Input to be Filtered. </param>

<returns> The Filtered Value. </returns> */

public static bool FilterBool(string userInput)
{
CheckEmptyString(userInput);

bool booleanValue = userInput switch
{
"F" or "FALSE" or "False" or "false" or "f" or "0" => false,
"T" or "TRUE" or "True" or "true" or "t" or "1" => true,
_ => false | true
};

return booleanValue;
}

/** <summary> Filters a DateTime from user's Input. </summary>

<param name = "userInput"> The user's Input to be Filtered. </param>

<returns> The Filtered DateTime. </returns> */

public static DateTime FilterDateTime(string userInput) => DateTime.TryParse(userInput, Environment_Info.CurrentCultureInfo, out DateTime filteredDate) ? filteredDate : DateTime.Now;

/** <summary> Filters a Name from User's Input. </summary>

<param name = "sourceName"> The Name to be Filtered. </param>

<returns> The Filtered Name. </returns> */

public static string FilterName(string sourceName)
{
string validStr = CheckEmptyString(sourceName);
char[] invalidChars = GetInvalidChars(true);

int invalidCharsCount = invalidChars.Length;
string filteredName = string.Empty;

for(int i = 0; i < invalidCharsCount; i++)
{
char invalidSymbol = invalidChars[i];
bool HasInvalidChar = sourceName.Contains(invalidSymbol);

if(HasInvalidChar)
{
filteredName = validStr.Replace(invalidSymbol.ToString(), string.Empty);
validStr = filteredName;
}

filteredName = validStr;
}

return filteredName;
}

/** <summary> Filters a numeric Value from user's Input. </summary>

<param name = "userInput"> The user's Input to be Filtered. </param>

<returns> The Filtered Value. </returns> */

public static T FilterNumber<T>(string userInput) where T : struct
{
CheckEmptyString(userInput);
return ValidateNumericRange<T>(ExtractNumericDigits(userInput) );
}


/** <summary> Extracts the Numeric Digits from user's Input. </summary>

<param name = "sourceStr"> The String to be Analized. </param>

<returns> A Sequence of Chars that represent the numerical Digits. </returns> */

public static string ExtractNumericDigits(string sourceStr)
{
Match numericMatch = Regex.Match(sourceStr, @"([-+]?\d*\.?\d+)");
string numericDigits;

if(numericMatch.Success)
{
Group numbersGroup = numericMatch.Groups[1];
numericDigits = numbersGroup.Value;
}

else
{
char defaultNumber = numericalChars[0];
numericDigits = defaultNumber.ToString();
}

return numericDigits;
}

/** <summary> Gets a List of Characters from the Keyword. </summary>

<param name = "excludeInvalidChars"> Determines if invalid Chars should be Excluded from the Keyword or not. </param>

<returns> The Keyword Characters List. </returns> */

private static char[] GetKeywordChars(bool excludeInvalidChars)
{
char[] keywordChars;

if(excludeInvalidChars)
keywordChars = MergeArrays(AlphabeticalChars, numericalChars, fillingChars);

else
{
char[] invalidChars = GetInvalidChars(true);
keywordChars = MergeArrays(AlphabeticalChars, numericalChars, fillingChars, invalidChars);
}

return keywordChars;
}

/** <summary> Generates a String by using the Characters from the Keyword. </summary>
<returns> The Keyword String Generated. </returns> */

public static string GenerateKeywordString()
{
char[] keywordChars = GetKeywordChars(false | true);
return new(keywordChars);
}

/** <summary> Generates a Random Value that serves as a String Complement . </summary>
<returns> The String Complement Generated. </returns> */

public static char GenerateStringComplement()
{
int fillingCharsCount = fillingChars.Length;
Random randomizer = new();

int randomIndex = randomizer.Next(fillingCharsCount - 1);
return fillingChars[randomIndex];
}

/** <summary> Gets the Display Size of a Meassure expressed in Bits, Bytes, Kilobytes, Megabytes and Gigabytes.  </summary>

<param name = "sourceAmount"> The Amount to be Displayed. </param>

<returns> The Display Size of the Meassure. </returns> */

public static string GetDisplaySize(long sourceAmount)
{
double sizeFactor;
string meassureSymbol;

if(sourceAmount >= Constants.ONE_GIGABYTE)
{
sizeFactor = sourceAmount/Constants.ONE_GIGABYTE;
meassureSymbol = "GB";
}

else if(sourceAmount >= Constants.ONE_MEGABYTE)
{
sizeFactor = sourceAmount/Constants.ONE_MEGABYTE;
meassureSymbol = "MB";
}

else if(sourceAmount >= Constants.ONE_KILOBYTE)
{
sizeFactor = sourceAmount/Constants.ONE_KILOBYTE;
meassureSymbol = "KB";
}

else
{
sizeFactor = sourceAmount/Constants.ONE_BYTE;
meassureSymbol = "B";
}

double sizeProximity = Math.Ceiling(sizeFactor);
long realSize = Convert.ToInt64(sizeProximity);

string sizeValue = realSize.ToString("n0", Environment_Info.CurrentCultureInfo);
return sizeValue + strSeparator_Blankspace + meassureSymbol;
}

/** <summary> Gets a List of Invalid Characters for File Names or Folder Names. </summary>

<param name = "isShortName"> Determines if the File/Folder name is a Name (Short Name) or a Path (Full Name). </param>

<returns> The Invalid Characters List. </returns> */

public static char[] GetInvalidChars(bool isShortName)
{
char[] invalidChars;

if(isShortName)
invalidChars = Path.GetInvalidFileNameChars();

else
invalidChars = Path.GetInvalidPathChars();

return invalidChars;
}

/** <summary> Fills an Array in order to Reach the specified Length. </summary>

<param name = "sourceArray"> The Array to be Filled. </param>
<param name = "expectedLength"> The Length expected. </param>

<returns> The Array Filled. </returns> */

public static void FillArray<T>(ref T[] sourceArray, int expectedLength)
{
expectedLength = (expectedLength < 0) ? 1 : expectedLength;
sourceArray ??= new T[expectedLength];

int currentLength = sourceArray.Length;

if(currentLength == expectedLength)
return;

T[] paddedArray = new T[expectedLength];

if(currentLength < expectedLength)
sourceArray.CopyTo(paddedArray, 0);

else
Array.Copy(sourceArray, paddedArray, expectedLength);

sourceArray = paddedArray;
}

/** <summary> Fills a String in order to Reach the specified Length. </summary>

<param name = "sourceStr"> The String to be Filled. </param>
<param name = "expectedLength"> The Length expected. </param>

<returns> The String Filled. </returns> */

public static void FillString(ref string sourceStr, int expectedLength)
{
expectedLength = (expectedLength < 0) ? 1 : expectedLength;
sourceStr ??= string.Empty;

int currentLength = sourceStr.Length;

if(currentLength == expectedLength)
return;

if(expectedLength < currentLength)
throw new ArgumentException("Padding Value can't be Less than the Length of your String");

sourceStr = sourceStr.PadLeft(expectedLength, '0');
}

/** <summary> Merges two Arrays as a Single one. </summary>

<param name="arrayX"> The First Array to be Merged. </param>
<param name="arrayY"> The Second Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayX, T[] arrayY)
{
T[] mergedArray;

if( (arrayX == null || arrayX.Length == 0) && (arrayY == null || arrayY.Length == 0) )
mergedArray = new T[0];

else if(arrayX == null || arrayX.Length == 0)
mergedArray = arrayY;

else if(arrayY == null || arrayY.Length == 0)
mergedArray = arrayX;

else
mergedArray = arrayX.Concat(arrayY).ToArray();

return mergedArray;
}

/** <summary> Merges three Arrays as a Single one. </summary>

<param name="arrayX"> The First Array to be Merged. </param>
<param name="arrayY"> The Second Array to be Merged. </param>
<param name="arrayZ"> The Third Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayX, T[] arrayY, T[] arrayZ)
{
T[] arrayXY = MergeArrays(arrayX, arrayY);
T[] mergedArray;

if( (arrayXY == null || arrayXY.Length == 0) && (arrayZ == null || arrayZ.Length == 0) )
mergedArray = new T[0];

else if(arrayXY == null || arrayXY.Length == 0)
mergedArray = arrayZ;

else if(arrayZ == null || arrayZ.Length == 0)
mergedArray = arrayXY;

else
mergedArray = MergeArrays(arrayXY, arrayZ);

return mergedArray;
}

/** <summary> Merges four Arrays as a Single one. </summary>

<param name="arrayW"> The First Array to be Merged. </param>
<param name="arrayX"> The Second Array to be Merged. </param>
<param name="arrayY"> The Third Array to be Merged. </param>
<param name="arrayZ"> The Fourth Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayW, T[] arrayX, T[] arrayY, T[] arrayZ)
{
T[] arrayWX = MergeArrays(arrayW, arrayX);
T[] arrayYZ = MergeArrays(arrayY, arrayZ);

T[] mergedArray;

if( (arrayWX == null || arrayWX.Length == 0) && (arrayYZ == null || arrayYZ.Length == 0) )
mergedArray = new T[0];

else if(arrayWX == null || arrayWX.Length == 0)
mergedArray = arrayYZ;

else if(arrayYZ == null || arrayYZ.Length == 0)
mergedArray = arrayWX;

else
mergedArray = MergeArrays(arrayWX, arrayYZ);

return mergedArray;
}

/** <summary> Checks if a sequence of Numbers repressented as a String is on Range or not. </summary>

<param name = "numericDigits"> The numeric Digits Sequence. </param>

<returns> A Value that is Inside the Range of the expected Type. </returns> */

private static T ValidateNumericRange<T>(string numericDigits) where T : struct
{
object parsedObj = Convert.ChangeType(numericDigits, typeof(T) );
T numericValue;

if(parsedObj == null)
numericValue = default;

else
numericValue = (T)parsedObj;

return numericValue;
}

}

}