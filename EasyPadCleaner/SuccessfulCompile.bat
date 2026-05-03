chcp 65001
set datestr=%date:~10,4%-%date:~4,2%-%date:~7,2%
SET HOUR=%time:~0,2%
SET dtStamp9=0%time:~1,1%_%time:~3,2%_%time:~6,2%
SET dtStamp24=%time:~0,2%_%time:~3,2%_%time:~6,2%

if "%HOUR:~0,1%" == " " (SET dtStamp=%dtStamp9%) else (SET dtStamp=%dtStamp24%)

if exist "A:\" (
   mkdir "A:\EasyPadCleaner"
   CD /D "A:\EasyPadCleaner"
   mkdir %datestr%
   CD %datestr%
   mkdir %dtStamp%
   CD %dtStamp%
   xcopy /e /c /h /k /y /i "D:\EasyPad\EasyPadCleaner\"
rem   rar a -r -dh EasyPadCleaner.rar "D:\EasyPad\EasyPadCleaner\"
)
echo %datestr%
echo %dtStamp%
Exit 0