
namespace SexyTool.Program.Core.Functions.SexyParsers.RealTimeObjectNotation.Definitions.Base
{
/// <summary> Represents a Class in the RtSystem. </summary>

public abstract class RtClass<T> : RtBaseType<T> where T : class
{
/** <summary> Gets a Value which Contains Info about the Size of a Class in the RtSystem. </summary>
<returns> The Size of the Class. </returns> */

protected int Size;
}

}