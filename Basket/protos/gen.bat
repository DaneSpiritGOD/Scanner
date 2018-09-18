setlocal

@rem enter this directory
cd /d %~dp0

set TOOLS_PATH=C:\Users\Dane\.nuget\packages\grpc.tools\1.14.1\tools\windows_x64

%TOOLS_PATH%\protoc.exe -I. --csharp_out=.. --grpc_out=.. --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe Mix.proto ImageFile.proto Image.proto SimpleRawImage.proto
endlocal
