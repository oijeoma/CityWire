using App.Interfaces;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        private ICompanyService _companyService;
        private ICustomer _customerRepository;
        public CustomerService(ICompanyService companyService, ICustomer customerRepository)
        {
            _companyService = companyService;
            _customerRepository = customerRepository;
        }

        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {

            try
            {

                if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
                {
                    return false;
                }

                if (!email.Contains("@") && !email.Contains("."))
                {
                    return false;
                }

                var now = DateTime.Now;
                int age = now.Year - dateOfBirth.Year;
                if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

                if (age < 21)
                {
                    return false;
                }

                //CompanyService companyService = new CompanyService();
                var company = _companyService.GetCompany(companyId);
                var customer = new Customer
                {
                    Company = company,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    Firstname = firstname,
                    Surname = surname
                };

                //var customerRepository = new CustomerRepository();


                if (company.Name == "VeryImportantClient")
                {
                    // Skip credit check
                    customer.HasCreditLimit = false;
                }
                else if (company.Name == "ImportantClient")
                {
                    // Do credit check and double credit limit
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        customer.CreditLimit = creditLimit;
                    }
                }
                else
                {
                    // Do credit check
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                }

                if (customer.HasCreditLimit && customer.CreditLimit < 500)
                {
                    return false;
                }
                _customerRepository.AddCustomer(customer);


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
