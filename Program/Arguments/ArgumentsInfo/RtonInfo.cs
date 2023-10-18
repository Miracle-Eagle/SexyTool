using SexyTool.Program.Graphics.UserSelections;

namespace SexyTool.Program.Arguments
{
/// <summary> Groups Info related to the RTON Parser. </summary>

public class RtonInfo : ParamGroupInfo
{
/** <summary> Gets or Sets a Boolean that Determines if Reference Strings should be Used or not. </summary>
<returns> <b>true</b> if Reference Strings should be Use; otherwise, <b>false</b>. </returns> */

public bool UseReferenceStrings{ get; set; }

/// <summary> Creates a new Instance of the <c>RtonInfo</c> Class. </summary>

public RtonInfo()
{
UseReferenceStrings = false;
}

/** <summary> The logic to be Applied when the User selects an Argument from <c>RtonInfo</c>. </summary>
<param name = "paramName"> The Name of the Param selected. </param> */

protected override void ParamSelectionLogic(string paramName)
{

UseReferenceStrings = paramName switch
{
_ => (bool)Interface.GetUserSelection<Base64SecureModeSelection>().GetSelectionParam() // Pendant
};

}

/// <summary> Edits the Info given in the <c>RtonInfo</c> instance. </summary>

public override void EditGroupInfo()
{
ActionWrapper<string> selectionLogic = new( ParamSelectionLogic );
ComitChanges<RtonInfo>(selectionLogic.Init);
}

}

}