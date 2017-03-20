@echo Starting to publish Aiursoft All...
@git status
@git pull

@cd AiursoftBase
REM @dotnet restore
@dotnet build
@cd ../

@cd Account
REM @dotnet restore
REM @dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd API
REM @dotnet restore
REM @dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd Developer
REM @dotnet restore
REM @dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd OSS
REM @dotnet restore
REM @dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd Wiki
REM @dotnet restore
REM @dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@echo Build and published finished!
@pause