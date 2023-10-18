using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.Menus;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SexyTool.Program
{	
/// <summary> Initializes Launching Functions for this Program. </summary>

public static class Program
{
/** <summary> Sets a Value which Contains Info about the Settings of this Program. </summary>
<returns> The Settings of this Program. </returns> */

public static Settings config = new();

/** <summary> Sets a Value which Contains Info about the Params of the User. </summary>
<returns> The Params of the User. </returns> */

public static UserParams userParams = new();

/** <summary> Launches the Program on the User's Enviroment. </summary>
<param name = "inputArgs"> The Arguments to be Passed to the Program in the Execution. </param> */

public static async Task Main(string[] inputArgs)
{
Stopwatch timer = new();
Task currentAppTask = default;

try
{
Console.Clear();
Console.InputEncoding = Encoding.UTF8;

Console.OutputEncoding = Encoding.Unicode;
config = Settings.Read();

userParams = UserParams.Read();
Interface.DisplayElements();

if(inputArgs.Length == 1)
{
char expectedKey = (char)Interface.GetDialog<QuickModeDialog>().Popup();
// inputArgs[0];
}

else
{
Framework targetFramework = (Framework)Interface.GetMenu<FrameworksMenu>().DynamicSelection();
currentAppTask = Loader.NormalLoad(targetFramework);
}

timer.Start();
currentAppTask.Start();
}

catch(AggregateException error)
{
Exceptions_Handler.SetErrorCaught(error);
Exceptions_Handler.DisplayErrorInfo();
}

finally
{

if(currentAppTask != null)
{
await currentAppTask;
timer.Stop();

Task_Manager.EvaluateTaskStatus(currentAppTask.Status);
string elapsedTime = Termination.GetElapsedTime(timer.ElapsedMilliseconds);

Text.PrintLine(true, Text.LocalizedData.PROCESS_ELAPSED_TIME, elapsedTime);
}

await Termination.CloseProgram();
}

}

}

}