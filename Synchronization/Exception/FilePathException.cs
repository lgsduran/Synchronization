/// <summary>
/// Class <c>FilePathException</c> represents errors that occur
/// when there is a mistake in the file path.
/// </summary>
public class FilePathException : Exception
{
    public FilePathException()
    {
    }

    public FilePathException(string message)
        : base(message)
    {
    }
}