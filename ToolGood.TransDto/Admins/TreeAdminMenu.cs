using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;

namespace ToolGood.TransDto.Admins
{
    public class TreeAdminMenu : TreeAdminMenuItem
    {
        public List<TreeAdminMenuItem> Items { get; set; }

        public TreeAdminMenu()
        {
            Items = new List<TreeAdminMenuItem>();
        }

        public TreeAdminMenu(DbAdminMenu adminMenu) : this()
        {
            Id = adminMenu.Id;
            Url = adminMenu.Url;
            Name = adminMenu.Name;
        }

    }

    public class TreeAdminMenuItem
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }


        public TreeAdminMenuItem() { }

        public TreeAdminMenuItem(DbAdminMenu adminMenu)
        {
            Id = adminMenu.Id;
            Url = adminMenu.Url;
            Name = adminMenu.Name;
        }
    }

}
