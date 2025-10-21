using System.Runtime.InteropServices;

public class Program
{
    public static void Main(string[] args)
    {
        DirectoryItem directory = new DirectoryItem("Folder1");
        directory.Add(new FileItem("File1", "pdf", 50));
        directory.Add(new FileItem("File2", "img", 100));

        DirectoryItem directory2 = new DirectoryItem("SubFolder1");
        directory2.Add(new FileItem("SubFolderFile11", "sql", 150));
        directory2.Add(new FileItem("SubFolderFile12", "mdf", 111));

        DirectoryItem directory21 = new DirectoryItem("SubSubFolder12");
        directory21.Add(new FileItem("SubSubFolderFile12", "pdf", 101));
        directory21.Add(new FileItem("SubSubFolderFile22", "jpg", 120));

        var file = new FileItem("SubFolderFile21", "linq", 121);
        DirectoryItem directory3 = new DirectoryItem("SubFolder2");
        directory3.Add(file);

        directory2.Add(directory21);
        directory.Add(directory2);
        directory.Add(directory3);

        directory.Dispay();
        var totalSize = directory.GetSize();
        Console.WriteLine($"Total Size: {totalSize} byte");

        //Remove Files / Folders
        Console.WriteLine("\nAfter Removing SubFolder1 OR Files \n");
        directory.Remove(directory2);

        directory.Dispay();
        totalSize = directory.GetSize();
        Console.WriteLine($"Total Size: {totalSize} byte");
    }

    #region FileSystemCompositePattern
    //Component
    public interface IComponent
    {
        void Dispay(int identLevel);
        long GetSize();
    }

    //Leaf
    public class FileItem : IComponent
    {
        private string _fileName;
        private long _size;
        private string _type;
        public FileItem(string name, string fileType, long size) {
            _fileName = name;
            _type = fileType;
            _size = size;
        }

        public void Dispay(int identLevel)
        {
            Console.WriteLine($"{new string(' ', identLevel * 2)} - FileName = {_fileName} , Filetype = {_type}, FileSize = {_size}");
        }

        public long GetSize()
        {
            return _size;
        }
    }
    //Composite
    public class DirectoryItem : IComponent
    {
        private string _directoryName;
        private List<IComponent> _components = new List<IComponent>();
        private long _totalSize;

        private long GetTotalSize()
        {
            long size = 0;
            foreach (var component in _components)
            {
                size += component.GetSize();
            }
            return size;
        }

        public DirectoryItem(string name) {
            _directoryName = name;
        }
        
        public void Add(IComponent component) {
            _components.Add(component);
            _totalSize = GetTotalSize();
        }

        public void Remove(IComponent component)
        {
            var itemsToRemove = new List<IComponent>();
            foreach (var comp in _components)
            {
                if (comp is DirectoryItem subFolder)
                {
                    subFolder.Remove(component);
                }

                if ((comp == component))
                {
                    if (component is DirectoryItem dir)
                    {
                        itemsToRemove.Add(component);
                        RemoveSubFolders(dir, itemsToRemove);
                    }
                    else
                    {
                        itemsToRemove.Add(component);
                    }
                }
            }
            foreach (var item in itemsToRemove)
            {
                _components.Remove(item);
            }


            _totalSize = GetTotalSize();
        }

        private void RemoveSubFolders(DirectoryItem dir, List<IComponent> itemsToRemove)
        {
            foreach (var component in dir._components)
            {
               if (component is DirectoryItem dir1)
               {
                  RemoveSubFolders(dir1, itemsToRemove);
               }
                itemsToRemove.Add(component);
            }
        }

        public void Dispay(int identLevel = 0)
        {
            Console.WriteLine($"{new string(' ', identLevel * 2)} - {_directoryName}" );

            foreach (var component in _components)
            {
                component.Dispay(identLevel + 1);
            }
        }

        public long GetSize()
        {
            return _totalSize;
        }
    }

    #endregion
}