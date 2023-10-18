using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SexyTool.Program
{
/// <summary> The Termination of this Program and all the Tasks related to it. </summary>

internal class Termination
{
/** <summary> Sets a Value which Contains Info about the return Key of this Program. </summary>
<returns> The return Key of this Program. </returns> */

public const ConsoleKey returnKey = ConsoleKey.R;

/// <summary> Closes the Program, releasing all the memory Consumed by its Process. </summary>

public static async Task CloseProgram()
{
Memory_Manager.ReleaseMemory();
Interface.GetDialog<CloseProgramDialog>().Popup();

int exitDelay = Constants.MILLISECONDS * 3;
CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();

_ = StartShutdownTimer(cancelTokenSrc.Token, exitDelay);
bool returnToMainMenu = WaitForReturnKey(exitDelay);

cancelTokenSrc.Cancel();

if(returnToMainMenu)
{
string[] returnArgs = { "Return", "Main" };
await Program.Main(returnArgs);
}

else
{
int exitCode = 9072;
Environment.Exit(exitCode);
}

}

/** <summary> Gets the Elapsed Time of a Function that is Completed. </summary> 
<param name = "sourceMs"> The Elapsed Time of a Function Expressed in Milliseconds. </param>

<returns> The Elapsed Time of the Function. </returns> */

public static string GetElapsedTime(long sourceMs)
{
string CompletionInMilliseconds = sourceMs.ToString("n0", Environment_Info.CurrentCultureInfo);
string millisecondsSymbol = "ms";

long elapsedSeconds = sourceMs / Constants.MILLISECONDS;
string CompletionInSeconds = elapsedSeconds.ToString("n0", Environment_Info.CurrentCultureInfo);

long minSecondsValue = Constants.MILLISECONDS;
char secondsSymbol = 's';

long elapsedMinutes = elapsedSeconds / Constants.SECONDS;
string CompletionInMinutes = elapsedMinutes.ToString("n0", Environment_Info.CurrentCultureInfo);

long minMinutesValue = minSecondsValue * Constants.SECONDS;
string minutesSymbol = "min";

long elapsedHours = elapsedMinutes / Constants.MINUTES;
string CompletionInHours = elapsedHours.ToString("n0", Environment_Info.CurrentCultureInfo);

long minHoursValue = minMinutesValue * Constants.MINUTES;
char hoursSymbol = 'h';

string minuteHand;
string secondHand;

string elapsedTime;

if(sourceMs >= minHoursValue)
{
minuteHand = CompletionInHours + Input_Manager.strSeparator_Blankspace + hoursSymbol + Input_Manager.strSeparator_Preposition + CompletionInMinutes + Input_Manager.strSeparator_Blankspace + minutesSymbol;
secondHand = CompletionInSeconds + Input_Manager.strSeparator_Blankspace + secondsSymbol + Input_Manager.strSeparator_Preposition + CompletionInMilliseconds + Input_Manager.strSeparator_Blankspace + millisecondsSymbol;

elapsedTime = minuteHand + Input_Manager.strSeparator_Conjunction + secondHand;
}

else if(sourceMs >= minMinutesValue)
{
minuteHand = CompletionInMinutes + Input_Manager.strSeparator_Blankspace + minutesSymbol;
secondHand = CompletionInSeconds + Input_Manager.strSeparator_Blankspace + secondsSymbol + Input_Manager.strSeparator_Conjunction + CompletionInMilliseconds + Input_Manager.strSeparator_Blankspace + millisecondsSymbol;

elapsedTime = minuteHand + Input_Manager.strSeparator_Comma + secondHand;
}

else if(sourceMs >= minSecondsValue)
{
elapsedTime = CompletionInSeconds + Input_Manager.strSeparator_Blankspace + secondsSymbol + Input_Manager.strSeparator_Preposition + CompletionInMilliseconds + Input_Manager.strSeparator_Blankspace + millisecondsSymbol;
}

else
{
elapsedTime = CompletionInMilliseconds + Input_Manager.strSeparator_Blankspace + millisecondsSymbol;
}

return elapsedTime;
}

/** <summary> Starts a Countdown with the specified CancellationToken and Duration for Closing this Program. </summary> 
<param name = "cancelToken"> Indicates if the shutdown Countdown should be Cancelled or not. </param>

<param name = "countdownDuration"> The duration of the Countdown, expressed in Milliseconds. </param>
<returns> A Task that Represents the Countdown made. </returns> */

private static async Task StartShutdownTimer(CancellationToken cancelToken, int countdownDuration)
{
int countdownSeconds = countdownDuration / Constants.MILLISECONDS;

for(int i = countdownSeconds; i >= 0; i--)
{

if(cancelToken.IsCancellationRequested)
return;

string countdownMsg = string.Format(Text.LocalizedData.ACTION_CLOSE_PROGRAM, Info.ProgramTitle, i);
Console.Write("\r" + countdownMsg);

await Task.Delay(Constants.MILLISECONDS);
}

}

/** <summary> Waits the User for Entering the return Key in the specified Delay. </summary>
<param name = "exitDelay"> The delay before Closing the Program, expressed in Milliseconds. </param>

<returns> A boolean that Indicates if the User pressed the return Key or not. </returns> */

private static bool WaitForReturnKey(int exitDelay)
{
DateTime startTime = DateTime.Now;

while(true)
{

if(Console.KeyAvailable)
{
ConsoleKey keyPressed = Console.ReadKey(true).Key;

if(keyPressed == returnKey)
{
return true;
}

}

TimeSpan timeDifference = DateTime.Now - startTime;

if(timeDifference.TotalMilliseconds >= exitDelay)
{
return false;
}

Thread.Sleep(100);
}

}

}

}