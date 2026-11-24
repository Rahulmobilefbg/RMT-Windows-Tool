RMT Native Windows Project (C# / WinForms)

Upload these items to the ROOT of your GitHub repository:
- .github folder (including workflows/build.yml)
- RMTNative.csproj
- Program.cs
- README_HINDI.txt (optional)

Then:
1. Commit the uploaded files.
2. Open Actions tab.
3. Select Build RMT Windows EXE.
4. Click Run workflow.
5. When finished, open the workflow run and download Artifact: RMT-Windows-EXE.

This is a basic diagnostic launcher. It does not implement unlocking, FRP bypass, IMEI repair, flashing, or exploit operations. ADB/Fastboot must be available in PATH or configured by user.
