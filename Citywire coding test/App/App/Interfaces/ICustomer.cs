using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Interfaces
{
    public interface ICustomer : IDisposable
    {
            void AddCustomer(Customer customer);
    }
}
