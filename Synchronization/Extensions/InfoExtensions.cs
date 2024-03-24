namespace Synchronization.Extensions
{
    public static class InfoExtensions
	{
		public static List<FileInfo> Except(this DirectoryInfo source, DirectoryInfo target)
        {
			return source.GetFiles().Except(target.GetFiles()).ToList();
        }

        public static List<FileInfo> AddFiles(this List<FileInfo> list, DirectoryInfo target)
        {
            foreach(var file in target.GetFiles().ToList())
            {
                list.Add(file);
            }

            return list;
        }

        public static List<string> AddFiles(this List<string> list, string path)
        {
            foreach (var dir in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList())
            {
                list.Add(dir);
            }

            return list;
        }

        public static List<string> AddDirectory(this List<string> list, string fullName)
        {
            foreach (var dir in Directory.GetDirectories(fullName, "*.*", SearchOption.AllDirectories).ToList())
            {
                list.Add(dir);
            }

            return list;
        }       
    }
}

