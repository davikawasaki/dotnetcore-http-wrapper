@echo off
setlocal

REM Script name: backupCSharpOldPackages
REM Version:      1.1.0
REM Description:  Backup previous version nuget file and compiled archives before actually building the new version
REM Requirements: Custom property item <PreviousVersion> in a <PropertyGroup> inside project .csproj file
REM @TODO: Error handling
REM @TODO: Unit testing 

REM ------- ARGS NECESSARY TO RUN SCRIPT (NEEDS TO BE SENT IN ORDER) -------
REM %~1 as SOLUTION FOLDER: $(SolutionDir)
REM %~2 as PROJECT FOLDER: CustomHttpWrapperLibrary\
REM %~3 as RELEASE OUTPUT: bin\Release\
REM %~4 as TARGET OUTPUT FOLDER: $(TargetFramework) Ex. netstandard2.0
REM %~5 as DESTINATION COPY PATH: Ex. Versions\
REM %~6 as PREVIOUS PACKAGE VERSION: $(PreviousVersion) Ex. 0.1.0
REM CMD prompt command line example:
REM "backupCSharpOldPackages.bat" $(SolutionDir) "CustomHttpWrapperLibrary\" "bin\Release\" $(TargetFramework) "Versions\" $(PreviousVersion)

REM Delete (if exists) a previous version backup (sub)folder(s) and inner files
REM rd \s \q $(SolutionDir)CustomHttpWrapperLibrary\Versions\$(PreviousVersion)

IF EXIST "%~1%~2%~5%~6" del /f /s /q "%~1%~2%~5%~6" 1>nul
IF EXIST "%~1%~2%~5%~6" rd /s /q "%~1%~2%~5%~6"

REM Create (if exists) a previous version backup folder
REM mkdir $(SolutionDir)CustomHttpWrapperLibrary\Versions\$(PreviousVersion)
IF NOT EXIST "%~1%~2%~5%~6" mkdir "%~1%~2%~5%~6"

REM Copy (if origin folder exists) the target framework folder to the backup versions folder
REM echo D | xcopy $(SolutionDir)CustomHttpWrapperLibrary\bin\Release\$(TargetFramework)\ $(SolutionDir)CustomHttpWrapperLibrary\Versions\$(PreviousVersion)\$(TargetFramework)\
IF EXIST "%~1%~2%~3%~4" echo D | xcopy "%~1%~2%~3%~4" "%~1%~2%~5%~6\%~4" /e /i

REM Copy the nuget package version file (if exists) to the versions' backup specific version folder
REM REM copy $(SolutionDir)CustomHttpWrapperLibrary\bin\Release\*$(PreviousVersion).nupkg $(SolutionDir)CustomHttpWrapperLibrary\Versions\$(PreviousVersion)
IF EXIST "%~1%~2%~3*%~6.nupkg" copy "%~1%~2%~3*%~6.nupkg" "%~1%~2%~5%~6\"

REM Delete (if exists) the nuget package version file
REM del $(ProjectDir)\bin\Release\*.nupkg
IF EXIST "%~1%~2%~3*%~6.nupkg" del /f "%~1%~2%~3*%~6.nupkg"