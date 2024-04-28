# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/P3RPC.Utils.CalendarFunctions.Reloaded/*" -Force -Recurse
dotnet publish "./P3RPC.Utils.CalendarFunctions.Reloaded.csproj" -c Release -o "$env:RELOADEDIIMODS/P3RPC.Utils.CalendarFunctions.Reloaded" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location