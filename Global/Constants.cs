/// <summary> Serves as a Reference to Values that never Change on Code. </summary> 

public static class Constants
{
/** <summary> Sets a Value which Contains Info about the Factor used for Caculating the Informatic Units. </summary>
<returns> The Standard Value for Informatic Units. </returns> */

private const long SIZE_FACTOR = 1024;


#region Int32 Constants START

/** <summary> Sets a Value which Contains Info about the Number of Milliseconds a Second has. </summary>
<returns> The Number of Milliseconds a Second has. </returns> */

public const int MILLISECONDS = 1000;

/** <summary> Sets a Value which Contains Info about the Number of Seconds a Minute has. </summary>
<returns> The Number of Seconds a Minute has. </returns> */

public const int SECONDS = 60;

/** <summary> Sets a Value which Contains Info about the Number of Minutes an Hour has. </summary>
<returns> The Number of Minutes an Hour has. </returns> */

public const int MINUTES = 60;


#endregion Int32 Constants END


#region Int64 Constants START

/** <summary> Sets a Value which Contains Info about the Value of one Byte. </summary>
<returns> The Value of one Byte. </returns> */

public const long ONE_BYTE = 1;

/** <summary> Sets a Value which Contains Info about the Value of one Kilobyte. </summary>
<returns> The Value of one Kilobyte. </returns> */

public static readonly long ONE_KILOBYTE = ONE_BYTE * SIZE_FACTOR;

/** <summary> Sets a Value which Contains Info about the Value of one Megabyte. </summary>
<returns> The Value of one Megabyte. </returns> */

public static readonly long ONE_MEGABYTE = ONE_KILOBYTE * SIZE_FACTOR;

/** <summary> Sets a Value which Contains Info about the Value of one Gigabyte. </summary>
<returns> The Value of one Gigabyte. </returns> */

public static readonly long ONE_GIGABYTE = ONE_MEGABYTE * SIZE_FACTOR;


#endregion Int64 Constants END
}