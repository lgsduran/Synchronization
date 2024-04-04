namespace SynchronizationTests.Utils
{
    public class FileUtils
	{
        private StreamWriter sw = null!;

        public void FileWritter(string filePath, bool append, string text)
        {
            Writter(filePath, append, text);
        }

        private void Writter(string filePath, bool append, string text)
        {
            try
            {
                sw = new StreamWriter(filePath, append);
                sw.WriteLine(text);
                sw.WriteLine("");
            }
            finally
            {
                if (sw is not null)
                    sw.Dispose();
            }
        }
    }
}

