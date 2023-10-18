using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a BlockCipher Name. </summary>

internal partial class BlockCipherNameSelection : CipherModeSelection
{
/// <summary> Creates a new Instance of the BlockCipherNameSelection. </summary>

public BlockCipherNameSelection()
{
selectionParams = new string[] { "CBC", "CFB", "OFB", "SIT" };
}

/** <summary> Displays the BlockCipherNameSelection. </summary>
<returns> The BlockCipher selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);

for(int i = 0; i < selectionParams.Length; i++)
Text.Print(true, "{0} ---> {1}", i, selectionParams[i] );

int nameIndex = -1;

while(nameIndex < 0 || nameIndex > selectionParams.Length - 1)
{
Text.PrintAdvice(false, "\r" + adviceText);
nameIndex = Input_Manager.FilterNumber<int>(Console.ReadLine() );
}

Text.PrintLine();
return selectionParams[nameIndex];
}

}

}