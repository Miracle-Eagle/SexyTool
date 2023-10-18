using SexyTool.Program.Graphics.UserSelections;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to the Base64 Parser. </summary>

public class Base64Info : ParamGroupInfo
{
/** <summary> Gets or Sets a Boolean that Determines how Base64 strings should be Generated. </summary>
<returns> <b>true</b> if Base64 Strings should be threated as WebSafe Strings; otherwise, returns <b>false</b>. </returns> */

public bool IsWebSafe{ get; set; }

/// <summary> Creates a new Instance of the <c>Base64Info</c> Class. </summary>

public Base64Info()
{
IsWebSafe = true;
}

/** <summary> The logic to be Applied when the User selects an Argument from <c>Base64Info</c>. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected override void ParamSelectionLogic(string paramName)
{

IsWebSafe = paramName switch
{
_ => (bool)Interface.GetUserSelection<Base64SecureModeSelection>().GetSelectionParam()
};

}

/// <summary> Edits the Info given in the 'Base64Info' instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<Base64Info>(selectionLogic.Init);
}

}

}