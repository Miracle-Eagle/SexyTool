using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.IO;

namespace SexyTool.Program.Core
{
/// <summary> Initializes serializing Functions for JSON Data. </summary>

internal static class JSON_Serializer
{
/** <summary> Sets the Settings used for Parsing JSON Data. </summary>
<returns> The JSON Parse Settings. </returns> */	

private static readonly Action<Config> parseSettings = delegate(Config serialConfig)
{
serialConfig.ParsePrimitiveFloatingPointTypes = ParseAsType.Double;
serialConfig.TryParseIntoBestFit = true;
serialConfig.ExcludeTypeInfo = true;
serialConfig.Indent = true;
serialConfig.MaxDepth = 64;
serialConfig.EscapeUnicode = true;
};

/** <summary> Formats a JSON String by using Indented Formatting. </summary>

<param name = "sourceStr"> The JSON String to be Formatted. </param>

<returns> The Formatted JSON String. </returns> */

public static void FormatJsonString(ref string sourceStr)
{
Newtonsoft.Json.JsonSerializer jsonParser = new();
using StringReader strReader = new(sourceStr);

using JsonTextReader jsonReader = new(strReader);
object jsonObj = jsonParser.Deserialize(jsonReader);

if(jsonObj == null)
return;

else
{
StringWriter strWriter = new();

JsonTextWriter jsonWriter = new(strWriter)
{
Formatting = Formatting.Indented,
Indentation = 2,
IndentChar = '\t'
};

jsonParser.Serialize(jsonWriter, jsonObj);
sourceStr = strWriter.ToString();
}

}

/** <summary> Reads the Data of a JSON File. </summary>

<param name = "targetPath"> The Path where the JSON File to be Read is Located. </param>

<returns> The Data Read. </returns> */

public static JToken ReadJsonFile(string targetPath)
{
string jsonFileContent = File.ReadAllText(targetPath);

JsonLoadSettings jsonSettings = new()
{
CommentHandling = CommentHandling.Ignore,
LineInfoHandling = LineInfoHandling.Load,
DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Ignore
};

return JToken.Parse(jsonFileContent, jsonSettings);
}

/** <summary> Serializes a JSON String. </summary>

<param name = "targetStr"> The String to be Serialized. </param>

<returns> The String serialized. </returns> */

public static string SerializeString(string targetStr) => JsonConvert.SerializeObject(targetStr);

/** <summary> Serializes a Object as a JSON String. </summary>

<param name = "targetObject"> The Object to be Serialized. </param>

<returns> The Object serialized. </returns> */

public static string SerializeObject<T>(T targetObj) => targetObj.ToJson(parseSettings);

/** <summary> Deserializes a JSON String. </summary>

<param name = "targetStr"> The String to be Deserialized. </param>

<returns> The deserialized String. </returns> */

public static string DeserializeString(string targetStr) => JsonConvert.DeserializeObject(targetStr).ToString();

/** <summary> Deserializes a JSON String as a Object. </summary>

<param name = "targetStr"> The String to be Deserialized. </param>

<returns> The deserialized Object. </returns> */

public static T DeserializeObject<T>(string targetStr) => targetStr.FromJson<T>();
}

}