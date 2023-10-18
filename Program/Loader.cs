using SexyTool.Program.Core;
using SexyTool.Program.Graphics.Menus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyTool.Program
{
/// <summary> Initializes Loading Functions for this Program. </summary>

internal class Loader
{
/** <summary> Gets a List of Frameworks from this Program. </summary>
<returns> The List of Frameworks from this Program. </returns> */

public static List<Framework> appFrameworks = Types_Handler.GetClassList<Framework>("SexyTool.Program.Core.Frameworks").OrderBy(framework => framework.ID).ToList();

/** <summary> Loads the Functions of this Program when its being Launched on its Normal Mode. </summary>
<param name = "targetFramework"> The Framework selected by the User. </param> */

public static Task NormalLoad(Framework targetFramework)
{
FunctionsMenu functionSelector = Interface.GetMenu<FunctionsMenu>();
functionSelector.UpdateMainFramework(ref targetFramework);

Function selectedFunction = functionSelector.DynamicSelection() as Function;
return new Task(selectedFunction.Process);
}

// Add QuickLoad Method here...
}

}