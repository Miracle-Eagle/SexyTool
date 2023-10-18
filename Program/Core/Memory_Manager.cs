using System;
using System.Diagnostics;
using System.IO;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Handling Functions for the Memory Consumed by the Process of this Program. </summary>

internal static class Memory_Manager
{
/// <summary> Releases the memory Consumed by a Process that its being Executed by this Program. </summary>

public static void ReleaseMemory()
{
GC.Collect();
GC.WaitForPendingFinalizers();

if(Environment_Info.OS_Version.Platform != PlatformID.Win32NT)
throw new PlatformNotSupportedException();

Process currentProcess = Process.GetCurrentProcess();
Task_Manager.ReleaseTaskResources(currentProcess.Handle);
}

}

}