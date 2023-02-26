using EpsilonDbContext.DbContexts;
using EpsilonDbContext.Models;
using EpsilonUI.Dtos;
using EpsilonUI.Dtos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EpsilonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly EpsilonContext _epsilonContext;

        public CustomersController(EpsilonContext epsilonContext)
        {
            _epsilonContext = epsilonContext;
        }

        [HttpGet("get_customers/{offset}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(int offset = 0)
        {
            try
            {
                var results = await _epsilonContext.Customers.OrderBy(i => i.Id).Skip(offset).Take(5).ToListAsync();
                return results;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost("create_customer")]
        public ActionResult<IEnumerable<Customer>> CreateCustomer(Customer customer)
        {
            try
            {
                _epsilonContext.Customers.Add(customer);
                _epsilonContext.SaveChanges();
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating customer.");
            }
        }

        [HttpPut("edit_customer")]
        public ActionResult<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                var customerFound = _epsilonContext.Customers.FirstOrDefault(item => item.Id == customer.Id);

                if (customerFound != null)
                {
                    _epsilonContext.Entry(customerFound).CurrentValues.SetValues(customer);
                    _epsilonContext.SaveChanges();
                    return Ok(customer);
                }
                else
                {
                    return Ok("Can not find the Customer...");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating customer.");
            }


        }

        [HttpPost("delete_customer")]
        public ActionResult<Customer> DeleteCustomer(Customer customer)
        {
            try
            {
                var customerFound = _epsilonContext.Customers.FirstOrDefault(item => item.Id == customer.Id);

                if (customerFound != null)
                {
                    _epsilonContext.Remove(customerFound);
                    _epsilonContext.SaveChanges();
                    return Ok(customer);
                }
                else
                {
                    return Ok("Can not find the Customer...");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer.");
            }

        }

        [HttpGet("print_person_name")]
        public void printCompanyPersonName()
        {
            CompanyPerson? companyPerson = new CompanyPerson();

            Manager manager = new Manager() { Name = "NESTOR" };
            Employee employee = new Employee() { Name = "nestor" };

            companyPerson.printCompanyPersonName(manager);
            companyPerson.printCompanyPersonName(employee);

        }

        

    }
}
