using System.Collections.Generic;
using System.Linq;

namespace TMDT.Models.CustomModel
{
    public class Menu
    {
        public List<ParentMenu> ParentMenuList = new List<ParentMenu>();

        public Menu(List<PostCategoryCustom> list)
        {
            ParentMenuList = list.Where(x => x.ParentID == 0).Select(x => new ParentMenu
            {
                ID = x.ID,
                Name = x.Name,
                Alias = x.Alias
            }).ToList();

            foreach (ParentMenu parentMenu in ParentMenuList)
            {
                parentMenu.ChildMenuList = list.Where(x => x.ParentID == parentMenu.ID).Select(x => new ChildMenu
                {
                    Name = x.Name,
                    Alias = x.Alias
                }).ToList();
                parentMenu.HasSub = parentMenu.ChildMenuList.Any();
            }
        }

        //Class
        public class ParentMenu
        {
            public ParentMenu()
            {
                ChildMenuList = new List<ChildMenu>();
            }
            public List<ChildMenu> ChildMenuList;

            public int ID { get; set; }

            public string Name { get; set; }

            public string Alias { get; set; }

            public bool HasSub { get; set; }
        }

        public class ChildMenu
        {
            public string Name { get; set; }

            public string Alias { get; set; }
        }
    }
}