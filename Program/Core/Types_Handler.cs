using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SexyTool.Program.Core
{
/// <summary> Initializes Handling Functions for any Object Type on C# by using Reflection. </summary>

internal static class Types_Handler
{
/** <summary> Gets a List of Classes from the Specific Namespace. </summary>

<param name = "sourceNamespace"> The Namespace where the Class List will be Obtained. </param>

<returns> The Classes of the Namespace. </returns> */

public static List<T> GetClassList<T>(string sourceNamespace)
{
List<T> classList = new();

foreach(Type classType in Environment_Info.CurrentAssemblyTypes)
{
Attribute autoGeneratedClass = classType.GetCustomAttribute(typeof(CompilerGeneratedAttribute) );

if(classType.Namespace == sourceNamespace && autoGeneratedClass == null)
classList.Add( (T)Activator.CreateInstance(classType) );

}

return classList;
}

/** <summary> Gets a Map of Classes from the Specific Namespace, including their Name and Instance. </summary>

<param name = "sourceNamespace"> The Namespace where the Class Map will be Obtained. </param>

<returns> The Classes of the Namespace. </returns> */

public static Dictionary<string, T> GetClassMap<T>(string sourceNamespace)
{
Dictionary<string, T> classMap = new();

foreach(Type classType in Environment_Info.CurrentAssemblyTypes)
{
Attribute autoGeneratedClass = classType.GetCustomAttribute(typeof(CompilerGeneratedAttribute) );

if(classType.Namespace == sourceNamespace && autoGeneratedClass == null)
classMap.Add(classType.Name, (T)Activator.CreateInstance(classType) );

}

return classMap;
}

}

}