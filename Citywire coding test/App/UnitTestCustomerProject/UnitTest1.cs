using App;
using App.Interfaces;
using App.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestCustomerProject
{
    [TestClass]
    public class UnitTest1
    {
        
       
        [TestMethod]
        public void AddCustomerTest()
        {          
            var companyServiceStub = new Mock<ICompanyService>();
            var customerRepoStub = new Mock<ICustomer>();
            var company = new Company
            {
                Id = 110,
                Classification = Classification.Gold,
                Name = "VeryImportantClient"

            };

          

            companyServiceStub.Setup(x => x.GetCompany(45)).Returns(company);
            
           
            var dob = DateTime.Now.AddYears(-27);
            var customer = new Customer
            {
                CreditLimit = 0,
                Company = company,
                DateOfBirth = dob,
                EmailAddress = "jfield@hotmail.com",
                Firstname = "Jake",
                Id = 110,
                HasCreditLimit = false,
                Surname = "Field"
            };
            customerRepoStub.Setup(x => x.AddCustomer(customer));
            var cust = new CustomerService(companyServiceStub.Object, customerRepoStub.Object);
            var value = cust.AddCustomer("Jake", "Field", "jfield@hotmail.com", dob, 45);
            Assert.IsTrue(value);
        }
        [TestMethod]
        public void AddBelowAgeCustomerTest()
        {
            var companyServiceStub = new Mock<ICompanyService>();
            var customerRepoStub = new Mock<ICustomer>();
            var company = new Company
            {
                Id = 110,
                Classification = Classification.Silver,
                Name = "VeryImportantClient"

            };

           
            

            companyServiceStub.Setup(x => x.GetCompany(66)).Returns(company);
            var dob = DateTime.Now.AddYears(-16);
            var customer = new Customer
            {
                CreditLimit = 0,
                Company = company,
                DateOfBirth = dob,
                EmailAddress = "jfield@hotmail.com",
                Firstname = "Jake",
                Id = 110,
                HasCreditLimit = false,
                Surname = "Field"
            };
            customerRepoStub.Setup(x => x.AddCustomer(customer));
            var cust = new CustomerService(companyServiceStub.Object, customerRepoStub.Object);
            var value = cust.AddCustomer("Jake", "Field", "jfield@hotmail.com", dob, 45);
            Assert.IsFalse(value);
        }
        [TestMethod]
        public void AddErromepisEmailCustomerTest()
        {
            var companyServiceStub = new Mock<ICompanyService>();
            var customerRepoStub = new Mock<ICustomer>();
            var company = new Company
            {
                Id = 110,
                Classification = Classification.Silver,
                Name = "VeryImportantClient"

            };




            companyServiceStub.Setup(x => x.GetCompany(66)).Returns(company);
            var dob = DateTime.Now.AddYears(-16);
            var customer = new Customer
            {
                CreditLimit = 0,
                Company = company,
                DateOfBirth = dob,
                EmailAddress = "jfield@hotmail.com",
                Firstname = "Jake",
                Id = 110,
                HasCreditLimit = false,
                Surname = "Field"
            };
            customerRepoStub.Setup(x => x.AddCustomer(customer));
            var cust = new CustomerService(companyServiceStub.Object, customerRepoStub.Object);
            var value = cust.AddCustomer("Jake", "Field", "jfieldhotmail.com", dob, 45);
            Assert.IsFalse(value);
        }
    }
}
