using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;

        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }

        //Lấy trạng thái contact
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(n => n.Status == true);
        }

        //Thêm feedback
        public int InsertFeedback(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
