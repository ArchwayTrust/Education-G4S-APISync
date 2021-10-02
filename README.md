# G4S API Sync
Tool that syncs the G4S API to a a local SQL database.

- Written by Ben Dobbs from Archway Learning Trust
- bdobbs@archwaytrust.co.uk
- Released under GNU General Public License v3.0

Feel free to use and ammend under the terms of the GNU license but we are unable to offer support or modifications.

## Initial Setup
You will need a Microsoft SQL Server and initially an AD user account with privaledges for database creation.

1. Ammend the connection string in appsettings.json to point to your server.
2. Install https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.10-windows-x64-installer
3. Using an account with sufficient privaledges run G4SApiSync.exe
4. This first run either creates the database or updates it to the latest version.

## Adding API keys into SQL
1. Connect to your SQL database using SSMS, right click on the table called sec.AcademySecurity and then "Edit Top 100".
2. Add a row for each academy. "CurrentAcademicYear" for 2020/2021 would be 2021.
3. AcademyCode in this table should be a unique aconym for the academy.

## First data sync
Run G4SApiSync.exe with an account that has read/right privaledges on the database.

## Schedule
We let G4SApiSync run on a schedule using Task Scheduler on the server hosting the database.

## Historic Data
You can change the current accademic year field in sec.AcademySecurity to get data from previous years. It is all tagged in the database so doesn't over write.

## Behaviour Data
To enable behaviour data you need to use SSMS to edit sec.AcademySecurity.
  1. Change GetBehaviour to True.
  2. If BehaviourFrom and BehaviorTo are left NULL then it will sync the last 7 days.
  3. If you enter a date range within the currently selected dataset it will get behaviour for those dates.

## Session Attendance Data
To enable session attendance data you need to use SSMS to edit sec.AcademySecurity.
  1. Change GetSessionAttendance to True.
  2. If AttendanceFrom and AttendanceTo are left NULL then it will sync the last 7 days.
  3. If you enter a date range within the currently selected dataset it will get data for those dates.

  ## Lesson Attendance Data
To enable session attendance data you need to use SSMS to edit sec.AcademySecurity.
  1. Change GetLessonAttendance to True.
  2. If AttendanceFrom and AttendanceTo are left NULL then it will sync yesterdays lesson attendance data.
  3. If you enter a date range within the currently selected dataset it will get data for those dates.