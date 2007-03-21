echo on
echo ====================================
echo = 	Build and Convert the Microsoft.VisualBasic_test_VB.dll into Java jar
echo =	
echo =	NOTE: firest, build the Microsoft.VisualBasic.dll using the TARGET_JVM=True define flag.
echo =	
echo =	sample usage: 
echo =	VB_TESTS_2005.J2EE.build.bat
echo =	
echo ====================================

SET VB_COMPILE_OPTIONS_J2EE=/define:TARGET_JVM=True

SET TEST_ASSEMBLY=Microsoft.2005_VisualBasic_test_VB
rem IF NOT EXIST "Test\bin\Debug\Microsoft.2005_VisualBasic_test_VB.dll" GOTO EXCEPTION
rem IF NOT EXIST "Test\bin\Debug\Microsoft.2005_VisualBasic_test.dll" GOTO EXCEPTION

echo == Microsoft.2005_VisualBasic_test_VB.dll exist, start working on it == 

rem ====================================
rem set environment settings for running J2EE applications
IF NOT DEFINED JAVA_HOME SET JAVA_HOME="C:\jdk1.5.0_06"
echo using JAVA_HOME=%JAVA_HOME%


rem ===========================
rem = SET CLASSPATH and Java options
rem = 
rem = Grasshopper variables and jars
rem ===========================

SET VMW4J2EE_DIR=C:\Program Files\Mainsoft\Visual MainWin for J2EE V2
SET VMW4J2EE_JGAC_DIR=java_refs\vmw4j2ee_110

SET VMW4J2EE_JGAC_JARS="%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\mscorlib.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Xml.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\System.Data.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\vmwutils.jar"
rem SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\J2SE.Helpers.jar"
SET VMW4J2EE_JGAC_JARS=%VMW4J2EE_JGAC_JARS%;"%VMW4J2EE_DIR%\%VMW4J2EE_JGAC_DIR%\Microsoft.VisualBasic.jar"

SET NET_FRAMEWORK_DIR="%WINDIR%\Microsoft.NET\Framework\v2.0.50727"

echo using NET_FRAMEWORK_DIR=%NET_FRAMEWORK_DIR%
set path=%path%;%NET_FRAMEWORK_DIR%

set NUNIT_PATH=..\..\mcs\nunit20\
set NUNIT_CLASSPATH=%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.framework.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.util.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit.core.jar;%NUNIT_PATH%nunit-console\bin\Debug_Java20\nunit-console.jar

set CLASSPATH=%NUNIT_CLASSPATH%;%VMW4J2EE_JGAC_JARS%

echo Set log file options.
set startDate=%date%
set startTime=%time%
set sdy=%startDate:~10%
set /a sdm=1%startDate:~4,2% - 100
set /a sdd=1%startDate:~7,2% - 100
set /a sth=%startTime:~0,2%
set /a stm=1%startTime:~3,2% - 100
set /a sts=1%startTime:~6,2% - 100
set TIMESTAMP=%sdy%_%sdm%_%sdd%_%sth%_%stm%

set OUTPUT_FILE_PREFIX=Microsoft.2005_VisualBasic
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log

echo Building tests solution
msbuild Test\2005VB_test.sln /t:rebuild /p:Configuration=Debug >>%BUILD_LOG% 2<&1

SET TEST_ASSEMBLY=Microsoft.2005_VisualBasic_test_VB

set OUTPUT_FILE_PREFIX=%TEST_ASSEMBLY%
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log

echo converting dll to jar without validator
"%VMW4J2EE_DIR%\bin\jcsc.exe" %CD%\Test\bin\Debug\%TEST_ASSEMBLY%.dll /debug:3 /novalidator /out:%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar /classpath:%CLASSPATH% /lib:%CD%;"%VMW4J2EE_DIR%\java_refs\jre";"%VMW4J2EE_DIR%\java_refs";C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;enterprise=3D4D0A45DB93955D87296AEC9233A701locale >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
rem echo running java validator
rem "%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW4J2EE_DIR%\bin\validator.jar";"%VMW4J2EE_DIR%\bin\bcel.jar";%CLASSPATH%;"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" >>%BUILD_LOG% 2<&1
rem IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
 
echo Running tests
rem run  Microsoft.VisualBasic_test.jar
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi /xml=%TEST_ASSEMBLY%.xml %CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar >>%RUN_LOG% 2<&1

SET TEST_ASSEMBLY=Microsoft.2005_VisualBasic_test

set OUTPUT_FILE_PREFIX=%TEST_ASSEMBLY%
set COMMON_PREFIX=%TIMESTAMP%_%OUTPUT_FILE_PREFIX%.J2EE
set BUILD_LOG=%COMMON_PREFIX%.build.log
set RUN_LOG=%COMMON_PREFIX%.run.log

echo converting dll to jar without validator
"%VMW4J2EE_DIR%\bin\jcsc.exe" %CD%\Test\bin\Debug\%TEST_ASSEMBLY%.dll /debug:3 /novalidator /out:%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar /classpath:%CLASSPATH% /lib:%CD%;"%VMW4J2EE_DIR%\java_refs\jre";"%VMW4J2EE_DIR%\java_refs";C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;enterprise=3D4D0A45DB93955D87296AEC9233A701locale >>%BUILD_LOG% 2<&1
IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
rem echo running java validator
rem "%JAVA_HOME%\bin\java.exe" -cp .;..;"%VMW4J2EE_DIR%\bin\validator.jar";"%VMW4J2EE_DIR%\bin\bcel.jar";%CLASSPATH%;"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" -Xms256m -Xmx512m validator.Validator -jar:"%CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar" >>%BUILD_LOG% 2<&1
rem IF %ERRORLEVEL% NEQ 0 GOTO EXCEPTION
 
echo Running tests
rem run  Microsoft.VisualBasic_test.jar
"%JAVA_HOME%\bin\java" -Xmx1024M -cp %CLASSPATH% NUnit.Console.ConsoleUi /xml=%TEST_ASSEMBLY%.xml %CD%\Test\bin\Debug_Java20\%TEST_ASSEMBLY%.jar >>%RUN_LOG% 2<&1


:FINALLY
echo ======================
echo finished
echo ======================
GOTO END

:EXCEPTION
echo ========================
echo ERROR --- Batch Terminated 
popd
echo ========================
PAUSE

:END
