using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace PTUDW.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        // INDEXS
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoriesDAO.getList("Index"));
        }

        // DETAILS
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại loại sản phẩm");
                return HttpNotFound();
            }
            return View(categories);
        }

        // CREATE
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                // Xu ly tu dong cho: CreateAt
                categories.CreateAt = DateTime.Now;
                
                // Xu ly tu dong cho: UpdateAt
                categories.UpdateAt = DateTime.Now;

                // Xu ly tu dong cho: ParentId
                if(categories.ParentId == null)
                {
                    categories.ParentId = 0;
                }

                // Xu ly tu dong cho: Order
                if(categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                categories.Slug = XString.Str_Slug(categories.Name);
                // them dong du lieu cho DB
                categoriesDAO.Insert(categories);
                // thong bao thanh cong
                TempData["message"] = new XMessage("success", "Tạo mới loại sản phẩm thành công");
                // tro ve trang index
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
        }

        // EDIT
        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                // Xu ly tu dong Slug
                categories.Slug = XString.Str_Slug(categories.Name);

                // Xu ly tu dong ParentId
                if (categories.ParentId == null)
                {
                    categories.ParentId = 0;
                }

                // Xu ly tu dong cho: Order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                // Xu ly tu dong UpdateAt
                categories.UpdateAt = DateTime.Now;

                // Cap nhat mau tin
                categoriesDAO.Update(categories);

                TempData["message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
        }

        // DELETE
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Xóa mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoriesDAO.getRow(id);
            categoriesDAO.Delete(categories);

            // thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash");
        }

        // STATUS
        // GET: Admin/Category/Status/5

        public ActionResult Status(int? id)
        {
            if(id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Categories categories = categoriesDAO.getRow(id);

            if (categories == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                // kiem tra trang thai cua status; neu hien tai la 1 -> 2 va nguoc lai
                categories.Status = (categories.Status == 1) ? 2 : 1;

                // cap nhat gia tri cho UpdateAt
                categories.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                categoriesDAO.Update(categories);

                // thong bao thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }

        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mãu tin");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Categories categories = categoriesDAO.getRow(id);

            if (categories == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mãu tin");
                return RedirectToAction("Index");
            }
            else
            {
                // chuyen doi trang thai cua status tu 1,2 -> 0: va nguoc lai
                categories.Status = 0;

                // cap nhat gia tri cho UpdateAt
                categories.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                categoriesDAO.Update(categories);

                // thong bao thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }
        // TRASH
        // GET: Admin/Category/Trash
        public ActionResult Trash()
        {
            return View(categoriesDAO.getList("Trash"));
        }

        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Categories categories = categoriesDAO.getRow(id);

            if (categories == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                // chuyen doi trang thai cua Status tu 0 -> 2: khong xuat ban
                categories.Status = 2;

                // cap nhat gia tri cho UpdateAt
                categories.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                categoriesDAO.Update(categories);

                // thong bao phuc hoi du lieu thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }
    }
}
