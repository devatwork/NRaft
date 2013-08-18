@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

set nuget=
if "%nuget%" == "" (
    set nuget=.nuget\NuGet.exe
)

set nunit="packages\NUnit.Runners.2.6.2\tools\nunit-console.exe"

echo Update self %nuget%
%nuget% update -self
if %errorlevel% neq 0 goto failure

echo Restore packages
%nuget% install ".nuget\packages.config" -OutputDirectory packages -NonInteractive
if %errorlevel% neq 0 goto failure

echo Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild NRaft.sln /t:Rebuild /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
if %errorlevel% neq 0 goto failure

echo Unit tests
%nunit% NRaft.Core.Tests\bin\NRaft.Core.Tests.dll /framework:net-4.5
if %errorlevel% neq 0 goto failure
%nunit% NRaft.Server.Tests\bin\NRaft.Server.Tests.dll /framework:net-4.5
if %errorlevel% neq 0 goto failure
%nunit% NRaft.Client.Tests\bin\NRaft.Client.Tests.dll /framework:net-4.5
if %errorlevel% neq 0 goto failure

echo Package
mkdir Build
cmd /c %nuget% pack "NRaft.Core\NRaft.Core.csproj" -symbols -o Build -p Configuration=%config% %version%
if %errorlevel% neq 0 goto failure
cmd /c %nuget% pack "NRaft.Server\NRaft.Server.csproj" -symbols -o Build -p Configuration=%config% %version%
if %errorlevel% neq 0 goto failure
cmd /c %nuget% pack "NRaft.Client\NRaft.Client.csproj" -symbols -o Build -p Configuration=%config% %version%
if %errorlevel% neq 0 goto failure

:success
echo Success
goto end

:failure
echo Failed

:end