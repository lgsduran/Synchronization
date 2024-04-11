namespace SynchronizationTests.Utils
{
    public class DirectoryUtils
	{

        public DirectoryInfo Folder(string path)
        {
            try
            {
                return new DirectoryInfo(path);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }      
	}
}

