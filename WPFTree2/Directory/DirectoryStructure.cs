using System.Collections.Generic;
using System.Linq;

namespace WPFTree2
{   
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Get every logical drive on the machine
            return System.IO.Directory.GetLogicalDrives()
                            .Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive })
                            .ToList();
        }

        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string normalizedPath = path.Replace('/', '\\');

            //Returns the substring after the last backslash
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex == -1)
            {
                return path;
            }
            else
            {
                return normalizedPath.Substring(lastIndex + 1);
            }

        }

        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            try
            {
                var fs = Directory.GetDirectories(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            return items;

        }

            

    }
}
