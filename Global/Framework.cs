using System.Collections.Generic;
using SexyTool.Program.Arguments;

/// <summary> Represents a Framework of this Program. </summary>

public class Framework
{
/** <summary> Gets or Sets the ID of a Framework. </summary>
<returns> The ID of the Framework. </returns> */

public ushort ID{ get; set; }

/** <summary> Gets or Sets the Display Name of a Framework. </summary>
<returns> The Display Name of the Framework. </returns> */

public string DisplayName{ get; set; }

/** <summary> Obtains or Creates a List of Functions from a Framework </summary>
<returns> The Functions List from the Framework. </returns> */

public List<Function> FunctionsList{ get; set; }

/// <summary> Creates a new Instance of the Framework Class. </summary>

public Framework()
{
ID = default;
DisplayName = string.Empty;

FunctionsList = new List<Function>();
}

/** <summary> Creates a new Instance of the Framework Class with the Specific ID. </summary>
<param name = "sourceID"> The ID of the Framework. </param> */

public Framework(ushort sourceID)
{
ID = sourceID;
DisplayName = string.Empty;

FunctionsList = new List<Function>();
}

/** <summary> Creates a new Instance of the Framework Class with the Specific Name. </summary>
<param name = "sourceName"> The Name of the Framework. </param> */

public Framework(string sourceName)
{
ID = default;
DisplayName = sourceName;

FunctionsList = new List<Function>();
}

/** <summary> Creates a new Instance of the Framework Class with the Specific Params. </summary>
<param name = "sourceParams"> The Params of the Framework. </param> */

public Framework(ArgumentsSet sourceParams)
{
ID = default;
DisplayName = string.Empty;

FunctionsList = new List<Function>();
}

/** <summary> Creates a new Instance of the Framework Class with the Specific Functions. </summary>
<param name = "sourceFunctions"> The Functions of the Framework. </param> */

public Framework(List<Function> sourceFunctions)
{
ID = default;
DisplayName = string.Empty;

FunctionsList = sourceFunctions;
}

/** <summary> Creates a new Instance of the Framework Class with the Specific ID and Name. </summary>
<param name = "sourceID"> The ID of the Function. </param>

<param name = "sourceName"> The Name of the Function. </param> */

public Framework(ushort sourceID, string sourceName)
{
ID = sourceID;
DisplayName = sourceName;

FunctionsList = new List<Function>();
}

/** <summary> Creates a new Instance of the Framework Class with the Specific ID, Name, Params and Functions. </summary>
<param name = "sourceID"> The ID of the Function. </param>

<param name = "sourceName"> The Name of the Function. </param>
<param name = "sourceFunctions"> The Functions of the Framework. </param> */

public Framework(ushort sourceID, string sourceName, List<Function> sourceFunctions)
{
ID = sourceID;
DisplayName = sourceName;

FunctionsList = sourceFunctions;
}

}