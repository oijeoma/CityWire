using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CompanyService
    {
        public Company GetCompany(int companyId)
        {
            var companyRepository = new CompanyRepository();
            var company = companyRepository.GetById(companyId);
            return company;

        }
    }
}
