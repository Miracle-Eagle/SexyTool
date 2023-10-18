using SexyTool.Program.Core;
using System;
using System.Reflection;

namespace SexyTool.Program.Graphics
{
/// <summary> Represents a user Selection. </summary>

public class UserSelection : Graphics
{
/** <summary> Stores an Array of Objects that represents the Parameters the user can Choose on a Selection. </summary>
<returns> The Selection Params. </returns> */

protected object[] selectionParams;

/** <summary> Displays the Parameters of a user Selection. </summary>
<returns> The Selection made by the User. </returns> */

public virtual object GetSelectionParam() => "Template1";

/** <summary> Displays the Parameters of a user Selection with the Specified Limit. </summary>
<returns> The Selection made by the User. </returns> */

public virtual object GetSelectionParam(Limit<int> selectionRange) => "Template2";

/** <summary> Displays the Members of a Class. </summary>

<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "memAttributes"> The Attributes of the Members to be Displayed. </param> */

protected void DisplayClassMembers<T>(out T expectedValue, BindingFlags memAttributes)
{
Type instanceType = typeof(T);
MemberInfo[] classMembers = instanceType.GetMembers(memAttributes);

for(int i = 0; i < classMembers.Length; i++)
Text.Print(true, "{0} ---> {1}", i, classMembers[i].Name);

int memberIndex = -1;

while(memberIndex < 0 || memberIndex > classMembers.Length - 1)
{
Text.PrintAdvice(false, "\r" + adviceText);
memberIndex = Input_Manager.FilterNumber<int>(Console.ReadLine() );
}

Text.PrintLine();
MemberInfo selectedMember = classMembers[memberIndex];

expectedValue = GetMemberInstance<T>(selectedMember);
}

/** <summary> Displays the Options of a Enum. </summary>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param> */

protected void DisplayEnumOptions<T>(out T expectedValue) where T : Enum
{
Type enumType = typeof(T);
Array enumFlags = Enum.GetValues(enumType);

for(int i = 0; i < enumFlags.Length; i++)
Text.Print(true, "{0} ---> {1}", i, enumFlags.GetValue(i) );

int enumIndex = -1;

while(enumIndex < 0 || enumIndex > enumFlags.Length - 1)
{
Text.PrintAdvice(false, "\r" + adviceText);
enumIndex = Input_Manager.FilterNumber<int>(Console.ReadLine() );
}

Text.PrintLine();
expectedValue = (T)enumFlags.GetValue(enumIndex);
}

/** <summary> Gets an Instance from a Member. </summary>

<param name = "sourceMember"> The Member where the Instance will be Obtained from. </param>

<returns> The Member Intance. </returns> */

private static T GetMemberInstance<T>(MemberInfo sourceMember)
{
T defaultValue = default;
object memberInstance;

if(sourceMember is PropertyInfo aProperty)
memberInstance = aProperty.GetValue(defaultValue);

else if(sourceMember is FieldInfo aField)
memberInstance = aField.GetValue(defaultValue);

else if(sourceMember is MethodInfo aMethod)
memberInstance = aMethod.GetMethodBody();

else
memberInstance = defaultValue;

return (T)memberInstance;
}

}

}