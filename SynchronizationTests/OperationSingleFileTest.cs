using Synchronization.Operations;
using Synchronization.Settings;
using Synchronization.Utils;
using SynchronizationTests.Utils;

namespace SynchronizationTests;

public class OperationSingleFileTest
{
    private readonly CheckSumUtils _checkSum = new();
    private readonly WriteLogFileUtils _logFile = new();
    private readonly OperationImpl _operation = new();
    private readonly FileUtils _fileUtils = new();
    private string checkSum { get; set; } = String.Empty;

    [SetUp]
    public void SetUp()
    {
        SettingOptions.GetSettings();
        if (string.IsNullOrWhiteSpace(FolderOptions.SourceFolder) || string.IsNullOrWhiteSpace(FolderOptions.DestinationFolder))
            throw new FilePathException("No empty path is allowed!");

        _operation.InitializeOperation(FolderOptions.SourceFolder, FolderOptions.DestinationFolder);
    }

    [Test]
    public void CopySingleFileTest()
    {
        _fileUtils.FileWritter(FolderOptions.SourceFolder + "test_1454.txt", false, "Copy");

        _operation.Copy();

        checkSum = _checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt");

        Assert.That(checkSum, Is.Not.Empty);
    }

    [Test]
    public void CopyUpdatedSingleFileTest()
    {
        checkSum = _checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt");

        _fileUtils.FileWritter(FolderOptions.SourceFolder + "test_1454.txt", true, "Update");

        _operation.Update();

        Assert.That(_checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt"),
            Is.Not.EqualTo(checkSum));
    }

    [Test]
    public void DeleteSingleFileTest()
    {
        File.Delete(FolderOptions.SourceFolder + "test_1454.txt");

        _operation.Delete();

        Assert.That(File.Exists(FolderOptions.DestinationFolder + "test_1454.txt"), Is.False);
    }
}
