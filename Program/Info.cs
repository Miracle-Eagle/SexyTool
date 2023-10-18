using System;
using System.IO;
using System.Reflection;

namespace SexyTool.Program
{
/// <summary> Initializes Analisis Functions about the Assembly Info of this Program when its Being Compiled. </summary>

internal class Info
{
/** <summary> Gets Info about the Title of this Program. </summary>
<returns> The Title of the Program. </returns> */

public static string ProgramTitle
{

get
{
Type attributesType = typeof(AssemblyTitleAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute titleAttribute = customAttributes[0];

AssemblyTitleAttribute assemblyAttribute = (AssemblyTitleAttribute)titleAttribute;
return assemblyAttribute.Title;
}

}

/** <summary> Gets Info about the Description of this Program. </summary>
<returns> The Description of the Program. </returns> */

public static string ProgramDescription
{

get
{
Type attributesType = typeof(AssemblyDescriptionAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute descriptionAttribute = customAttributes[0];

AssemblyDescriptionAttribute assemblyAttribute = (AssemblyDescriptionAttribute)descriptionAttribute;
return assemblyAttribute.Description;
}

}

/** <summary> Gets Info about the Producer of this Program. </summary>
<returns> The Producer of the Program. </returns> */

public static string ProgramProducer
{

get
{
Type attributesType = typeof(AssemblyCompanyAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute companyAttribute = customAttributes[0];

AssemblyCompanyAttribute assemblyAttribute = (AssemblyCompanyAttribute)companyAttribute;
return assemblyAttribute.Company;
}

}

/** <summary> Gets Info about the Product of this Program. </summary>
<returns> The Product of the Program. </returns> */

public static string ProgramProduct
{

get
{
Type attributesType = typeof(AssemblyProductAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute productAttribute = customAttributes[0];

AssemblyProductAttribute assemblyAttribute = (AssemblyProductAttribute)productAttribute;
return assemblyAttribute.Product;
}

}

/** <summary> Gets Info about the Build Config of this Program. </summary>
<returns> The Build Config of the Program. </returns> */

public static string BuildConfig
{

get
{
Type attributesType = typeof(AssemblyConfigurationAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute buildConfigAttribute = customAttributes[0];

AssemblyConfigurationAttribute assemblyAttribute = (AssemblyConfigurationAttribute)buildConfigAttribute;
return assemblyAttribute.Configuration;
}

}

/** <summary> Gets Info about the Compile Version of this Program. </summary>
<returns> The Compile Version of the Program. </returns> */

public static Version CompileVersion
{

get
{
return Environment_Info.CurrentAssemblyVersion;
}

}

/** <summary> Gets Info about the Version of this Program. </summary>
<returns> The Version of Program. </returns> */

public static string ProgramVersion
{

get
{
Type attributesType = typeof(AssemblyFileVersionAttribute);
bool inheritAttributes = false;

Attribute[] customAttributes = Attribute.GetCustomAttributes(Environment_Info.CurrentAssembly, attributesType, inheritAttributes);
Attribute versionAttribute = customAttributes[0];

AssemblyFileVersionAttribute assemblyAttribute = (AssemblyFileVersionAttribute)versionAttribute;
return assemblyAttribute.Version;
}

}

/** <summary> Gets Info about the Last Compilation Time of this Program. </summary>
<returns> The Last Compilation Time of the Program. </returns> */

public static DateTime CompilationTime => File.GetLastWriteTime(Environment_Info.CurrentAssemblyLocation);
}

}