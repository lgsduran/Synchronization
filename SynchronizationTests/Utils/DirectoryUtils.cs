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

        public void DirectoryI(string path)
		{
            DirectoryInfo di = new DirectoryInfo(path);
            try
            {
               

                // Try to create the directory.
                di.Create();
                Console.WriteLine("The directory was created successfully.");

                // Delete the directory.
                //di.Delete();
                Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
	}
}

