using Repository.Context;
using ExpenseTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;
using Repository.Entities;
using System.Data.Entity;

namespace ExpenseTracking.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly AuthContext _context = null;

        public ExpenseController()
        {
            _context = new AuthContext();
        }

        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Create(string id) {

            var model = new Expense();
            
            if (id != "0") {
                int expense_id = Convert.ToInt32(Encode64.DecodeFrom64(id));
                model  = _context.Expense.Find(expense_id);
                model.submited = DateFormate.DateForDBToDatePicker(model.submited);
                model.due_date = DateFormate.DateForDBToDatePicker(model.due_date);
                model.paid_date = DateFormate.DateForDBToDatePicker(model.paid_date);
                model.expense_id = Encode64.EncodeTo64(model.id.ToString());
            }

            return View(model);
        }

        public ActionResult ExpenseUpdate(Expense param)
        {
            try {

                if (string.IsNullOrEmpty(param.expense_id))
                {
                    
                    param.submited = DateFormate.DatePickerToDateForDB(param.submited);
                    param.due_date = DateFormate.DatePickerToDateForDB(param.due_date);
                    param.paid_date = DateFormate.DatePickerToDateForDB(param.paid_date);
                    param.receipt_format = (param.get_receipt == true) ? param.receipt_format : string.Empty;
                    _context.Expense.Add(param);
                    _context.SaveChanges();

                    return Json(new
                    {
                        status = true,
                        message = "complete"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int expense_id = Convert.ToInt32(Encode64.DecodeFrom64(param.expense_id));

                    var updateModel = _context.Expense.Find(expense_id);
                    updateModel.submited = DateFormate.DatePickerToDateForDB(param.submited);
                    updateModel.category_id = param.category_id;
                    updateModel.supplier_id = param.supplier_id;
                    updateModel.description = param.description;
                    updateModel.amount = param.amount;
                    updateModel.due_date = DateFormate.DatePickerToDateForDB(param.due_date);
                    updateModel.paid_date = DateFormate.DatePickerToDateForDB(param.paid_date);
                    updateModel.withholding_tax = param.withholding_tax;
                    updateModel.get_receipt = param.get_receipt;
                    updateModel.receipt_format = (param.get_receipt == true ) ? param.receipt_format: string.Empty;
                    updateModel.setUpdateDate();
                    _context.Entry(updateModel).State = EntityState.Modified;
                    _context.SaveChanges();

                    return Json(new
                    {
                        status = true,
                        message = "updated"
                    }, JsonRequestBehavior.AllowGet);
                }

            } catch (Exception ex) {

                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
            
            
        }

        [HttpGet]
        public ActionResult GetListCategory()
        {

            try
            {
                var query = (from b in _context.Category
                             select new {
                                 id = b.id.ToString(),
                                 text = b.category_name
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

        public ActionResult ListExpenseForDatatables(ExpenseModel param)
        {
            try
            {
                //int shop_id = Convert.ToInt32(Encode64.DecodeFrom64(param.shop_id));
                var query = (from e in _context.Expense
                             join c in _context.Category on e.category_id equals c.id
                             join s in _context.Supplier on e.supplier_id equals s.id
                             //join b in _context.Bank on s.bank_id equals b.id
                             select new
                             {
                                 expense = e,
                                 category = c,
                                 supplier = s,
                                 //bank = b
                             });

                if (param.unpaid)
                {
                    query = query.Where(x => x.expense.paid_date == null || x.expense.paid_date == "");
                }

                if (param.not_get_receipt) {
                    query = query.Where(x => x.expense.get_receipt == false);
                }

                query = query.OrderBy(x => x.expense.submited);

                var list = query.AsEnumerable().Select((v, index) => new
                {
                    id = v.expense.id.ToString(),
                    expense_id = Encode64.EncodeTo64(v.expense.id.ToString()),
                    submited = DateFormate.DateForSortDate(v.expense.submited),
                    v.category.category_name,
                    v.supplier.supplier_name,
                    v.expense.description,
                    amount = String.Format("{0:0,0.00}", v.expense.amount),
                    due_date = DateFormate.DateForSortDate(v.expense.due_date),
                    paid_date = DateFormate.DateForSortDate(v.expense.paid_date),
                    receipt_format = v.expense.receipt_format,
                    v.expense.get_receipt,
                    update_date = v.expense.update_date,
                    rowid = index + 1
                });

                

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    list = list.Where(x => x.submited.ToLower().Contains(param.sSearch.ToLower()) || x.supplier_name.ToLower().Contains(param.sSearch.ToLower()));
                }

                int total = list.Count();
                

                list = list.OrderBy(x=>x.rowid).Skip(param.iDisplayStart).Take(param.iDisplayLength);


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