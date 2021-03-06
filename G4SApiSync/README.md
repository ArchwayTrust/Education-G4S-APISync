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

## First data sync
Run G4SApiSync.exe with an account that has read/right privaledges on the database.

## Schedule
We let G4SApiSync run on a schedule using Task Scheduler on the server hosting the database.

## Historic Data
You can change the current accademic year field in sec.AcademySecurity to get data from previous years. It is all tagged in the database so doesn't over write.


