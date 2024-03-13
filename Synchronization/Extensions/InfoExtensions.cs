namespace Synchronization.Extensions
{
    public static class InfoExtensions
	{
		public static List<FileInfo> Except(this DirectoryInfo source, DirectoryInfo target)
        {
			return source.GetFiles().Except(target.GetFiles()).ToList();
        }
    }
}

