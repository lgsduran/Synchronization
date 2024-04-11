using Synchronization.Operations;
using Synchronization.Settings;
using Synchronization.Utils;
using SynchronizationTests.Utils;

namespace SynchronizationTests;

public class OperationSingleFileUnderFolderTest
{
    private readonly CheckSumUtils _checkSum = new();
    private readonly WriteLogFileUtils _logFile = new();
    private readonly OperationImpl _operation = new();
    private readonly FileUtils _fileUtils = new();
    private readonly DirectoryUtils _dirUtils = new();

    private string? _folderPath { get; set; } = String.Empty;      
    private string checkSum { get; set; } = String.Empty;

    [SetUp]
    public void SetUp()
    {
        SettingOptions.GetSettings();
        if (string.IsNullOrWhiteSpace(FolderOptions.SourceFolder) || string.IsNullOrWhiteSpace(FolderOptions.DestinationFolder))
            throw new FilePathException("No empty path is allowed!");

        _operation.InitializeOperation(FolderOptions.SourceFolder, FolderOptions.DestinationFolder);

        _dirUtils.Folder(String.Format(@"{0}{1}", FolderOptions.SourceFolder, "source")).Create();

        _folderPath = String.Format(@"{0}{1}", FolderOptions.SourceFolder, "source");
    }

    [Test]
    public void CopySingleFileUnderFolderTest()
    {
        _fileUtils.FileWritter(FolderOptions.SourceFolder + "test_1454.txt", false, "Copy");
        _fileUtils.FileWritter(String.Format(@"{0}/{1}", _folderPath, "test_1454.txt"), false, "Copy");

        _operation.Copy();

        checkSum = _checkSum.SHA256CheckSum(String.Format(@"{0}{1}/{2}", FolderOptions.DestinationFolder, "source", "test_1454.txt"));

        Assert.That(checkSum, Is.Not.Empty);
    }        
}

