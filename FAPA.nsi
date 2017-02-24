# This installs two files, app.exe and logo.ico, creates a start menu shortcut, builds an uninstaller, and
# adds uninstall information to the registry for Add/Remove Programs
 
# To get started, put this script into a folder with the two files (app.exe, logo.ico, and license.rtf -
# You'll have to create these yourself) and run makensis on it
 
# If you change the names "app.exe", "logo.ico", or "license.rtf" you should do a search and replace - they
# show up in a few places.
# All the other settings can be tweaked by editing the !defines at the top of this script
!define APPNAME "FePA"
!define COMPANYNAME PA•WARE
!define DESCRIPTION "Fatturazione elettronica"
# These three must be integers
!define VERSIONMAJOR 1
!define VERSIONMINOR 1
!define VERSIONBUILD 1
# These will be displayed by the "Click here for support information" link in "Add/Remove Programs"
# It is possible to use "mailto:" links in here to open the email client
!define HELPURL "http://..." # "Support Information" link
!define UPDATEURL "http://..." # "Product Updates" link
!define ABOUTURL "http://..." # "Publisher" link
# This is the size (in kB) of all the files copied into "Program Files"
!define INSTALLSIZE 7233
 
RequestExecutionLevel admin ;Require admin rights on NT6+ (When UAC is turned on)
 
InstallDir "$PROGRAMFILES\${COMPANYNAME}\${APPNAME}"
 
# rtf or txt file - remember if it is txt, it must be in the DOS text format (\r\n)
LicenseData "license.rtf"

# This will be in the installer/uninstaller's title bar
Name "${COMPANYNAME} - ${APPNAME}"

 #Icon "C:\FAPAL\FaPA\GUI\Design\Styles\Images\fpa.jpg"

outFile "FAPA.exe"
 
!include LogicLib.nsh
 
# Just three pages - license agreement, install location, and installation
page license
page directory
Page instfiles

; ################################################################
; appends \ to the path if missing
; example: !insertmacro GetCleanDir "c:\blabla"
; Pop $0 => "c:\blabla\"
!macro GetCleanDir INPUTDIR
  ; ATTENTION: USE ON YOUR OWN RISK!
  ; Please report bugs here: http://stefan.bertels.org/
  !define Index_GetCleanDir 'GetCleanDir_Line${__LINE__}'
  Push $R0
  Push $R1
  StrCpy $R0 "${INPUTDIR}"
  StrCmp $R0 "" ${Index_GetCleanDir}-finish
  StrCpy $R1 "$R0" "" -1
  StrCmp "$R1" "\" ${Index_GetCleanDir}-finish
  StrCpy $R0 "$R0\"
${Index_GetCleanDir}-finish:
  Pop $R1
  Exch $R0
  !undef Index_GetCleanDir
!macroend
 
; ################################################################
; similar to "RMDIR /r DIRECTORY", but does not remove DIRECTORY itself
; example: !insertmacro RemoveFilesAndSubDirs "$INSTDIR"
!macro RemoveFilesAndSubDirs DIRECTORY
  ; ATTENTION: USE ON YOUR OWN RISK!
  ; Please report bugs here: http://stefan.bertels.org/
  !define Index_RemoveFilesAndSubDirs 'RemoveFilesAndSubDirs_${__LINE__}'
 
  Push $R0
  Push $R1
  Push $R2
 
  !insertmacro GetCleanDir "${DIRECTORY}"
  Pop $R2
  FindFirst $R0 $R1 "$R2*.*"
${Index_RemoveFilesAndSubDirs}-loop:
  StrCmp $R1 "" ${Index_RemoveFilesAndSubDirs}-done
  StrCmp $R1 "." ${Index_RemoveFilesAndSubDirs}-next
  StrCmp $R1 ".." ${Index_RemoveFilesAndSubDirs}-next
  IfFileExists "$R2$R1\*.*" ${Index_RemoveFilesAndSubDirs}-directory
  ; file
  Delete "$R2$R1"
  goto ${Index_RemoveFilesAndSubDirs}-next
${Index_RemoveFilesAndSubDirs}-directory:
  ; directory
  RMDir /r "$R2$R1"
${Index_RemoveFilesAndSubDirs}-next:
  FindNext $R0 $R1
  Goto ${Index_RemoveFilesAndSubDirs}-loop
${Index_RemoveFilesAndSubDirs}-done:
  FindClose $R0
 
  Pop $R2
  Pop $R1
  Pop $R0
  !undef Index_RemoveFilesAndSubDirs
!macroend
 
!macro VerifyUserIsAdmin
UserInfo::GetAccountType
pop $0
${If} $0 != "admin" ;Require admin rights on NT4+
        messageBox mb_iconstop "Administrator rights required!"
        setErrorLevel 740 ;ERROR_ELEVATION_REQUIRED
        quit
${EndIf}
!macroend
 
 Function CheckAndDownloadDotNet45
	# Let's see if the user has the .NET Framework 4.5 installed on their system or not
	# Remember: you need Vista SP2 or 7 SP1.  It is built in to Windows 8, and not needed
	# In case you're wondering, running this code on Windows 8 will correctly return is_equal
	# or is_greater (maybe Microsoft releases .NET 4.5 SP1 for example)
 
	# Set up our Variables
	Var /GLOBAL dotNET45IsThere
	Var /GLOBAL dotNET_CMD_LINE
	Var /GLOBAL EXIT_CODE
 
        # We are reading a version release DWORD that Microsoft says is the documented
        # way to determine if .NET Framework 4.5 is installed
	ReadRegDWORD $dotNET45IsThere HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Release"
	IntCmp $dotNET45IsThere 378389 is_equal is_less is_greater
 
	is_equal:
		Goto done_compare_not_needed
	is_greater:
		# Useful if, for example, Microsoft releases .NET 4.5 SP1
		# We want to be able to simply skip install since it's not
		# needed on this system
		Goto done_compare_not_needed
	is_less:
		Goto done_compare_needed
 
	done_compare_needed:
		#.NET Framework 4.5 install is *NEEDED*
 
		# Microsoft Download Center EXE:
		# Web Bootstrapper: http://go.microsoft.com/fwlink/?LinkId=225704
		# Full Download: http://go.microsoft.com/fwlink/?LinkId=225702
 
		# Setup looks for components\NDP451-KB2858728-x86-x64-AllOS-ENU.exe relative to the install EXE location
		# This allows the installer to be placed on a USB stick (for computers without internet connections)
		# If the .NET Framework 4.5 installer is *NOT* found, Setup will connect to Microsoft's website
		# and download it for you
 
		# Reboot Required with these Exit Codes:
		# 1641 or 3010
 
		# Command Line Switches:
		# /showrmui /passive /norestart
 
		# Silent Command Line Switches:
		# /q /norestart
 
 
		# Let's see if the user is doing a Silent install or not
		IfSilent is_quiet is_not_quiet
 
		is_quiet:
			StrCpy $dotNET_CMD_LINE "/q /norestart"
			Goto LookForLocalFile
		is_not_quiet:
			StrCpy $dotNET_CMD_LINE "/showrmui /passive /norestart"
			Goto LookForLocalFile
 
		LookForLocalFile:
			# Let's see if the user stored the Full Installer
			IfFileExists "$INSTDIR\NDP451-KB2858728-x86-x64-AllOS-ENU.exe" do_local_install do_network_install
 
			do_local_install:
				# .NET Framework found on the local disk.  Use this copy
 
				ExecWait '"$INSTDIR\NDP451-KB2858728-x86-x64-AllOS-ENU.exe" $dotNET_CMD_LINE' $EXIT_CODE
				Goto is_reboot_requested
 
			# Now, let's Download the .NET
			do_network_install:
 
				Var /GLOBAL dotNetDidDownload
				NSISdl::download "http://go.microsoft.com/fwlink/?LinkId=225704" "$TEMP\dotNET45Web.exe" $dotNetDidDownload
 
				StrCmp $dotNetDidDownload success fail
				success:
					ExecWait '"$TEMP\dotNET45Web.exe" $dotNET_CMD_LINE' $EXIT_CODE
					Goto is_reboot_requested
 
				fail:
					MessageBox MB_OK|MB_ICONEXCLAMATION "Download .NET Framework 4.5 non riuscito.  ${APPNAME} sarà installato, ma non funzionerà senza il framework!"
					Goto done_dotNET_function
 
				# $EXIT_CODE contains the return codes.  1641 and 3010 means a Reboot has been requested
				is_reboot_requested:
					${If} $EXIT_CODE = 1641
					${OrIf} $EXIT_CODE = 3010
						SetRebootFlag true
					${EndIf}
 
	done_compare_not_needed:
		# Done dotNET Install
		Goto done_dotNET_function
 
	#exit the function
	done_dotNET_function:
 
    FunctionEnd

function .onInit
	setShellVarContext all
	!insertmacro VerifyUserIsAdmin

	 ReadRegStr $R0 HKLM \
	  "Software\Microsoft\Windows\CurrentVersion\Uninstall\PA•WARE Energy Manager" \
	  "UninstallString"
	  StrCmp $R0 "" done
	 
	  MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
	  "PA•WARE Energy Manager is already installed. $\n$\nClick `OK` to remove the \
	  previous version or `Cancel` to cancel this upgrade." \
	  IDOK uninst
	  Abort
	 
	;Run the uninstaller
	uninst:
	  ClearErrors
	  ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file
	 
	  IfErrors no_remove_uninstaller done
		;You can either use Delete /REBOOTOK in the uninstaller or add some code
		;here to remove the uninstaller. Use a registry key to check
		;whether the user has chosen to uninstall. If you are using an uninstaller
		;components page, make sure all sections are uninstalled.
	  no_remove_uninstaller:
	 
	done:	
functionEnd

section "install"
	# Files for the install directory - to build the installer, these should be in the same directory as the install script (this file)
	setOutPath $INSTDIR

	AccessControl::GrantOnFile $INSTDIR "(BU)" "FullAccess"
	AccessControl::GrantOnFile "$SMPROGRAMS\${COMPANYNAME}\${APPNAME}" "(BU)" "FullAccess"

	file "C:\EML\EnergyManager\NDP451-KB2858728-x86-x64-AllOS-ENU.exe"
	Call CheckAndDownloadDotNet45

	#file "C:\EML\EnergyManager\logo.ico"

	file "C:\FAPAL\FaPA\bin\Release\FaPA.EXE"
	file "C:\FAPAL\FaPA\bin\Release\Caliburn.Micro.dll"
	#file "C:\FAPAL\FaPA\bin\Release\NHibernate.Caches.SysCache.dll"
	file "C:\FAPAL\FaPA\bin\Release\NHibernate.dll"
	#file "C:\FAPAL\FaPA\bin\Release\NHibernate.Spatial.dll"
	#file "C:\FAPAL\FaPA\bin\Release\NHibernate.Spatial.MsSql.dll"
	file "C:\FAPAL\FaPA\bin\Release\NHibernate.Validator.dll"
	file "C:\FAPAL\FaPA\bin\Release\NHibernate.Validator.Specific.dll"
	file "C:\FAPAL\FaPA\bin\Release\System.Reactive.Core.dll"
	file "C:\FAPAL\FaPA\bin\Release\System.Reactive.Interfaces.dll"
	file "C:\FAPAL\FaPA\bin\Release\System.Reactive.Linq.dll"
	file "C:\FAPAL\FaPA\bin\Release\System.Reactive.PlatformServices.dll"
	file "C:\FAPAL\FaPA\bin\Release\Xceed.Wpf.Toolkit.dll"
	file "C:\FAPAL\FaPA\bin\Release\FaPA.exe.config"
	file "C:\FAPAL\FaPA\bin\Release\fatturaordinaria_v1.2.xsl"
	file "C:\FAPAL\FaPA\bin\Release\fatturapa_v1.2.xsl"
	
	delete $INSTDIRNDP451-KB2858728-x86-x64-AllOS-ENU.exe

	#file "logo.ico"
	
	# Add any other files for the install directory (license files, app data, etc) here
 
	# Uninstaller - See function un.onInit and section "uninstall" for configuration
	writeUninstaller "$INSTDIR\uninstall.exe"
 
	# Start Menu
	createDirectory "$SMPROGRAMS\${COMPANYNAME}"
	createShortCut "$SMPROGRAMS\${COMPANYNAME}\${APPNAME}.lnk" "$INSTDIR\EMG.exe" "" "$INSTDIR\logo.ico"
	 
	# Registry information for add/remove programs
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "DisplayName" "${COMPANYNAME} - ${APPNAME} - ${DESCRIPTION}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "QuietUninstallString" "$\"$INSTDIR\uninstall.exe$\" /S"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "InstallLocation" "$\"$INSTDIR$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "DisplayIcon" "$\"$INSTDIR\logo.ico$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "Publisher" "${COMPANYNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "HelpLink" "$\"${HELPURL}$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "URLUpdateInfo" "$\"${UPDATEURL}$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "URLInfoAbout" "$\"${ABOUTURL}$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "DisplayVersion" "$\"${VERSIONMAJOR}.${VERSIONMINOR}.${VERSIONBUILD}$\""
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "VersionMajor" ${VERSIONMAJOR}
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "VersionMinor" ${VERSIONMINOR}
	# There is no option for modifying or repairing the install
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "NoModify" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "NoRepair" 1
	# Set the INSTALLSIZE constant (!defined at the top of this script) so Add/Remove Programs can accurately report the size
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}" "EstimatedSize" ${INSTALLSIZE}
sectionEnd
 
# Uninstaller
 
function un.onInit
	SetShellVarContext all
 
	#Verify the uninstaller - last chance to back out
	MessageBox MB_OKCANCEL "Rimuovere permanentemente ${APPNAME}?" IDOK next
		Abort
	next:
	!insertmacro VerifyUserIsAdmin
functionEnd
 
section "uninstall"
 
	# Remove Start Menu launcher
	delete "$SMPROGRAMS\${COMPANYNAME}\${APPNAME}.lnk"
	# Try to remove the Start Menu folder - this will only happen if it is empty
	rmDir "$SMPROGRAMS\${COMPANYNAME}"
 
	# Remove files
	delete $INSTDIR\it-IT
	delete $INSTDIR\NDP451-KB2858728-x86-x64-AllOS-ENU.exe
	delete $INSTDIR\FaPA.EXE
	delete $INSTDIR\Caliburn.Micro.dll
	delete $INSTDIR\NHibernate.Caches.SysCache.dll
	delete $INSTDIR\NHibernate.dll
	delete $INSTDIR\NHibernate.Spatial.dll
	delete $INSTDIR\NHibernate.Spatial.MsSql.dll
	delete $INSTDIR\NHibernate.Validator.dll
	delete $INSTDIR\NHibernate.Validator.Specific.dll
	delete $INSTDIR\System.Reactive.Core.dll
	delete $INSTDIR\System.Reactive.Interfaces.dll
	delete $INSTDIR\System.Reactive.Linq.dll
	delete $INSTDIR\System.Reactive.PlatformServices.dll
	delete $INSTDIR\Xceed.Wpf.Toolkit.dll
	delete $INSTDIR\FaPA.exe.config
	delete $INSTDIR\fatturaordinaria_v1.2.xsl
	delete $INSTDIR\fatturapa_v1.2.xsl
	
	 
	# Always delete uninstaller as the last action
	delete $INSTDIR\uninstall.exe
 
	# Try to remove the install directory - this will only happen if it is empty
	!insertmacro RemoveFilesAndSubDirs "$INSTDIR\Design\"
	!insertmacro RemoveFilesAndSubDirs "$INSTDIR\it-IT\"
	rmDIR /r "$INSTDIR\Design"
	rmDIR /r "$INSTDIR\it-IT"
	RMDir /r "$INSTDIR\SqlServerTypes"
	rmDir /r "$INSTDIR"

	# Remove Start Menu launcher
	delete "$SMPROGRAMS\${COMPANYNAME}\${APPNAME}.lnk"
	# Try to remove the Start Menu folder - this will only happen if it is empty
	rmDir /r "$SMPROGRAMS\${COMPANYNAME}"

 
	# Remove uninstaller information from the registry
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${COMPANYNAME} ${APPNAME}"
sectionEnd