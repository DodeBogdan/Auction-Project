//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Transilvania University of Brasov">
//     Copyright (c) Brassoi Silvia Maria. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using log4net.Config;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AuctionLogic")]
[assembly: AssemblyDescription("Application for collage.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Transilvania University of Brasov")]
[assembly: AssemblyProduct("AuctionLogic")]
[assembly: AssemblyCopyright("Brassoi Silvia Maria")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the Id of the typelib if this project is exposed to COM
[assembly: Guid("8015a9b0-df30-4aa0-8414-0aa19503e3bb")]

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
[assembly: InternalsVisibleTo("Auction.Tests")]
[assembly: XmlConfigurator(Watch = false)]
