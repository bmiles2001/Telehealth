REM Specify a name for the Migration
dotnet ef migrations add %1 -o Infrastructure\Migrations\%1 -p ..\..\ -s ..\..\
dotnet ef migrations script -o ..\Migrations\%1\%1.sql -p ..\..\ -s ..\..\