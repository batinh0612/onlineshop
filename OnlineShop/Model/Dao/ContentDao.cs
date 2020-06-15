using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        #region Tag
        //Lấy ra các tags
        public long Create(Content content)
        {
            //xử lý metatitle
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            //xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert tới bảng tag
                    if (!existedTag)
                    {
                        InsertTag(tagId, tag);
                    }

                    //insert tới bảng contenttag
                    InsertContentTag(content.ID, tagId);
                }
            }
            return content.ID;
        }

        //kiểm tra tag đã tồn tại chưa
        public bool CheckTag(string id)
        {
            return db.Tags.Count(n => n.ID == id) > 0;
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        #endregion

        //Danh sách tin tức, phần admin
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(n => n.Name.Contains(searchString));
            }
            return model.OrderByDescending(n => n.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(n => n.CreatedDate).ToPagedList(page, pageSize);
        }

        #region Edit Content
        public long Edit(Content content)
        {
            
            //xử lý metatitle
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            db.SaveChanges();

            //xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                //remove contentTag
                RemoveAllContentTag(content.ID);

                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert tới bảng tag
                    if (!existedTag)
                    {
                        InsertTag(tagId, tag);
                    }

                    //insert tới bảng contenttag
                    InsertContentTag(content.ID, tagId);
                }
            }
            return content.ID;
        }

        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(n => n.ContentID == contentId));
            db.SaveChanges();
        }

        //lấy ra danh sách tag
        public List<Tag> ListTag(long contentId)
        {
            //join 2 bảng, nếu xuất hiện ở cả 2 bảng thì đọc ra
            var model = (from a in db.Tags
                        join b in db.ContentTags
                        on a.ID equals b.TagID
                        where b.ContentID == contentId
                        select new
                        {
                            ID = b.TagID,
                            Name = a.Name
                        }).AsEnumerable().Select(n => new Tag() {
                            ID = n.ID,
                            Name = n.Name
                        });
            return model.ToList();
        }

        public IEnumerable<Content> ListAllByTag(string tag ,int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             Metatitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID
                         }).AsEnumerable().Select(n => new Content() {
                             Name = n.Name,
                             MetaTitle = n.Metatitle,
                             Image = n.Image,
                             Description = n.Description,
                             CreatedDate = n.CreatedDate,
                             CreatedBy = n.CreatedBy,
                             ID = n.ID
                         });
            return model.OrderByDescending(n => n.CreatedDate).ToPagedList(page, pageSize);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        #endregion


    }
}
