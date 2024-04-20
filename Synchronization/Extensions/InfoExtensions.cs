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

        public static string FirstCharToUpper(this String input, char separator = ' ')
        {
            var formatedString = new List<string>();
            foreach (var word in input.Split(separator))
            {
                switch (word)
                {
                    case null: throw new ArgumentNullException(nameof(word));

                    case "": throw new ArgumentException($"{nameof(word)} cannot be empty", nameof(word));

                    default:
                        formatedString.Add(word.First().ToString().ToUpper() + word.Substring(1));
                        break;
                }
            }
            return string.Join(" ", formatedString);
        }
    }
}

