using SexyTool.Program.Graphics.UserSelections;
using System;

namespace SexyTool.Program.Core.Functions.Other
{
/// <summary> Initializes calculating Functions for Time Values (such as DateTimes and TimeStamps). </summary>

public class TimeCalculator
{
/** <summary> Sets a Value which Contains Info about the epoch Time. </summary>
<returns> The epoch Time. </returns> */

private static readonly DateTime epochTime = DateTime.Parse("1970/1/1");

/** <summary> Displays the Conversion of a TimeStamp into a DateTime. </summary>
<param name = "targetValue"> The TimeStamp to Convert. </param> */

internal static void DateTimeConversion(double targetValue)
{
DateTime dateTimeValue = CalculateDateTime(targetValue);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_DATE_TIME, dateTimeValue) );
}

/** <summary> Displays the Conversion of a DateTime into a TimeSpam. </summary>
<param name = "targetValue"> The DateTime to Convert. </param> */

internal static void TimeStampConversion(DateTime targetValue)
{
double timeStampValue = CalculateTimeStamp(targetValue);
Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_TIME_STAMP, timeStampValue) );
}

/** <summary> Calculates a DateTime from a given TimeStamp Value. </summary>
<param name = "timeStampValue"> The TimeStamp where the DateTime will be Calculated from. </param>

<exception cref = "ArgumentOutOfRangeException"></exception>
<exception cref = "OverflowException"></exception>

<returns> The DateTime Calculated. </returns> */

public static DateTime CalculateDateTime(double timeStampValue) => epochTime.AddSeconds(timeStampValue);

/** <summary> Calculates a TimeStamp from a given DateTime Value. </summary>
<param name = "dateTimeValue"> The DateTime where the TimeStamp will be Calculated from. </param>

<exception cref = "ArgumentOutOfRangeException"></exception>
<returns> The TimeStamp Calculated. </returns> */

public static double CalculateTimeStamp(DateTime dateTimeValue)
{
TimeSpan timeDifference = dateTimeValue.Subtract(epochTime);
return Math.Truncate(timeDifference.TotalSeconds);
}

}

}