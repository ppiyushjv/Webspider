SET ECHO OFF
CLS
ECHO Creating Folder Structure...
MKDIR Build\App_Code
MKDIR Build\Cache

ECHO Copying files...
COPY WebSpider.Core\bin\x86\Debug\* Build\*
COPY WebSpider.Data\bin\x86\Debug\* Build\*
COPY WebSpider.Data\bin\x86\Debug\App_Code\* Build\App_Code\*
COPY WebSpider.Objects\bin\x86\Debug\* Build\*
COPY WebSpiderLibrary\bin\x86\Debug\* Build\*
COPY SpiderAlert\bin\Debug\* Build\*
COPY WebSpider1\bin\Debug\* Build\*
COPY WebSpider1\bin\Debug\Cache\* Build\Cache\*

ECHO Finalizing...
DEL Build\*.PDB
DEL Build\*.MANIFEST
DEL Build\*vshost*
DEL Build\Errors.txt
DEL Build\JsonResult.txt

ECHO Starting...
