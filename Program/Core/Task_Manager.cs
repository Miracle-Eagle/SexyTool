using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Handling Functions for any Task that is being Executed in the Runtime Enviroment of the Device. </summary>

internal static class Task_Manager
{
/** <summary> Sets a Value which Contains Info about the Minimum Size of a Process. </summary>
<returns> The Minimum Process Size Obtained. </returns> */

private static readonly int minProcessSize = -1;

/** <summary> Sets a Value which Contains Info about the Maximum Size of a Process. </summary>
<returns> The Maximum Process Size Obtained. </returns> */

private static readonly int maxProcessSize = -1;

/** <summary> Sets a new Working Size to a Process. </summary>

<param name = "targetProcessHandle"> The Handle of the Process to be Resized. </param>
<param name = "minWorkingSize"> The Minimum Size that will be Given to the Process. </param>
<param name = "maxWorkingSize"> The Maximum Size that will be Given to the Process. </param>

<returns> The new Working Size of the Process. </returns> */

[ DllImport("kernel32.dll") ]

private static extern int SetProcessWorkingSetSize(IntPtr targetProcessHandle, int minWorkingSize, int maxWorkingSize);

/** <summary> Peforms a Task in Batches by iterating between each File inside of a Directory. </summary>

<param name = "inputPath"> The Path where the Files to be Processed are Located. </param>
<param name = "outputPath"> The Location where the resulting Files will be Saved. </param>
<param name = "targetAction"> The Action to be Performed. </param>

<param name = "extensionsFilter"> A filter that specifies wich Files will be retrieved from the Directory<para>
(Default filter is *.*, meaning all files from tbe Directory will be Listed, no mather their Name or Extension. </param> */

public static void BatchTask(string inputPath, string outputPath, Action<string, string> targetAction, string extensionsFilter = "*.*")
{
Input_Manager.CheckExtensionFilter(ref extensionsFilter);
string[] inputFilesList = Directory.GetFiles(inputPath, extensionsFilter, SearchOption.AllDirectories);

Directory_Manager.CheckMissingFolder(outputPath);

foreach(string inputFilePath in inputFilesList)
{
string relativePath = Path.GetRelativePath(inputPath, inputFilePath);
string outputFilePath = Path.Combine(outputPath, relativePath);

string outputSubfolderPath = Path.GetDirectoryName(outputFilePath);
Directory_Manager.CheckMissingFolder(outputSubfolderPath);

targetAction(inputFilePath, outputFilePath);
}

}

/** <summary> Peforms a Task in Batches by iterating between each File inside of a Directory. </summary>

<param name = "inputPath"> The Path where the Files to be Processed are Located. </param>
<param name = "outputPath"> The Location where the resulting Files will be Saved. </param>
<param name = "specificNames"> A List of Specific Names used for Filtering the Files. </param>
<param name = "specificExtensions"> A List of Specific Extensions used for Filtering the Files. </param>
<param name = "targetAction"> The Action to be Performed. </param> */

public static void BatchTask(string inputPath, string outputPath, List<string> specificNames, List<string> specificExtensions, Action<string, string> targetAction)
{
string[] inputFilesList = Directory.GetFiles(inputPath, "*.*", SearchOption.AllDirectories);
Path_Helper.FilterFilesList(ref inputFilesList, specificNames, specificExtensions);

Directory_Manager.CheckMissingFolder(outputPath);

foreach(string inputFilePath in inputFilesList)
{
string relativePath = Path.GetRelativePath(inputPath, inputFilePath);
string outputFilePath = Path.Combine(outputPath, relativePath);

string outputSubfolderPath = Path.GetDirectoryName(outputFilePath);
Directory_Manager.CheckMissingFolder(outputSubfolderPath);

targetAction(inputFilePath, outputFilePath);
}

}

/** <summary> Evaluates and Displays the Status of a Task. </summary> 
<param name = "sourceTaskStatus"> The TaskStatus to be Evaluated. </param> */

public static void EvaluateTaskStatus(TaskStatus sourceTaskStatus)
{
string displayStatusMsg;

switch(sourceTaskStatus)
{
case TaskStatus.Created:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_CREATED;
break;

case TaskStatus.WaitingForActivation:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_WAITING_FOR_ACTIVATION;
break;

case TaskStatus.WaitingToRun:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_WAITING_TO_EXECUTION;
break;

case TaskStatus.Running:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_RUNNING;
break;

case TaskStatus.WaitingForChildrenToComplete:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_WAITING_CHILDREN_COMPLETION;
break;

case TaskStatus.RanToCompletion:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_SUCCESSFUL;
break;

default:
displayStatusMsg = Text.LocalizedData.TASK_STATUS_FAILED;
break;
}

Text.PrintLine(false, displayStatusMsg);
}

/** <summary> Peforms an Action based on the type of the Path given. </summary>

<param name = "inputPath"> The Path where the FileSystem to be Processed is Located. </param>
<param name = "outputPath"> The Location where the resulting FileSystem will be Saved. </param>
<param name = "singleAction"> The single Action to be Performed. </param> 
<param name = "batchAction"> The Action to be Performed in Batches. </param> */

public static void PerformSystemAction(string inputPath, string outputPath, Action<string, string> singleAction, Action<string, string> batchAction)
{
FileAttributes pathType = Path_Helper.CheckPathType(inputPath);

if(pathType == FileAttributes.Archive)
singleAction(inputPath, outputPath);

else if(pathType == FileAttributes.Directory)
batchAction(inputPath, outputPath);

else
throw new IOException("Unknown Path Type");

}

/** <summary> Releases all the Resources that a Task Occupies. </summary>
<param name = "targetHandle"> The Handle of the Process to be Released. </param> */

public static void ReleaseTaskResources(IntPtr targetHandle) => SetProcessWorkingSetSize(targetHandle, minProcessSize, maxProcessSize);
}

}