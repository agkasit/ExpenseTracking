using ExpenseTracking.Models;
using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace ExpenseTracking.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AuthContext _context = null;

        public SupplierController()
        {
            _context = new AuthContext();
        }

        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string id)
        {
            var model = new Supplier();

            if (id != "0")
            {
                int supplier_id = Convert.ToInt32(Encode64.DecodeFrom64(id));
                model = _context.Supplier.Find(supplier_id);
                model.supplier_id = Encode64.EncodeTo64(model.id.ToString());
            }

            return View(model);
        }


        public ActionResult SupplierUpdate(Supplier param)
        {
            try
            {

                if (string.IsNullOrEmpty(param.supplier_id))
                {
                   _context.Supplier.Add(param);
                    _context.SaveChanges();

                    return Json(new
                    {
                        status = true,
                        message = "complete"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int supplier_id = Convert.ToInt32(Encode64.DecodeFrom64(param.supplier_id));

                    var updateModel = _context.Supplier.Find(supplier_id);
                    updateModel.supplier_name = param.supplier_name;
                    updateModel.tax_id = param.tax_id;
                    updateModel.contact = param.contact;
                    updateModel.phone = param.phone;
                    updateModel.email = param.email;
                    updateModel.bank_id = param.bank_id;
                    updateModel.account_no = param.account_no;
                    updateModel.account_name = param.account_name;
                    updateModel.setUpdateDate();
                    _context.Entry(updateModel).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(new
                    {
                        status = true,
                        message = "updated"
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }


        }

        //GetListBanks
        [HttpGet]
        public ActionResult GetListBanks()
        {

            try
            {
                var query = (from b in _context.Bank
                             select new
                             {
                                 id = b.id.ToString(),
                                 text = b.bank_name
                             });

                var jsonResult = Json(new
                {
                    status = true,
                    message = "complete",
                    total = query.Count(),
                    data = query.ToList()
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            catch (Exception ex)
            {
                var jsonResult = Json(new
                {
                    status = false,
                    message = ex.Message,
                    total = 0,
                    data = ""
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }

        }

        

        [HttpGet]
        public ActionResult GetListSupplier()
        {

            try
            {
                var query = (from b in _context.Supplier
                             select new
                             {
                                 id = b.id.ToString(),
                                 text = b.supplier_name
                             });

                var jsonResult = Json(new
                {
                    status = true,
                    message = "complete",
                    total = query.Count(),
                    data = query.ToList()
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            catch (Exception ex)
            {
                var jsonResult = Json(new
                {
                    status = false,
                    message = ex.Message,
                    total = 0,
                    data = ""
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }

        }

        public ActionResult ListSupplierForDatatables(ExpenseModel param)
        {
            try
            {
                //int shop_id = Convert.ToInt32(Encode64.DecodeFrom64(param.shop_id));
                var query = (from s in _context.Supplier
                             select new
                             {
                                 supplier = s
                             }).AsEnumerable();


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x => x.supplier.supplier_name.ToLower().Contains(param.sSearch.ToLower()));
                }

                query = query.OrderBy(x => x.supplier.supplier_name);
                var list = query.AsEnumerable().Select((v, index) => new
                {
                    id = v.supplier.id.ToString(),
                    supplier_id = Encode64.EncodeTo64(v.supplier.id.ToString()),
                    supplier_name = v.supplier.supplier_name,
                    v.supplier.tax_id,
                    v.supplier.contact,
                    v.supplier.phone,
                    v.supplier.email,
                    rowid = index + 1
                });


                int total = list.Count();
                list = list.OrderBy(x => x.rowid).Skip(param.iDisplayStart).Take(param.iDisplayLength);

                var jsonResult = Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = total,
                    aaData = list.ToList()
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            catch (Exception ex)
            {
                var jsonResult = Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = ""
                }, JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
        }


    }
}