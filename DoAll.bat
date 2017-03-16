@echo Starting to publish Aiursoft All...
@git status
@git pull

@cd AiursoftBase
@dotnet restore
@dotnet build
@cd ../

@cd Account
@dotnet restore
@dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd API
@dotnet restore
@dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd Developer
@dotnet restore
@dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd OSS
@dotnet restore
@dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@cd Wiki
@dotnet restore
@dotnet ef database update
@taskkill /IM dotnet.exe /F
@dotnet publish
@cd ../

@echo Build and published finished!
@pause