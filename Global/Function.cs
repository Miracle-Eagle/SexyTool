using System;

/// <summary> Represents a Function that this Program can Perform. </summary>

public class Function
{
/** <summary> Gets or Sets the ID of a Function. </summary>
<returns> The ID of the Function. </returns> */

public ConsoleKey ID{ get; set; }

/** <summary> Gets or Sets the Display Name of a Function. </summary>
<returns> The Display Name of the Function. </returns> */

public string DisplayName{ get; set; }

/** <summary> Gets or Sets the Process of a Function. </summary>
<returns> The Process of the Function. </returns> */

public Action Process{ get; set; }

/// <summary> Creates a new Instance of the Function Class. </summary>

public Function()
{
ID = default;
DisplayName = string.Empty;

Process = null;
}

/** <summary> Creates a new Instance of the Function Class with the Specific ID. </summary>
<param name = "sourceID"> The ID of the Function. </param> */

public Function(ConsoleKey sourceID)
{
ID = sourceID;
DisplayName = string.Empty;

Process = null;
}

/** <summary> Creates a new Instance of the Function Class with the Specific Name. </summary>
<param name = "sourceName"> The Name of the Function. </param> */

public Function(string sourceName)
{
ID = default;
DisplayName = sourceName;

Process = null;
}

/** <summary> Creates a new Instance of the Function Class with the Specific Process. </summary>
<param name = "sourceProcess"> The Process of the Function. </param> */

public Function(Action sourceProcess)
{
ID = default;
DisplayName = string.Empty;

Process = sourceProcess;
}

/** <summary> Creates a new Instance of the Function Class with the Specific ID and Name. </summary>
<param name = "sourceID"> The ID of the Function. </param>

<param name = "sourceName"> The Name of the Function. </param> */

public Function(ConsoleKey sourceID, string sourceName)
{
ID = sourceID;
DisplayName = sourceName;

Process = null;
}

/** <summary> Creates a new Instance of the Function Class with the Specific ID, Name and Process. </summary>
<param name = "sourceID"> The ID of the Function. </param>

<param name = "sourceName"> The Name of the Function. </param>
<param name = "sourceProcess"> The Process of the Function. </param> */

public Function(ConsoleKey sourceID, string sourceName, Action sourceProcess)
{
ID = sourceID;
DisplayName = sourceName;

Process = sourceProcess;
}

}