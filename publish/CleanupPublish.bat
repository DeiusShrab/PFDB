@echo off
if not exist "Application Files" exit /b
if not exist "Publish" mkdir Publish
cd "Application Files"
for /F %%d in ('dir /b') do (
cd %%d
for /F %%f in ('dir /b') do (
move %%f ../../Publish
)
cd ..
rmdir %%d
)
cd ..
del *.application
rmdir "Application Files"