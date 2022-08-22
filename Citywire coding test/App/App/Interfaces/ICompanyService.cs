using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Interfaces
{
    public interface ICompanyService
    {
        Company GetCompany(int companyId);
    }
}
