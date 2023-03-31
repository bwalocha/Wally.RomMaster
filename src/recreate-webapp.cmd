SET WEBAPP=wally.rommaster.webapp

rmdir .\%WEBAPP% /S /Q
call npx create-next-app@latest --typescript --eslint %WEBAPP%
cd ./%WEBAPP%
call npm i
call npm run build
cd ..
pause 
