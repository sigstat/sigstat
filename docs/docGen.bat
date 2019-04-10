@echo off
echo Hello! This is our documentation generation tool. 
echo Setting up environment... (1/2)
dotnet tool install --global igloo15.MarkdownApi.Tool > nul 2>&1
dotnet build ..\src\SigStat.Common\SigStat.Common.csproj --configuration Debug /p:WarningLevel=0 > nul 2>&1
markdownapi ..\src\SigStat.Common\bin\Debug\net461\SigStat.Common.dll %cd%\md > nul 2>&1
echo Updating documentation has started... (2/2)

dotnet build ..\src\SigStat.Common\SigStat.Common.csproj --configuration Debug /p:WarningLevel=0
markdownapi ..\src\SigStat.Common\bin\Debug\net461\SigStat.Common.dll %cd%\md
dotnet tool uninstall --global igloo15.MarkdownApi.Tool > nul 2>&1

echo This is it! You can find the newly generated documentation at docs\md. Credits: neuecc, jyasuu, Igloo15

PAUSE