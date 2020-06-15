using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineShop.Common;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCrendential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString ,page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        //Thêm mới người dùng
        //httpget tải trang
        [HttpGet]
        [HasCrendential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HasCrendential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pass;
                user.CreatedDate = DateTime.Now;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("","Thêm mới người dùng thất bại.");
                }
            }
            return View("Index");
        }

        //Cập nhật người dùng
        [HttpGet]
        [HasCrendential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [HasCrendential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pass = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pass;
                }
                user.CreatedDate = DateTime.Now;
                var result = dao.Update(user);
                if (result == true)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng thất bại.");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        [HasCrendential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            SetAlert("Xoá người dùng thành công", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCrendential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}