// Developer Express Code Central Example:
// How to fix a row in GridControl
// 
// This example demonstrates how to provide the capability to fix a row in
// GridControl. Our GridControl doesn't have the capability to fix a row. So, to
// provide this capability in this sample, we manipulate
// the child collection of
// the HeirarchyPanel. We add a StackPanel with a collection of editors to the
// child
// collection of the HeirarchyPanel and bind them to respective cells of the
// selected row. Then, we assign
// it to a ScrollChanged event of the ScrollViewer.
// When ScrollChanged raises, we change the location of the StackPanel, so it is
// always displayed on the top of the grid view.
// Please note, that this approach
// is not compatible with Bands and Conditional Formatting. Also, a fixed row may
// not be displayed immediately when PerPixelScrolling is enabled.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=T161325

// Developer Express Code Central Example:
// How to fix a row in GridControl
// 
// This example demonstrates how to provide the capability to fix a row in
// GridControl. Our GridControl doesn't have the capability to fix a row. So, to
// provide this capability in this sample, we manipulate
// the child collection of
// the HeirarchyPanel. We add a StackPanel with a collection of editors to the
// child
// collection of the HeirarchyPanel and bind them to respective cells of the
// selected row. Then, we assign
// it to a ScrollChanged event of the ScrollViewer.
// When ScrollChanged raises, we change the location of the StackPanel, so it is
// always displayed on the top of the grid view.
// Please note, that this approach
// is not compatible with Bands and Conditional Formatting. Also, a fixed row may
// not be displayed immediately when PerPixelScrolling is enabled.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=T161325

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("FixedRowExample")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("FixedRowExample")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
