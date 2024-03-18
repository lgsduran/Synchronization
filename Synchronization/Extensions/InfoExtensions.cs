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
    }
}

