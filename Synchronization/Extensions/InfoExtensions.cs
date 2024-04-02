namespace Synchronization.Extensions
{
    public static class InfoExtensions
	{
		public static List<FileInfo> Except(this DirectoryInfo source, DirectoryInfo target)
        {
			return source.GetFiles().Except(target.GetFiles())
                .ToList();
        }

        public static List<FileInfo> RootFileSearcher(this DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetFiles()
                .ToList();            
        }

        public static List<string> DirectoriesSearcher(this string fullName)
        {
            return Directory.GetDirectories(fullName, "*.*", SearchOption.AllDirectories)
                .ToList();
        }

        public static List<string> SubFileSearcher(this string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .ToList();
        }
    }
}

