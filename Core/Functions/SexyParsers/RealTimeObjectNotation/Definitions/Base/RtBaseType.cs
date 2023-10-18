namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base
{
/// <summary> Represents a Type in the RtSystem. </summary>

public abstract class RtBaseType<T>
{
/** <summary> Gets a Value which Contains Info about the Identifier of a Type. </summary>
<returns> The Identifier of the Type. </returns> */

protected RtTypeIdentifier Identifier;

/** <summary> Gets or Sets a Value which Contains Info about the Value which a Type holds. </summary>
<returns> The Value of the Type. </returns> */

public T Value{ get; set; }
}

}