namespace WPFTree2
{
    public class DirectoryItem
    {
        public string FullPath { get; set; }
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }
        public DirectoryItemType Type { get; set; }
    }
}
