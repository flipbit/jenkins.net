Name "Hudson Monitor"

OutFile "install.hudson.tray.exe"

InstallDir "$PROGRAMFILES\Hudson Monitor"
InstallDirRegKey HKEY_LOCAL_MACHINE "SOFTWARE\cogworks.co.uk\Hudson Monitor" ""
DirText "Select the directory to install Hudson Monitor in:"

### Default Section
Section

	## Check Admin Level

		# call userInfo plugin to get user info.  The plugin puts the result in the stack
		userInfo::getAccountType

		# pop the result from the stack into $0
		pop $0

		# compare the result with the string "Admin" to see if the user is admin.
		# If match, jump 3 lines down.
		strCmp $0 "Admin" +3

		# if there is not a match, print message and return
		messageBox MB_OK "Admin Account Need for installation: $0"
		return

	## Install Application

		SetOutPath "$INSTDIR"

		file "*.exe"
		file "*.dll"

		# add files / whatever that need to be installed here.
		WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\cogworks.co.uk\Hudson Monitor" "" "$INSTDIR"
		WriteRegStr HKEY_LOCAL_MACHINE "Software\Microsoft\Windows\CurrentVersion\Uninstall\Hudson Monitor" "DisplayName" "Hudson Monitor (remove only)"
		WriteRegStr HKEY_LOCAL_MACHINE "Software\Microsoft\Windows\CurrentVersion\Uninstall\Hudson Monitor" "UninstallString" '"$INSTDIR\uninst.exe"'

		# write out uninstaller
		WriteUninstaller "$INSTDIR\uninst.exe"

		# Start menu items
		createShortCut "$SMPROGRAMS\Hudson Monitor\Hudson Monitor.lnk" "$INSTDIR\hudson.tray.exe"

SectionEnd

# begin uninstall settings/section
UninstallText "This will uninstall Hudson Monitor from your system"


### Uninstall Section
Section Uninstall

	## add delete commands to delete whatever files/registry keys/etc you installed here.
	
		Delete "$INSTDIR\*.exe"
		Delete "$INSTDIR\*.dll"
		Delete "$SMPROGRAMS\Hudson Monitor\Hudson Monitor.lnk"

	## Remove registry keys
	
		DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\cogworks.co.uk\Hudson Monitor"
		DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Hudson Monitor"

	## Remove shortcuts and program files
	
		RMDir "$SMPROGRAMS\Hudson Monitor"
		RMDir "$INSTDIR"

SectionEnd