@echo off
echo [96mHello! This is our documentation generation tool.[0m 
echo [96mSetting up environment... (1/2)[0m
WHERE dotnet > nul 2>&1
IF %ERRORLEVEL% NEQ 0 (ECHO [91mdotnet command was not found. Install .NetCore SDK 2.1 from Microsoft! - www.dotnet.microsoft.com/download[0m 
PAUSE
EXIT)
dotnet tool install --global igloo15.MarkdownApi.Tool --version 1.0.5 > nul 2>&1
dotnet build ..\src\SigStat.Common\SigStat.Common.csproj --configuration Debug /p:WarningLevel=0 > nul 2>&1
rem The execution will fail at the first try
markdownapi ..\src\SigStat.Common\bin\Debug\net461\SigStat.Common.dll %cd%\md > nul 2>&1
echo [96mUpdating documentation has started... (2/2)[0m 
echo.
dotnet build ..\src\SigStat.Common\SigStat.Common.csproj --configuration Debug /p:WarningLevel=0
markdownapi ../src/SigStat.Common/bin/Debug/net461/SigStat.Common.dll --title="API Documentation" %cd%\md
dotnet tool uninstall --global igloo15.MarkdownApi.Tool > nul 2>&1
echo.
echo [96mThis is it! You can find the newly generated documentation at docs\md.[0m 
echo Credits: neuecc, jyasuu, Igloo15

PAUSE