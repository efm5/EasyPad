chcp 65001
set datestr=%date:~10,4%-%date:~4,2%-%date:~7,2%
SET HOUR=%time:~0,2%
SET dtStamp9=0%time:~1,1%_%time:~3,2%_%time:~6,2%
SET dtStamp24=%time:~0,2%_%time:~3,2%_%time:~6,2%

if "%HOUR:~0,1%" == " " (SET dtStamp=%dtStamp9%) else (SET dtStamp=%dtStamp24%)

if exist "A:\" (
   mkdir "A:\EasyPad"
   CD /D "A:\EasyPad"
   mkdir %datestr%
   CD %datestr%
   mkdir %dtStamp%
   CD %dtStamp%
   xcopy /e /c /h /k /y /i "E:\EasyPad\"
)

if exist "D:\EasyPad\Help" (
   if exist "D:\styles.css" (
      CD /D "D:\EasyPad\Help"
      xcopy /c /h /k /y "D:\styles.css"
   )
   if exist "D:\EasyPad\EasyPad\bin\Debug\net10.0-windows7.0" (
      CD /D "D:\EasyPad\EasyPad\bin\Debug\net10.0-windows7.0"
      mkdir "Help"
      CD /D "D:\EasyPad\EasyPad\bin\Debug\net10.0-windows7.0\Help"
      xcopy /c /h /k /y "D:\EasyPad\Help\*.*"
   )
   if exist "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0" (
      CD /D "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0"
      mkdir "Help"
      CD /D "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0\Help"
      xcopy /c /h /k /y "D:\EasyPad\Help\*.*"
   )
)
if exist "D:\EasyPad\EasyPad\bin\Debug\net10.0-windows7.0" (
   CD /D "D:\EasyPad\EasyPad\bin\Debug\net10.0-windows7.0"
   if exist "D:\EasyPad\EasyPadCleaner\EasyPadCleaner\bin\Debug\net10.0-windows7.0" (
      xcopy /c /h /k /y "D:\EasyPad\EasyPadCleaner\EasyPadCleaner\bin\Debug\net10.0-windows7.0\*.*"
   )
)
if exist "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0" (
   CD /D "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0"
   if exist "D:\EasyPad\EasyPadCleaner\EasyPadCleaner\bin\Release\net10.0-windows7.0" (
      xcopy /c /h /k /y "D:\EasyPad\EasyPadCleaner\EasyPadCleaner\bin\Release\net10.0-windows7.0\*.*"
   )
   del "D:\EasyPad\EasyPad\bin\Release\net10.0-windows7.0\*.pdb"
)
echo %datestr%
echo %dtStamp%
Exit 0
