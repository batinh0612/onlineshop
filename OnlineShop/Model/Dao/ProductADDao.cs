using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductADDao
    {
        OnlineShopDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ProductADDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);


        }

        public IEnumerable<Product> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long Create(Product product)
        {

            //xử lý alias
            if (string.IsNullOrEmpty(product.MetaTitle))
            {
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);
            }
            product.CreatedDate = DateTime.Now;
            db.Products.Add(product);
            db.SaveChanges();

            return product.ID;
        }

        public long Edit(Product product)
        {

            //xử lý alias
            if (string.IsNullOrEmpty(product.MetaTitle))
            {
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);
            }

            product.CreatedDate = DateTime.Now;
            db.SaveChanges();


            return product.ID;
        }

        public bool Update(Product product)
        {
            try
            {
                var pro = db.Products.Find(product.ID);
                pro.Name = product.Name;
                pro.Code = product.Code;
                pro.MetaTitle = product.MetaTitle;
                pro.Description = product.Description;
                pro.Image = product.Image;
                pro.Price = product.Price;
                pro.Quantity = product.Quantity;
                pro.CategoryID = product.CategoryID;
                pro.Detail = product.Detail;
                pro.Warranty = product.Warranty;
                pro.MetaKeywords = product.MetaKeywords;
                pro.MetaDescriptions = product.MetaDescriptions;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateImages(long productId, string images)
        {
            var product = db.Products.Find(productId);
            product.MoreImages = images;
            db.SaveChanges();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
