@echo off
chcp 65001
cd /d "%~dp0"

echo ====================================
echo Удаление кэшей и временных файлов
echo ====================================
rmdir /s /q bin 2>nul
rmdir /s /q obj 2>nul
rmdir /s /q .vs 2>nul
dotnet nuget locals all --clear

echo ====================================
echo Установка global.json
echo ====================================
echo { ^
  "sdk": { ^
    "version": "9.0.308", ^
    "rollForward": "disable", ^
    "allowPrerelease": false ^
  } ^
} > global.json

echo ====================================
echo Проверка SDK
echo ====================================
dotnet --version

echo ====================================
echo Восстановление пакетов
echo ====================================
dotnet restore --no-cache

echo ====================================
echo Переустановка EF Core Tools
echo ====================================
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 9.0.0

echo ====================================
echo Сборка проекта
echo ====================================
dotnet build --no-restore

pause