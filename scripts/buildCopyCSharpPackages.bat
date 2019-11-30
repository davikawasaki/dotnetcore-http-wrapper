@echo off
setlocal

REM Script name: buildCopyCSharpPackages
REM Version:      1.1.0
REM Description:  After process is built, installs the nupkg output file, copy other remaining output files and copy the generated docs to version docs folder
REM Requirements: Custom property item <NuGetCustomPath> in a <PropertyGroup> inside project .csproj file
REM @TODO: Error handling
REM @TODO: Unit testing 

REM ------- ARGS NECESSARY TO RUN SCRIPT (NEEDS TO BE SENT IN ORDER) -------
REM ------------------------------------------------------------------------
REM WANT TO USE MORE THAN 9 ARGS? CHECK THIS:
REM https://stackoverflow.com/questions/8328338/how-do-you-utilize-more-than-9-arguments-when-calling-a-label-in-a-cmd-batch-scr
REM ------------------------------------------------------------------------
REM %~1 as SOLUTION FOLDER: $(SolutionDir)
REM %~2 as PROJECT FOLDER: CustomHttpWrapperLibrary
REM %~3 as RELEASE OUTPUT: bin\Release\
REM %~4 as TARGET OUTPUT FOLDER: $(TargetFramework) Ex. netstandard2.0
REM %~5 as DESTINATION COPY PATH: Ex. <LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\
REM %~6 as PACKAGE VERSION: $(Version) Ex. 0.2.0
REM %~7 as DESTINATION DOCS COPY PATH: docs\versions\
REM %~8 as NUGET CUSTOM PATH: C:\Users\davi.kawasaki\Programs\NuGet
REM %~9 as NUGET CUSTOM SOURCE SHARED FOLDER: <LOCAL_FEED_REPO>
REM CMD prompt command line example:
REM "buildCopyCSharpPackages.bat" $(SolutionDir) "CustomHttpWrapperLibrary\" "bin\Release\" $(TargetFramework) "<LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\" $(Version) "docs\versions\" <LOCAL_FEED_REPO> CustomHttpWrapperLibrary

REM REM Set nuget path if the argument path exists
echo "Starting post-build after packing..."
IF EXIST %~8 SET PATH=%PATH%;%~8
IF EXIST %~8 IF EXIST %~5%~6 echo Y | nuget delete %~2 %~6 -Source %~9
IF EXIST "%~1%~2\%~3%~2.%~6.nupkg" echo "%~1%~2\%~3%~2.%~6.nupkg"
IF EXIST %~8 IF EXIST "%~1%~2\%~3%~2.%~6.nupkg" nuget add "%~1%~2\%~3%~2.%~6.nupkg" -Source %~9

REM REM Delete (if exists) package version folder from nuget custom source in the shared folder
REM REM @Deprecated
REM REM rd \s \q <LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\$(Version)
REM IF EXIST "%~5%~6" del /f /s /q "%~5%~6" 1>nul
REM IF EXIST "%~5%~6" rd \s \q "%~5%~6"

REM REM Create (if exists) package version folder from nuget custom source in the shared folder
REM REM @Deprecated
REM mkdir <LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\$(Version)
REM mkdir "%~5%~6"

REM REM Copy (if exists) target framework version folder to the library nuget custom source version folder
REM REM echo D | xcopy $(SolutionDir)CustomHttpWrapperLibrary\bin\Release\$(TargetFramework) <LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\$(Version)\$(TargetFramework)
IF EXIST "%~1%~2\%~3%~4" echo D | xcopy "%~1%~2\%~3%~4" "%~5%~6\%~4" /e /i

REM REM Copy (if exists) nuget package file to the nuget custom source in the shared folder
REM REM @Deprecated
REM REM copy $(SolutionDir)CustomHttpWrapperLibrary\bin\Release\*$(Version).nupkg <LOCAL_FEED_REPO>\CustomHttpWrapperLibrary\$(Version)
REM IF EXIST "%~1%~2\%~3*%~6.nupkg" copy "%~1%~2\%~3*%~6.nupkg" "%~5%~6"

REM REM Create (if not exists) the docs version folder inside the docs folder
REM REM mkdir $(SolutionDir)docs\versions\$(Version)
IF NOT EXIST "%~1%~7%~6" mkdir "%~1%~7%~6"

REM REM Copy (if exists the destination folder) the docs version folder and maintain the last version docs into docs root folder
REM REM copy $(SolutionDir)CustomHttpWrapperLibrary\*.xml $(SolutionDir)docs\versions\$(Version)\
IF EXIST "%~1%~7%~6\" copy "%~1%~2\*.xml" "%~1%~7%~6\"
IF EXIST "%~1%~7%~6\" copy "%~1%~2\*.md" "%~1%~7%~6\"
IF EXIST "%~1%~7%~6\" copy "%~1*.md" "%~1%~7%~6\"
IF EXIST "%~1docs\" copy "%~1%~2\%~2.md" "%~1%docs\"