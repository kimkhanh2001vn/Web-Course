using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TMDT.Models.DAO
{
    public class PostCategoriesDAO
    {
        private TapChiDB db = new TapChiDB();

        public PostCategory getCategory(string categoryAlias)
        {
            return db.PostCategories.Where(x => x.Status == true && x.Alias == categoryAlias).FirstOrDefault();
        }

        public IEnumerable<PostCategory> getSubContentCategories(int id)
        {
            return db.PostCategories.Where(x => x.Status == true && x.ParentID == id).ToList();
        }

        public IEnumerable<PostCategory> getParentMenu()
        {
            return db.PostCategories.Where(x => x.Status == true && x.ParentID.ToString().Length == 1).OrderBy(x => x.DisplayOrder).ToList();
        }

        public IEnumerable<PostCategory> getChildMenu()
        {
            return db.PostCategories.Where(x => x.Status == true && x.ParentID.ToString().Length > 1).OrderBy(x => x.DisplayOrder).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                PostCategory postcat = db.PostCategories.Find(id);
                db.PostCategories.Remove(postcat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Used in this project    

        public IQueryable<PostCategory> getCatgories()
        {
            return db.PostCategories.Where(x => x.Status == true && x.ID != 1041);
        }
        public IQueryable<PostCategory> getProduct()
        {
            return db.PostCategories.Where(x => x.Status == true);
        }
        public List<SelectListItem> ProductCategories()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            itemList.AddRange(getProduct().Select(x => new SelectListItem { Text = x.Name, Value = x.Name }).ToList());
            return itemList;
        }
        public List<SelectListItem> CreateMagazineCategorySelectList()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            itemList.AddRange(getCatgories().Select(x => new SelectListItem { Text = x.Name, Value = x.Name }).ToList());
            return itemList;
        }

        public List<SelectListItem> CreateMagazineCategorySelectList(string selectedValue)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem { Text = "", Value = "" });
            itemList.AddRange(getCatgories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Name == selectedValue ? true : false
            })
                                                                            .ToList());
            return itemList;
        }

        public List<PostCategory> getOrderedCatgories()
        {
            List<PostCategory> CategoriesOrderByParentID = getCatgories().OrderBy(x => x.ParentID).ToList();
            List<PostCategory> orderedList = CategoriesOrderByParentID.Where(x => x.ParentID == 0).ToList();

            foreach (PostCategory parentCategory in CategoriesOrderByParentID.Where(x => x.ParentID == 0).ToList()) //Should be in orderedList but error list was modified during foreach
            {
                int i = orderedList.IndexOf(parentCategory);
                orderedList.InsertRange(i + 1, CategoriesOrderByParentID.Where(x => x.ParentID == parentCategory.ID).ToList());
            }
            foreach (PostCategory postcat in orderedList)
            {
                if (postcat.ParentID != 0)
                {
                    postcat.Name = "--- " + postcat.Name;
                }
            }
            return orderedList;
        }

        public string getPostCatNames () //return Tin Tức, Đi đâu ăn gì, ....
        {
            string PostCatNames = "";
            foreach (var postcatname in getCatgories().Select(x => x.Name))
            {
                PostCatNames += "'" + postcatname + "'" + ',';
            }
            PostCatNames = PostCatNames.Remove(PostCatNames.Length - 1);
            return PostCatNames;
        }        
    }
}