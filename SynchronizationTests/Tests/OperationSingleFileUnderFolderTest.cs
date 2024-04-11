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

        var dir = _dirUtils.Folder(String.Format(@"{0}{1}", FolderOptions.SourceFolder, "source"));

        if (!dir.Exists) dir.Create();

        _folderPath = String.Format(@"{0}{1}", FolderOptions.SourceFolder, "source");
    }

    [Test]
    public void CopySingleFileToFolderTest()
    {
        _fileUtils.FileWritter(FolderOptions.SourceFolder + "test_1454.txt", false, "Copy");
        _fileUtils.FileWritter(String.Format(@"{0}/{1}", _folderPath, "test_1454.txt"), false, "Copy");

        _operation.Copy();

        checkSum = _checkSum.SHA256CheckSum(String.Format(@"{0}{1}/{2}", FolderOptions.DestinationFolder, "source", "test_1454.txt"));

        Assert.That(checkSum, Is.Not.Empty);
    }

    [Test]
    public void CopyUpdatedFileToFolderTest()
    {
        checkSum = _checkSum.SHA256CheckSum(String.Format(@"{0}{1}/{2}", FolderOptions.DestinationFolder, "source", "test_1454.txt"));

        _fileUtils.FileWritter(String.Format(@"{0}/{1}", _folderPath, "test_1454.txt"), false, "Updated folder");

        _operation.Update();

        Assert.That(_checkSum.SHA256CheckSum(String.Format(@"{0}{1}/{2}", FolderOptions.DestinationFolder, "source", "test_1454.txt")),
            Is.Not.EqualTo(checkSum));
    }

    [Test]
    public void validateRootFilesAreEqualsTest()
    {
        var srcFolder = _checkSum.SHA256CheckSum(FolderOptions.SourceFolder + "test_1454.txt");
        var destFolder = _checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt");      

        Assert.That(srcFolder, Is.EqualTo(destFolder));
    }

    [Test]
    public void DeleteFileFromFolderTest()
    {
        File.Delete(String.Format(@"{0}{1}/{2}", FolderOptions.SourceFolder, "source", "test_1454.txt"));

        _operation.Delete();

        Assert.That(File.Exists(String.Format(@"{0}{1}/{2}", FolderOptions.DestinationFolder, "source", "test_1454.txt")), Is.False);
    }

    [Test]
    public void DeleteSourceFolderTest()
    {
        _dirUtils.Folder(String.Format(@"{0}{1}", FolderOptions.SourceFolder, "source")).Delete();

        _operation.Delete();

        Assert.That(_dirUtils.Folder(String.Format(@"{0}{1}", FolderOptions.DestinationFolder, "source")).Exists, Is.False);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        File.Delete(FolderOptions.SourceFolder + "test_1454.txt");
        _operation.Delete();
    }
}

