@echo off

:: Compile
dotnet publish -r win-x64 --self-contained false -o bin/ -c Release

:: Folder structure
mkdir ".\bin\server"
mkdir ".\bin\client"
del ".\bin\*.deps.json"

:: Server files
copy ".\bin\EorzeansVoiceLib.*" ".\bin\server\"
copy ".\bin\EorzeansVoiceServer.*" ".\bin\server\"
copy ".\bin\Newtonsoft.Json.dll" ".\bin\server\"

:: Client files
copy ".\bin\EorzeansVoiceLib.*" ".\bin\client\"
copy ".\bin\EorzeansVoice.*" ".\bin\client\"
copy ".\bin\NAudio.*" ".\bin\client\"
copy ".\bin\Newtonsoft.Json.dll" ".\bin\client\"
copy ".\bin\opus.dll" ".\bin\client\"
copy ".\bin\POpusCodec.dll" ".\bin\client\"

:: Cleanup
del /q ".\bin\*"

:: TODO : Add Configuration files