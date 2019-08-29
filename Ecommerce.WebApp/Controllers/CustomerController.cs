using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Helpers;
using System.Web.Mvc;
using Ecommerce.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Controller = Microsoft.AspNetCore.Mvc.Controller;



namespace Ecommerce.WebApp.Controllers
{
    public class CustomerController: Controller
    {
        private CustomerRepository _customerRepository;
        private EcommerceDbContext _db;

        public CustomerController()
        {
            _customerRepository = new CustomerRepository();
            _db = new EcommerceDbContext();
        }
        bool status;
        string message;
        public async Task<IActionResult> Index()
        {
            return View(await _db.Customers.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = _customerRepository.Add(model);
                if (isAdded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Microsoft.AspNetCore.Mvc.Bind("Id,Name,Address,LoyaltyPoint")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(customer);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Movies/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CustomerExists(int id)
        {
            return _db.Customers.Any(e => e.Id == id);
        }
        //public IActionResult GetByAjax()
        //{
        //    var var = _customerRepository.GetAll();

        //    var MDirectorList = Mapper.Map<List<Customer>>(var);
        //    return Json(new {  data = MDirectorList }/*, JsonRequestBehavior.AllowGet*/);
        //   // return new JsonResult { Data = MDirectorList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //   // return View("Index");

        //}

        //// ********** TEST END *****************//

        //public IActionResult GetbyID(int id)
        //{



        //    var var = _customerRepository.GetById(id);


        //    var result = Mapper.Map<Customer>(var);


        //    return Json(new {result}/*, JsonRequestBehavior.AllowGet*/);
        //   // return View("Index");
        //}

        //[Microsoft.AspNetCore.Mvc.HttpPost]

        //public JsonResult Edit(Customer vmObj)
        //{

        //    var result = Mapper.Map<Customer>(vmObj);




        //    bool isUpdated = _customerRepository.Update(result);



        //    if (isUpdated)
        //    {

        //        status = true;
        //         message = "Update Successfully!!";
        //    }
        //    else
        //    {

        //        status = false;

        //        message = "Error In Update!!";
        //    }


        //    //return new JsonResult { Data = new { status = status, message = message } };
        //    return Json ( new { status = status, message = message });
        //    //return View("Index");
        //}

        //public IActionResult Delete(int id)
        //{
        //    Customer var = _customerRepository.GetById(id);

        //    bool isDeleted = _customerRepository.Remove(var);

        //    message = "This Item Deleted Successfully!!";

        //     return new JsonResult ( new { status = isDeleted, message = message } );
        //    //return View("Index");


        //}

    }
}
