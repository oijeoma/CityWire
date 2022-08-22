using App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Repository
{
    public class CustomerRepository : ICustomer
    {
       
       
        public void AddCustomer(Customer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
