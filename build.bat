@echo off

:: Compile
cd EorzeansVoice
dotnet publish -r win-x64 --self-contained false -o ../bin/client/ -c Release
cd ../EorzeansVoiceServer
dotnet publish -r win-x64 --self-contained false -o ../bin/server/win -c Release
dotnet publish -r linux-x64 --self-contained true -o ../bin/server/linux -c Release
cd ../

:: Unused files
del ".\bin\client\*.deps.json"
del ".\bin\server\win\*.deps.json"
del ".\bin\server\linux\*.deps.json"

:: TODO : Add Configuration files