# Synchronization

### Task Structure
```sh
├── Synchronization
│   ├── Exception
│   │   └── FilePathException.cs
│   ├── Job
│   │   └── SynchronizationJob.cs
│   ├── Logs
│   │   ├── ConsoleLog.cs
│   │   └── SynchronizationLog.txt
│   ├── Operations
│   │   ├── Copy.cs
│   │   ├── Removal.cs
│   │   └── Update.cs
│   ├── Program.cs
│   ├── Settings
│   │   ├── FolderOptions.cs
│   │   └── appsettings.json
│   ├── Synchronization.csproj
│   ├── Utils
│   │   ├── CheckSumUtils.cs
│   │   └── WriteLogFileUtils.cs│  
└── Synchronization.sln
```
![](https://img.shields.io/badge/build-success-brightgreen.svg)
[![License](http://img.shields.io/:license-apache-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0.html)

## Instructions

The solution relies on mainly four paths to compile succesfully. Therefore, there are two files which must be updated according to the source and target folders as well as the settings as below.

- The file **program.cs** stores the kick off path value which is expected to reflect the current folder path: 
```csharp
string _settingsPath = @"/foo/Synchronization/Synchronization/Settings/appsettings.json";
```

- On the other hand, the file **appsettings.json** stores the paths related to the two synchronized folders as well as the log target file that must reflect the current folder path: 
```json
{
  "Folders": {
    "SourceFolder": "/foo/Synchronization/Synchronization/srcFolder/",
    "DestinationFolder": "foo/Synchronization/Synchronization/destFolder/",
    "LogFilePath": "foo/Synchronization/Synchronization/Logs/"
  }
}
```

- The file **SynchronizationLog.txt** has the operation logs. 
