using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //List<Customer> customers = new List<Customer>()
        //  {
        //      new Customer {Id=1,name="Lucas Gonzalez"},
        //      new Customer { Id=2,name ="Hernan Gonzalez"}
        //  };
        // GET: Customers

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        public ActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes,
                Header = "New Customer"
             };
        

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if(customer.Id==0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //official mvc tutorial
                //open security holes
                //updates all properties of given entitiy
                //malicious users can update any data
                //TryUpdateModel(customerInDb);
                customerInDb.name = customer.name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeID = customer.MembershipTypeID;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ViewResult Index()
        {
            //replaced by query
            //  var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();//View(customers);

        }
        //public ActionResult Index()
        //{

        //  //  var customers = new List<Customer>()
        //  //{
        //  //    new Customer {Id=1,name="Lucas Gonzalez"},
        //  //    new Customer { Id=2,name ="Hernan Gonzalez"}
        //  //};

        //    var viewModel = new CustomersListViewModel
        //    {
        //        Customers = customers
        //    };



        //    return View(viewModel);
        //}
        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id )
        {
            //Customer cust = customers.Find(s => s.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(s => s.Id == id);

            if (customer == null)
                return HttpNotFound();
            return View( customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList(),
                Header="Edit Customer"
            };


            return View("CustomerForm",viewModel);
        }

        //public IEnumerable<Customer> GetCustomers()
        //{
        //    return  new List<Customer>()
        //  {
        //      new Customer {Id=1,name="Lucas Gonzalez"},
        //      new Customer { Id=2,name ="Hernan Gonzalez"}
        //  };
        //}
    }
}