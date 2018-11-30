using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;

namespace ToolGood.TransDto.Admins
{
    public class TopAdminMenu
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public bool Activity { get; set; }

        public TopAdminMenu() { }

        public TopAdminMenu(DbAdminMenu adminMenu, int id)
        {
            Id = adminMenu.Id;
            Url = adminMenu.Url;
            Name = adminMenu.Name;
            Activity = adminMenu.Id == id;
        }
        public TopAdminMenu(DbAdminMenu adminMenu, string code)
        {
            Id = adminMenu.Id;
            Url = adminMenu.Url;
            Name = adminMenu.Name;
            Activity = adminMenu.Code == code;
        }

    }
}
