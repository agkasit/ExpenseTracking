using ExpenseTracking.Models;
using Repository.Context;
using System;
using System.Collections.Generic;
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

        public ActionResult Create()
        {
            return View();
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
                                 s.id,
                                 s.supplier_name,
                                 s.tax_id,
                                 s.contact,
                                 s.phone,
                                 s.email
                             });


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x => x.supplier_name.ToLower().Contains(param.sSearch.ToLower()));
                }

                int total = query.Count();
                query = query.OrderBy(x => x.id).Skip(param.iDisplayStart).Take(param.iDisplayLength);

                var jsonResult = Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = query.Count(),
                    iTotalDisplayRecords = total,
                    aaData = query.ToList()
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