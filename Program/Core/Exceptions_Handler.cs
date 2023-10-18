using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Handling Functions for the Exceptions that Ocur when Running this Program. </summary>

internal static class Exceptions_Handler
{
/** <summary> Grts or Sets a Value which Contains Info about the Exception that was Caught by this Class. </summary>
<returns> The Error Caught. </returns> */

public static Exception errorCaught;

/// <summary> Displays all the Info related to the Exception that was Caught. </summary>

public static void DisplayErrorInfo()
{
Text.PrintHeader(Text.LocalizedData.HEADER_ERROR_INFO);
var errorInfo = GetErrorInfo();

Text.PrintDictionary(errorInfo);
}

/** <summary> Gets all the Info related to the Exception that was Caught. </summary>
<returns> The Info Obtained from the Exception that was Caught. </returns> */

private static Dictionary<string, object> GetErrorInfo()
{
Dictionary<string, object> errorInfo = new();
errorInfo.Add(Text.LocalizedData.ERROR_INFO_TYPE, errorCaught.GetType() );

errorInfo.Add(Text.LocalizedData.ERROR_INFO_MESSAGE, errorCaught.Message);
errorInfo.Add(Text.LocalizedData.ERROR_INFO_HELPFUL_LINK, errorCaught.HelpLink);

if(Info.BuildConfig == "Debug")
{
errorInfo.Add(Text.LocalizedData.ERROR_INFO_HANDLE_RESULT, errorCaught.HResult);
errorInfo.Add(Text.LocalizedData.ERROR_INFO_TRACE_CODE, errorCaught.StackTrace);

errorInfo.Add(Text.LocalizedData.ERROR_INFO_TARGET_SITE, errorCaught.TargetSite);
errorInfo.Add(Text.LocalizedData.ERROR_INFO_SOURCE, errorCaught.Source);

errorInfo.Add(Text.LocalizedData.ERROR_INFO_BASE_EXCEPTION, errorCaught.GetBaseException() );
errorInfo.Add(Text.LocalizedData.ERROR_INFO_INNER_EXCEPTION, errorCaught.InnerException);

IDictionary errorData = errorCaught.Data;

if(errorData.Count > 0)
errorInfo.Add(Text.LocalizedData.ERROR_INFO_DATA, errorData);

else
errorInfo.Add(Text.LocalizedData.ERROR_INFO_DATA, Text.LocalizedData.DIALOG_NO_DATA_AVAILABLE);

}

return errorInfo;
}

/** <summary> Sets a value for the Exception that was Caught. </summary>
<param name = "targetException"> The Exception to be Handled. </param> */

public static void SetErrorCaught(Exception targetException) => errorCaught = targetException;
}

}