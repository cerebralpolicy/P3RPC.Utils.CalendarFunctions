# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/p3rpc.nativetypes.calendarfunctions.reloaded/*" -Force -Recurse
dotnet publish "./p3rpc.nativetypes.calendarfunctions.reloaded.csproj" -c Release -o "$env:RELOADEDIIMODS/p3rpc.nativetypes.calendarfunctions.reloaded" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location