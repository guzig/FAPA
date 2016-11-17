using System.Collections.Generic;

namespace FaPA.GUI.Feautures.Main
{
    public class ItemSources
    {
        private List<string> folderitmes;
        public List<string> FolderItems
        {
            get
            {
                this.folderitmes = this.folderitmes ?? this.LoadFolderItems();
                return this.folderitmes;
            }
        }

        private List<string> LoadFolderItems()
        {
            List<string> lst = new List<string>();
            for (int i = 1; i < 10; i++)
                lst.Add(string.Format("Item {0}", i));
            return lst;
        }


        private List<string> gallaryItems;
        public List<string> GallaryItems
        {
            get
            {
                this.gallaryItems = this.gallaryItems ?? this.LoadFolderItems();
                return this.gallaryItems;
            }
        }

    }
}
