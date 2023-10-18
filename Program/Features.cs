using Newtonsoft.Json.Linq;
using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Menus;
using SexyTool.Program.Graphics.UserSelections;
using System.Collections;
using System.IO;

namespace SexyTool.Program
{
/// <summary> The Features of this Program. </summary>

internal class Features
{
/// <summary> Displays the Settings of this Program. </summary>

public static void DisplayAppSettings()
{
Interface.GetMenu<AppSettings>().DynamicSelection();
Settings.Write(Program.config);
}

/// <summary> Displays info related to this Program. </summary>

public static void DisplayProgramInfo()
{
Text.PrintHeader(Text.LocalizedData.HEADER_PROGRAM_INFO);
Text.PrintJson(GetProgramInfo() );
}

/// <summary> Displays info about the User who is running the Program. </summary>

public static void DisplayUserInfo()
{
Text.PrintHeader(Text.LocalizedData.HEADER_USER_INFO);
Text.PrintJson(GetUserInfo() );
}

/// <summary> Allows the User to comit Changes to the ParamsGroup of this Program. </summary>

public static void EditParamsGroup()
{
Interface.GetMenu<ParamsEditor>().DynamicSelection();
UserParams.Write(Program.userParams);
}

/** <summary> Gets Info about the curent Environment where the Program is being Executed. </summary>
<returns> Info related to the Environment where the Program is being Executed. </returns> */

private static JObject GetEnvironmentInfo()
{
JObject environmentInfo = new();
JProperty property_deviceArchitecture = new(Text.LocalizedData.ENVIRONMENT_INFO_DEVICE_ARCHITECTURE, Environment_Info.DeviceArchitecture);

environmentInfo.Add(property_deviceArchitecture);
JProperty property_OS_Version = new(Text.LocalizedData.ENVIRONMENT_INFO_OS_VERSION, Environment_Info.OS_Version.ToString() );

environmentInfo.Add(property_OS_Version);
JProperty property_CLR_Version = new(Text.LocalizedData.ENVIRONMENT_INFO_CLR_VERSION, Environment_Info.CLR_Version.ToString() );

environmentInfo.Add(property_CLR_Version);
JProperty property_systemStartupTime = new(Text.LocalizedData.ENVIRONMENT_INFO_SYSTEM_STARTUP_TIME, Termination.GetElapsedTime(Environment_Info.SystemStartupTime) );

environmentInfo.Add(property_systemStartupTime);
JProperty property_systemPageSize = new(Text.LocalizedData.ENVIRONMENT_INFO_SYSTEM_PAGE_SIZE, Input_Manager.GetDisplaySize(Environment_Info.SystemPageSize) );

environmentInfo.Add(property_systemPageSize);
JProperty property_systemDirectory = new(Text.LocalizedData.ENVIRONMENT_INFO_SYSTEM_DIRECTORY, Environment_Info.SystemDirectory);

environmentInfo.Add(property_systemDirectory);
JProperty property_machineName = new(Text.LocalizedData.ENVIRONMENT_INFO_MACHINE_NAME, Environment_Info.MachineName);

environmentInfo.Add(property_machineName);
JProperty property_logicalDrives = new(Text.LocalizedData.ENVIRONMENT_INFO_LOGICAL_DRIVES, Environment_Info.DrivesList);

environmentInfo.Add(property_logicalDrives);
JProperty property_processorsCount = new(Text.LocalizedData.ENVIRONMENT_INFO_PROCESSORS_COUNT, Environment_Info.ProcessorsCount);

environmentInfo.Add(property_processorsCount);
JProperty property_currentAppDirectory = new(Text.LocalizedData.ENVIRONMENT_INFO_CURRENT_APP_DIRECTORY, Environment_Info.CurrentAppDirectory);

environmentInfo.Add(property_currentAppDirectory);
JProperty property_currentProcessPath = new(Text.LocalizedData.ENVIRONMENT_INFO_CURRENT_PROCESS_PATH, Environment_Info.CurrentProcessPath);

environmentInfo.Add(property_currentProcessPath);
JProperty property_appWorkingSet = new(Text.LocalizedData.ENVIRONMENT_INFO_APP_WORKING_SET, Input_Manager.GetDisplaySize(Environment_Info.AppWorkingSet) );

environmentInfo.Add(property_appWorkingSet);
JObject systemProperties = new JObject();

foreach(DictionaryEntry singleVar in Environment_Info.Variables)
{
string singleName = singleVar.Key.ToString();
object singleValue = singleVar.Value;

JProperty singleVarInfo = new JProperty(singleName, singleValue);
systemProperties.Add(singleVarInfo);
}

JProperty property_Variables = new(Text.LocalizedData.ENVIRONMENT_INFO_SYSTEM_VARIABLES, systemProperties);
environmentInfo.Add(property_Variables);

JProperty dataContainer = new(Text.LocalizedData.HEADER_ENVIRONMENT_INFO, environmentInfo);
return new JObject(dataContainer);
}

/** <summary> Gets Info about this Program. </summary>
<returns> Info related to this Program. </returns> */

private static JObject GetProgramInfo()
{
JObject programInfo = new();
JProperty property_programProducer = new(Text.LocalizedData.PROGRAM_INFO_PRODUCER, Info.ProgramProducer);

programInfo.Add(property_programProducer);
JProperty property_programProduct = new(Text.LocalizedData.PROGRAM_INFO_NAME, Info.ProgramProduct);

programInfo.Add(property_programProduct);
JProperty property_buildConfig = new(Text.LocalizedData.PROGRAM_INFO_BUILD_CONFIG, Info.BuildConfig);

programInfo.Add(property_buildConfig);
JProperty property_programVersion = new(Text.LocalizedData.PROGRAM_INFO_VERSION, Info.ProgramVersion);

programInfo.Add(property_programVersion);
JProperty property_compileVersion = new(Text.LocalizedData.PROGRAM_INFO_COMPILE_VERSION, Info.CompileVersion.ToString() );

programInfo.Add(property_compileVersion);
JProperty property_compilationTime = new(Text.LocalizedData.PROGRAM_INFO_COMPILE_TIME, Info.CompilationTime);

programInfo.Add(property_compilationTime);
return programInfo;
}

/** <summary> Gets Info about the current User who is Running this Program. </summary>
<returns> Info related to the User who is Running the Program. </returns> */

private static JObject GetUserInfo()
{
JObject userInfo = new();
JProperty property_currentUserName = new(Text.LocalizedData.USER_INFO_CURRENT_NAME, Environment_Info.CurrentUserName);

userInfo.Add(property_currentUserName);
JProperty property_currentUserDomain = new(Text.LocalizedData.USER_INFO_CURRENT_DOMAIN, Environment_Info.CurrentUserDomain);

userInfo.Add(property_currentUserDomain);
JProperty property_currentUserCountry = new(Text.LocalizedData.USER_INFO_CURRENT_COUNTRY, Environment_Info.CurrentUserCountry);

userInfo.Add(property_currentUserCountry);
JProperty property_currentUserLanguage = new(Text.LocalizedData.USER_INFO_CURRENT_LANGUAGE, Environment_Info.CurrentUserLanguage);

userInfo.Add(property_currentUserLanguage);
return userInfo;
}

/** <summary> Saves the Environment Info of this Program to a new File. </summary>
<param name = "outputPath"> The Path where the Environment Info will be Saved. </param> */

public static void SaveEnvironmentInfo(string outputPath)
{
Text.Print(true, Text.LocalizedData.ACTION_SAVE_ENVIRONMENT_INFO, Path.GetFileName(outputPath) );
string jsonText = GetEnvironmentInfo().ToString();

JSON_Serializer.FormatJsonString(ref jsonText);
File.WriteAllText(outputPath, jsonText);
}

}

}