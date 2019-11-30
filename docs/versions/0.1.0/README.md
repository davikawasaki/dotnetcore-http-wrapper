# .NET Core 2.2 HTTP Wrapper Library

This repository covers .NET Core 2.2 HTTP-related modules and classes libraries that are used in other projects.

Want to start contributing? Read the [Build Process](#build-process), [How to Use In Projects](#how-to-use-in-projects) and [Documentation](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/docs/CustomHttpWrapperLibrary.md) sections **before anything**!

## Table of Contents
---

- [Libraries Dependencies](#libraries-dependencies)
- [Versions](#versions)
- [Build Process](#build-process)
- [How to Use In Projects](#how-to-use-in-projects)
- [Changelog](#changelog)
- [TBD](#tbd)
- [Testing](#testing)
- [Documentation](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/docs/CustomHttpWrapperLibrary.md)

## Libraries Dependencies
---

- ClosedXML 0.94.2
- Vsxmd 1.4.1

## Versions
---

All compiled versions of this library is released as a NuGet .NET library project package, which contains the mapping of methods and the respective assembly binary file.

In order to get the output NuGet and its related files, clone this repository and build the project. Release files and the NuGet package will be found at **CustomHttpWrapperLibrary\bin\Release** folder.

Details of build process can be found at [build process topic](#build-process).

## Build Process
---

### Windows

The build process of this library uses two batches processes for a pre-build and a post-build, all in order to reinstall the library locally using a **[local feed repository](https://medium.com/@churi.vibhav/creating-and-using-a-local-nuget-package-repository-9f19475d6af8)**. The basic process can be seen in [CustomHttpWrapperLibrary.csproj](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/CustomHttpWrapperLibrary/CustomHttpWrapperLibrary.csproj) file in the following lines:

```
<PropertyGroup>
    <PostBuildEventDependsOn>
        $(PostBuildEventDependsOn);
        PostBuildMacros;
    </PostBuildEventDependsOn>
</PropertyGroup>

<Target Name="PreBuild" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\backupCSharpOldPackages.bat&quot; $(SolutionDir) CustomHttpWrapperLibrary\ bin\Release\ $(TargetFramework) Versions\ $(PreviousVersion)" />
</Target>

<Target Name="PostBuild" AfterTargets="Pack" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\buildCopyCSharpPackages.bat&quot; $(SolutionDir) $(ProjectName) bin\Release\ $(TargetFramework) &quot;<LOCAL_FEED_REPOSITORY_FOLDER>\CustomHttpWrapperLibrary\&quot; $(Version) docs\versions\ $(NuGetCustomPath) <LOCAL_FEED_REPOSITORY_FOLDER>" />
</Target>
```

The steps are the following:

1\. Go to [CustomHttpWrapperLibrary.csproj](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/CustomHttpWrapperLibrary/CustomHttpWrapperLibrary.csproj) file and change the <PropertyGroup> item called <NuGetCustomPath> for the **NuGet location path installed in your machine**. If you can't find it, download a [Portable version](https://www.nuget.org/downloads) and save in a folder you can rely on, which will demand you to edit the NuGet location path again in the .csproj.

2\. Before building any **release** version, a backup version of the previous version is made automatically through a [backupCSharpOldPackages.bat file](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/scripts/backupCSharpOldPackages.bat). Using a property *<PreviousVersion>* in the main .csproj file, all release files are copied to a /Versions/X.X.X folder inside CustomHttpWrapperLibrary/ folder (not synced with Git). For more details, check the [backupCSharpOldPackages.bat file](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/scripts/backupCSharpOldPackages.bat).

```
<Target Name="PreBuild" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\backupCSharpOldPackages.bat&quot; $(SolutionDir) CustomHttpWrapperLibrary\ bin\Release\ $(TargetFramework) Versions\ $(PreviousVersion)" />
</Target>
```

3\. After that, select the configuration environment option as *Release* (in the toolbar, near *Any CPU* and *Start*)

4\. Go to the option *Build* in the tabs toolbar and select the option *Build CustomHttpWrapperLibrary*

5\. After the nuget build/packing step, a post-build event is triggered, which calls the [buildCopyCSharpPackages.bat script](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/scripts/buildCopyCSharpPackages.bat). Using the change property *<NuGetCustomPath>* in the main .csproj file, this batch script (re)installs the nuget package version to a local feed repository (<LOCAL_FEED_REPOSITORY_FOLDER>) and then copies the standard2.0/ folder from release to the local feed repository in the specific version created folder for the library. After that, the generated XML/Markdown code documentation is copied to the docs version folder docs/Versions/X.X.X/. 

```
<Target Name="PostBuild" AfterTargets="Pack" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\buildCopyCSharpPackages.bat&quot; $(SolutionDir) $(ProjectName) bin\Release\ $(TargetFramework) &quot;<LOCAL_FEED_REPOSITORY_FOLDER>\CustomHttpWrapperLibrary\&quot; $(Version) docs\versions\ $(NuGetCustomPath) <LOCAL_FEED_REPOSITORY_FOLDER>" />
</Target>
```

### Linux

TBD/TBT. The goal is to test the following build steps :

```bash
# Make sure you have .NET Core 2.2 version installed
dotnet build -c Release  # Debug is building properly, but Release has to override .bat execs
dotnet pack
```

## How to Use In Projects
---

1\. Since this is a private package, you won't find it in nuget public repository. The way it's being used is through local feed repositories. To configure the local feed repository in Visual Studio, go through the following steps **(on Windows)**:

- Go to Tools > NuGet Package Manager > Package Manager Settings. In there add the folder you will store your local NuGet package repositories.
    
- Go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution. Select the local drive you setted and install the CustomHttpWrapperLibrary in your project.

2\. After installation of the package and respective nested ones, import any of the desired folders below into your project:

```csharp
using CustomHttpWrapperLibrary.ExcelService.Creation;
using CustomHttpWrapperLibrary.ExcelService.Search;
using CustomHttpWrapperLibrary.ExcelService.Manipulation;
```

Methods documentation for each one of the folders can be found with CustomHttpWrapperLibrary [Markdown file inside docs folder](https://github.com/davikawasaki/dotnetcore-http-wrapper/blob/master/docs/CustomHttpWrapperLibrary.md).

## Changelog
---

- Beta 0.1.0 [22/05/19]: Organization for Excel methods using ClosedXML library.

## TBD
---

1\. Implement proper unit tests

2\. Implement methods for Word and Powerpoint