using System;

/// <summary> Serves as a Wrapper for each Action on a Method. </summary>

public class ActionWrapper
{
/** <summary> Gets or Sets the inner Action of this Class. </summary>
<returns> The inner Action of this Class. </returns> */

private readonly Action innerAction;

/// <summary> Creates a new Instance of the ActionWrapper Class. </summary>

public ActionWrapper()
{
innerAction = default;
}

/** <summary> Creates a new Instance of the ActionWrapper Class with the Specified Action. </summary>
<param name = "sourceAction"> The Action to be Wrapped. </param> */

public ActionWrapper(Action sourceAction)
{
innerAction = sourceAction;
}

/// <summary> Initializes the Action of this Instance. </summary>

public void Init() => innerAction();
}

/** <summary> Serves as a Wrapper for each Action on a Method. </summary>
<remarks> This Class admits one Type as a Parameter. </remarks> */

public class ActionWrapper<T>
{
/** <summary> Gets or Sets the inner Action of this Class. </summary>
<returns> The inner Action of this Class. </returns> */

private readonly Action<T> innerAction;

/// <summary> Creates a new Instance of the ActionWrapper Class. </summary>

public ActionWrapper()
{
innerAction = default;
}

/** <summary> Creates a new Instance of the ActionWrapper Class with the Specified Action. </summary>
<param name = "sourceAction"> The Action to be Wrapped. </param> */

public ActionWrapper(Action<T> sourceAction)
{
innerAction = sourceAction;
}

/** <summary> Initializes the Action of this Instance with the given Parameter. </summary>
<param name = "sourceParams"> The Parameter from which the Action will be Initialized. </param> */

public void Init(T sourceParam) => innerAction(sourceParam);
}

/** <summary> Serves as a Wrapper for each Action on a Method. </summary>
<remarks> This Class admits two Types as a Sequence of Parameters. </remarks> */

public class ActionWrapper<T1, T2>
{
/** <summary> Gets or Sets the inner Action of this Class. </summary>
<returns> The inner Action of this Class. </returns> */

private readonly Action<T1, T2> innerAction;

/// <summary> Creates a new Instance of the ActionWrapper Class. </summary>

public ActionWrapper()
{
innerAction = default;
}

/** <summary> Creates a new Instance of the ActionWrapper Class with the Specified Action. </summary>
<param name = "sourceAction"> The Action to be Wrapped. </param> */

public ActionWrapper(Action<T1, T2> sourceAction)
{
innerAction = sourceAction;
}

/** <summary> Initializes the Action of this Instance with the given Parameters. </summary>
<param name = "sourceParamX"> The first Parameter from which the Action will be Initialized. </param> 

<param name = "sourceParamY"> The second Parameters from which the Action will be Initialized. </param>  */

public void Init(T1 sourceParamX, T2 sourceParamY)
{
innerAction(sourceParamX, sourceParamY);
}

}

/** <summary> Serves as a Wrapper for each Action on a Method. </summary>
<remarks> This Class admits three Types as a Sequence of Parameters. </remarks> */

public class ActionWrapper<T1, T2, T3>
{
/** <summary> Gets or Sets the inner Action of this Class. </summary>
<returns> The inner Action of this Class. </returns> */

private readonly Action<T1, T2, T3> innerAction;

/// <summary> Creates a new Instance of the ActionWrapper Class. </summary>

public ActionWrapper()
{
innerAction = default;
}

/** <summary> Creates a new Instance of the ActionWrapper Class with the Specified Action. </summary>
<param name = "sourceAction"> The Action to be Wrapped. </param> */

public ActionWrapper(Action<T1, T2, T3> sourceAction)
{
innerAction = sourceAction;
}

/** <summary> Initializes the Action of this Instance with the given Parameters. </summary>
<param name = "sourceParamX"> The first Parameter from which the Action will be Initialized. </param> 

<param name = "sourceParamY"> The second Parameter from which the Action will be Initialized. </param>
<param name = "sourceParamZ"> The third Parameter from which the Action will be Initialized. </param> */

public void Init(T1 sourceParamX, T2 sourceParamY, T3 sourceParamZ)
{
innerAction(sourceParamX, sourceParamY, sourceParamZ);
}

}