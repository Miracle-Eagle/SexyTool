using SexyTool.Program.Core;
using System;

namespace SexyTool.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a CipherPadding. </summary>

internal partial class CipherPaddingSelection : PaddingModeSelection
{
/// <summary> Creates a new Instance of the CipherPaddingSelection. </summary>

public CipherPaddingSelection()
{
selectionParams = new string[] { "ZeroByte Padding", "ISO7816d4 Padding", "PKCS7 Padding", "TBC Padding", "X923 Padding" };
}

/** <summary> Displays the BlockCipherSelection. </summary>
<returns> The BlockCipher selected by the User. </returns> */

public override object GetSelectionParam()
{
Text.PrintHeader(headerText);

for(int i = 0; i < selectionParams.Length; i++)
Text.Print(true, "{0} ---> {1}", i, selectionParams[i] );

int paddingIndex = -1;

while(paddingIndex < 0 || paddingIndex > selectionParams.Length - 1)
{
Text.PrintAdvice(false, "\r" + adviceText);
paddingIndex = Input_Manager.FilterNumber<int>(Console.ReadLine() );
}

Text.PrintLine();
return paddingIndex;
}

}

}