using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Ecommerce.DatabaseContext;
using Ecommerce.DatabaseContext.Migrations;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CustomerRepository
    {
        private EcommerceDbContext _db;

        public CustomerRepository()
        {
            _db = new EcommerceDbContext();
        }
     

        public bool Add(Customer customer)
        {
            _db.Customers.Add(customer);
            return _db.SaveChanges() > 0;
        }
        public bool Remove(Customer customer)
        {
            _db.Customers.Remove(customer);
            return _db.SaveChanges() > 0;
        }

        public ICollection<Customer> GetAll()
        {
            return _db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }

        public bool Update(Customer customer)
        {
            _db.Entry(customer).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

    }
}
