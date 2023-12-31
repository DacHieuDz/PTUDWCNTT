﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace PTUDW.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SuppliersDAO suppliersDAO = new SuppliersDAO();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(suppliersDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
            }
            return View(suppliers);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View();
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                // xu ly tu dong cho cac truong: Slug, createAt, by, updateAt/By, Order

                // xu ly tu dong cho CreateAt
                suppliers.CreateAt = DateTime.Now;
                // xu ly tu dong cho UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                // xu ly tu dong cho Order
                suppliers.CreateBy = Convert.ToInt32(Session["UserId"]);
                // xu ly tu dong cho UpdateAt
                suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
                // xu ly tu dong cho Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                } else
                {
                    suppliers.Order += 1;
                }
                // Xu ly tu dong cho slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);
                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + suppliers.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Img = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //ket thuc phan upload hinh anh
                // chen dong du lieu thanh cong
                suppliersDAO.Insert(suppliers);
                TempData["message"] = new XMessage("success", "Tạo mới nhà cung cấp thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                // xu ly tu dong cho cac truong: Slug, createAt, by, updateAt/By, Order

                // xu ly tu dong cho UpdateAt
                suppliers.UpdateAt = DateTime.Now;
                // xu ly tu dong cho Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                // Xu ly tu dong cho slug
                suppliers.Slug = XString.Str_Slug(suppliers.Name);

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                string PathDir = "~/Public/img/supplier";
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + suppliers.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Img = imgName;
                        //upload hinh
                        
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //ket thuc phan upload hinh anh

                // Xu ly cho muc xoa hinh anh
                if(suppliers.Img != null)
                {
                    string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Img);
                    System.IO.File.Delete(DelPath);
                }
                // Cap nhat mau tin vao DB
                suppliersDAO.Update(suppliers);
                TempData["message"] = new XMessage("success", "Cập nhật nhà cung cấp thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = suppliersDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suppliers suppliers = suppliersDAO.getRow(id);
            //xu ly cho phan upload hinh anh
            var img = Request.Files["img"];//lay thong tin file
            string PathDir = "~/Public/img/supplier";
            //xoa mau tin ra khoi DB
            if (suppliersDAO.Delete(suppliers) == 1)
            {
                //Xu ly cho muc xoa hinh anh
                if (suppliers.Img != null)
                {
                    string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Img);
                    System.IO.File.Delete(DelPath);
                }
            }
            // thong bao xoa mau tin thanh cong
            TempData["message"] = new XMessage("success", "Xóa nhà cung cấp thành công");
            return RedirectToAction("Trash");
        }

        // phat sinh them 1 so action: Status, Trash, Deltrash, Undo. Copy tu category controller
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Suppliers suppliers = suppliersDAO.getRow(id);

            if (suppliers == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                // kiem tra trang thai cua status; neu hien tai la 1 -> 2 va nguoc lai
                suppliers.Status = (suppliers.Status == 1) ? 2 : 1;

                // cap nhat gia tri cho UpdateAt
                suppliers.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                suppliersDAO.Update(suppliers);

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
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Suppliers suppliers = suppliersDAO.getRow(id);

            if (suppliers == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            else
            {
                // chuyen doi trang thai cua status tu 1,2 -> 0: va nguoc lai
                suppliers.Status = 0;

                // cap nhat gia tri cho UpdateAt
                suppliers.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                suppliersDAO.Update(suppliers);

                // thong bao thanh cong
                TempData["message"] = TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }
        // TRASH
        // GET: Admin/Supplier/Trash
        public ActionResult Trash()
        {
            return View(suppliersDAO.getList("Trash"));
        }

        // RECOVER
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }

            // tim row co id == id cua loai SP can thay doi Status
            Suppliers suppliers = suppliersDAO.getRow(id);

            if (suppliers == null)
            {
                // thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi mẫu tin thất bại");
                return RedirectToAction("Index");
            }
            else
            {
                // chuyen doi trang thai cua Status tu 0 -> 2: khong xuat ban
                suppliers.Status = 2;

                // cap nhat gia tri cho UpdateAt
                suppliers.UpdateAt = DateTime.Now;

                // cap nhat lai DB
                suppliersDAO.Update(suppliers);

                // thong bao phuc hoi du lieu thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

                // tra ket qua ve Index
                return RedirectToAction("Index");
            }
        }
    }
}
