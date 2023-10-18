namespace SexyTool.Program.Graphics
{
/// <summary> Represents a Dialog of this Program. </summary>

public class Dialog : Graphics
{
/** <summary> Shows the Dialog on Screen. </summary>
<param name = "inputRange"> The Range which user Input must follow (Optional). </param>

<returns> The Type expected to be Entered by the user. </returns> */

public virtual object Popup(Limit<int> inputRange = null) => "Template1";

/** <summary> Shows the Dialog with the specified Advice and Body on Screen. </summary>
<param name = "sourceAdvice"> The Advice to Display on Screen. </param>

<param name = "inputRange"> The Range which user Input must follow (Optional). </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>

<returns> The Type expected to be Entered by the user. </returns> */

public virtual object Popup(string sourceAdvice, string sourceBody = default, Limit<int> inputRange = null) => "Template2";
}

}