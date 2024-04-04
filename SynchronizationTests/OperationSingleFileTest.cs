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

    private string? _source { get; set; } = String.Empty;
    private string? _target { get; set; } = String.Empty;
    private string checkSum { get; set; } = String.Empty;

    [SetUp]
    public void SetUp()
    {
        SettingOptions.GetSettings();
        _source = FolderOptions.SourceFolder;
        _target = FolderOptions.DestinationFolder;

        if (string.IsNullOrWhiteSpace(_source) || string.IsNullOrWhiteSpace(_target))
            throw new FilePathException("No empty path is allowed!");

        _operation.InitializeOperation(_source, _target);
    }

    [Test]
    public void CopySingleFileTest()
    {
        _fileUtils.FileWritter(_source + "test_1454.txt", false, "Copy");

        _operation.Copy();

        checkSum = _checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt");

        Assert.That(checkSum, Is.Not.Empty);
    }

    [Test]
    public void CopyUpdatedSingleFileTest()
    {
        checkSum = _checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt");

        _fileUtils.FileWritter(_source + "test_1454.txt", true, "Update");

        _operation.Update();

        Assert.That(_checkSum.SHA256CheckSum(FolderOptions.DestinationFolder + "test_1454.txt"),
            Is.Not.EqualTo(checkSum));
    }

    [Test]
    public void DeleteSingleFileTest()
    {
        File.Delete(_source + "test_1454.txt");

        _operation.Delete();

        Assert.That(File.Exists(_target + "test_1454.txt"), Is.False);
    }
}
