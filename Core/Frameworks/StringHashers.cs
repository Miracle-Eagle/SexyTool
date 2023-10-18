using SexyTool.Program.Core.Functions.StringHashers;
using SexyTool.Program.Graphics.Dialogs;
using SexyTool.Program.Graphics.UserSelections;
using System;

namespace SexyTool.Program.Core.Frameworks
{
/// <summary> Performs functions Related to the <c>StringHashers</c> of this Program. </summary>

internal class StringHashers : Framework
{
/// <summary> Creates a new Instance of the <c>StringHashers</c>. </summary>

public StringHashers()
{;
ID = 3;
DisplayName = Text.LocalizedData.FRAMEWORK_STRING_HASHERS;

FunctionsList = new()
{
// Function A - MD5 Hasher: Hash String

new Function()
{
ID = ConsoleKey.A,
DisplayName = Text.LocalizedData.FUNCTION_HASH_STRING_MD5,
Process = () => Md5StringDigest.DisplayStringDigest(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DIGEST) as string, (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam() )
},

// Function B - SHA-1 Hasher: Hash String

new Function()
{
ID = ConsoleKey.B,
DisplayName = Text.LocalizedData.FUNCTION_HASH_STRING_SHA1,
Process = () => Sha1StringDigest.DisplayStringDigest(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DIGEST) as string, (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam() )
},

// Function C - SHA-256 Hasher: Hash String

new Function()
{
ID = ConsoleKey.C,
DisplayName = Text.LocalizedData.FUNCTION_HASH_STRING_SHA256,
Process = () => Sha256StringDigest.DisplayStringDigest(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DIGEST) as string, (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam() )
},

// Function D - SHA-384 Hasher: Hash String

new Function()
{
ID = ConsoleKey.D,
DisplayName = Text.LocalizedData.FUNCTION_HASH_STRING_SHA384,
Process = () => Sha384StringDigest.DisplayStringDigest(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DIGEST) as string, (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam() )
},

// Function B - SHA-512 Hasher: Hash String

new Function()
{
ID = ConsoleKey.E,
DisplayName = Text.LocalizedData.FUNCTION_HASH_STRING_SHA512,
Process = () => Sha512StringDigest.DisplayStringDigest(Interface.GetDialog<InputStringDialog>().Popup(Text.LocalizedData.ADVICE_ENTER_STRING_FOR_DIGEST) as string, (bool)Interface.GetUserSelection<DigestAuthModeSelection>().GetSelectionParam() )
}

};

}

}

}