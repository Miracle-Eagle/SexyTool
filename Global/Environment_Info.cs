using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;

/// <summary> Serves as a Reference to the Variables of the Environment where this Program runs. </summary> 

public static class Environment_Info
{
/** <summary> Sets a Value which Contains Info about the Current Assembly that is being Executed. </summary>
<returns> The Current Assembly that is being Executed. </returns> */

public static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

/** <summary> Sets a Value which Contains Info about the Name of the Current Assembly that is being Executed. </summary>
<returns> The Name of the Current Assembly that is being Executed. </returns> */

public static readonly AssemblyName CurrentAssemblyName = CurrentAssembly.GetName();

/** <summary> Sets a Value which Contains Info about the Version of the Current Assembly that is being Executed. </summary>
<returns> The Version of the Current Assembly that is being Executed. </returns> */

public static readonly Version CurrentAssemblyVersion = CurrentAssemblyName.Version;

/** <summary> Sets a Value which Contains Info about the Location of the Current Assembly that is being Executed. </summary>
<returns> The Location of the Current Assembly that is being Executed. </returns> */

public static readonly string CurrentAssemblyLocation = CurrentAssembly.Location;

/** <summary> Sets a Value which Contains Info about the Types of the Current Assembly that is being Executed. </summary>
<returns> The Types of the Current Assembly that is being Executed. </returns> */

public static readonly Type[] CurrentAssemblyTypes = CurrentAssembly.GetTypes();

/** <summary> Gets the Architecture of the Device where the Program is Installed. </summary>
<returns> The Architecture of the Device </returns> */

public static string DeviceArchitecture
{

get
{
string deviceFlags;

if(Environment.Is64BitOperatingSystem)
deviceFlags = "64-Bits";

else
deviceFlags = "32-Bits";

return deviceFlags;
}

}

/** <summary> Sets a Value which Contains Info about the Operating System where the Program is being Executed. </summary>
<returns> The OS Version of the Environment. </returns> */

public static readonly OperatingSystem OS_Version = Environment.OSVersion;

/** <summary> Sets a Value which Contains Info about the CLR Version of the Operating System where the Program is being Executed. </summary>
<returns> The CLR Version of the Environment. </returns> */

public static readonly Version CLR_Version = Environment.Version;

/** <summary> Gets the System Startup Time. </summary>
<returns> The System Startup Time </returns> */

public static long SystemStartupTime
{

get
{
long startupTime;

if(DeviceArchitecture == "32-Bits")
startupTime = Environment.TickCount;

else
startupTime = Environment.TickCount64;

return startupTime;
}

}

/** <summary> Sets a Value which Contains Info about the System Page Size. </summary>
<returns> The System Page Size. </returns> */

public static readonly int SystemPageSize = Environment.SystemPageSize;

/** <summary> Sets a Value which Contains Info about the System Directory. </summary>
<returns> The System Directory. </returns> */

public static readonly string SystemDirectory = Environment.SystemDirectory;

/** <summary> Sets a Value which Contains Info about the Name of the Machine. </summary>
<returns> The Name of the Machine. </returns> */

public static readonly string MachineName = Environment.MachineName;

/** <summary> Sets a Value which Contains Info about the Drives List of the Device. </summary>
<returns> The Drives List of the Device. </returns> */

public static readonly string[] DrivesList = Directory.GetLogicalDrives();

/** <summary> Sets a Value which Contains Info about the Number of Processors the Device has. </summary>
<returns> The Number of Processors the Device has. </returns> */

public static readonly int ProcessorsCount = Environment.ProcessorCount;

/** <summary> Sets a Value which Contains Info about the Directory which the Current App is using. </summary>
<returns> The Directory which the Current App is using. </returns> */

public static readonly string CurrentAppDirectory = Environment.CurrentDirectory;

/** <summary> Sets a Value which Contains Info about the Path of the Process which is being Executed. </summary>
<returns> The Path of the Process which is being Executed. </returns> */

public static readonly string CurrentProcessPath = Environment.ProcessPath;

/** <summary> Sets a Value which Contains Info about the WorkingSet which the Current App is using. </summary>
<returns> The WorkingSet which the Current App is using. </returns> */

public static readonly long AppWorkingSet = Environment.WorkingSet;

/** <summary> Sets a Value which Contains Info about the Name of the User which is using the App. </summary>
<returns> The Name of the User which is using the App. </returns> */

public static readonly string CurrentUserName = Environment.UserName;

/** <summary> Sets a Value which Contains Info about the Domain of the User which is using the App. </summary>
<returns> The Domain of the User which is using the App. </returns> */

public static readonly string CurrentUserDomain = Environment.UserDomainName;

/** <summary> Sets a Value which Contains Info about the Culture that Program should use. </summary>
<returns> The Culture that Program should use. </returns> */

public static CultureInfo CurrentCultureInfo
{

get => Thread.CurrentThread.CurrentCulture;

}

/** <summary> Sets a Value which Contains Info about the Region where the User belongs to. </summary>
<returns> The Region where the User belongs to. </returns> */

private static RegionInfo CurrentRegionInfo
{

get
{
string cultureName = CurrentCultureInfo.Name;
return new RegionInfo(cultureName);
}

}

/** <summary> Sets a Value which Contains Info about the Country where the User belongs to. </summary>
<returns> The Country where the User belongs to. </returns> */

public static readonly string CurrentUserCountry = CurrentRegionInfo.DisplayName;

/** <summary> Sets a Value which Contains Info about the Language of the User. </summary>
<returns> The Language of the User. </returns> */

public static string CurrentUserLanguage
{

get => CurrentCultureInfo.IsNeutralCulture ? CurrentCultureInfo.NativeName : CurrentCultureInfo.Parent.NativeName;

}

/** <summary> Sets a Value which Contains Info about the Current Date Format. </summary>
<returns> The Current Date Formart </returns> */

public static readonly DateTimeFormatInfo CurrentDateFormat = CurrentCultureInfo.DateTimeFormat;

/** <summary> Creates or Obtains a List of Variables used on the Environment of the Device. </summary>
<returns> The List of Variables used on the Environment of the Device. </returns> */

public static IDictionary Variables
{

get => Environment.GetEnvironmentVariables();
set => Variables = value;

}

}