@echo off
tasklist /fi "imagename eq ExcelConverter.exe" | find /i "ExcelConverter.exe" > nul
start /d "./" /b ExcelConverter.exe "./ExcelFiles" "./JsonFiles" "./Script" 0

