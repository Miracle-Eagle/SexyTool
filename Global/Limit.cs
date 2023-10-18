using System;
using SexyTool.Program;

namespace SexyTool
{
/// <summary> Represents a Limit that a Input should follow. </summary>

public class Limit<T>
{
/** <summary> Gets the Minimum Value of a Limit. </summary>
<returns> The Minimum Value of a Limit. </returns> */

public T MinValue{ get; set; }

/** <summary> Gets the Maximum Value of a Limit. </summary>
<returns> The Maximum Value of a Limit. </returns> */

public T MaxValue{ get; set; }

/// <summary> Creates a new Instance of the Limit Class. </summary>

public Limit()
{
MinValue = default;
MaxValue = default;
}

/** <summary> Creates a new Instance of the Limit Class with the given Range. </summary>
<param name = "sourceRange"> The Range to be Applied. </param> */

public Limit(T sourceRange)
{
MinValue = sourceRange;
MaxValue = sourceRange;
}

/** <summary> Creates a new Instance of the Limit Class with the Specific Limitations. </summary>
<param name = "minRange"> The Minimum Range of the Limit. </param>

<param name = "maxRange"> The Maximum Range of the Limit. </param> */

public Limit(T minRange, T maxRange)
{
MinValue = minRange;
MaxValue = maxRange;
}

/** <summary> Checks if a Parameter is inside the Specified Range. </summary>
<param name = "userParam"> The Parameter to be Analized. </param>

<param name = "paramRange"> The Range to be Followed. </param> */

public T CheckParamRange(T userParam)
{
IComparable<T> comparableParam = userParam as IComparable<T>;

if(comparableParam == null)
userParam = default;

if(comparableParam.CompareTo(MinValue) < 0 || comparableParam.CompareTo(MaxValue) > 0)
userParam = MinValue;

return userParam;
}


/** <summary> Gets the Range of limitRange Instance. </summary>
<returns> The Range of limitRange Instance. </returns> */

public static Limit<T> GetLimitRange()
{
Type inputType = typeof(T);
string typeName = inputType.Name;

Limit<T> limitRange = typeName switch
{
"Boolean" => new Limit<T>( (T)(object)false, (T)(object)true),
"Byte" => new Limit<T>( (T)(object)byte.MinValue, (T)(object)byte.MaxValue),
"SByte" => new Limit<T>( (T)(object)sbyte.MinValue, (T)(object)sbyte.MaxValue),
"Int16" => new Limit<T>( (T)(object)short.MinValue, (T)(object)short.MaxValue),
"UInt16" => new Limit<T>( (T)(object)ushort.MinValue, (T)(object)ushort.MaxValue),
"Int32" => new Limit<T>( (T)(object)int.MinValue, (T)(object)int.MaxValue),
"UInt32" => new Limit<T>( (T)(object)uint.MinValue, (T)(object)uint.MaxValue),
"Int64" => new Limit<T>( (T)(object)long.MinValue, (T)(object)long.MaxValue),
"UInt64" => new Limit<T>( (T)(object)ulong.MinValue, (T)(object)ulong.MaxValue),
"Single" => new Limit<T>( (T)(object)float.MinValue, (T)(object)float.MaxValue),
"Double" => new Limit<T>( (T)(object)double.MinValue, (T)(object)double.MaxValue),
"DateTime" => new Limit<T>( (T)(object)DateTime.MinValue, (T)(object)DateTime.MaxValue),
_ => new Limit<T>()
};

return limitRange;
}

/** <summary> Shows the Range of a Value asked to the User to Enter. </summary>
<param name = "sourceRange"> The Range to be Displayed. </param> */

public void ShowInputRange() => Text.PrintDialog(true, string.Format(Text.LocalizedData.DIALOG_INPUT_RANGE, MinValue, MaxValue) );
}

}