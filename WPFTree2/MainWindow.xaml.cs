using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;

namespace WPFTree2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region OnLoaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem
                {
                    Header = drive,
                    Tag = drive
                };

                //Add a dummy item
                item.Items.Add(null);

                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main TreeView
                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();


            List<string> directories = this.GetItemsFromItem(item, TreeItemType.Folder);

            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath,
                };

                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });

            directories = this.GetItemsFromItem(item, TreeItemType.File);

            directories.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath,
                };

                item.Items.Add(subItem);
            });
        }

        #endregion

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

        private List<string> GetItemsFromItem(TreeViewItem item, TreeItemType type)
        {
            var directories = new List<string>();

            
            string fullPath = (string)item.Tag;

            try
            {
                string[] items = null;
                switch (type)
                {
                    case TreeItemType.Folder:
                        items = Directory.GetDirectories(fullPath);
                        break;
                    case TreeItemType.File:
                        items = Directory.GetFiles(fullPath);
                        break;
                };

                if (items.Length > 0)
                    directories.AddRange(items);
            }
            catch { }

            return directories;
        }




    }
}
