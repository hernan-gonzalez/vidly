﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers(string query=null)
        {
            //var customerDtos= _context.Customers
            //    .Include(c=> c.MembershipType)

            var customersQuery = _context.Customers.Include(c => c.MembershipType);
            
            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.name.Contains(query));

            var customerDtos = customersQuery
              .ToList()
              .Select(Mapper.Map<Customer, CustomerDto>);

             
            return Ok(customerDtos);


        }



          

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id==id);

            if (customer == null)
                return NotFound();// throw new HttpResponseException(HttpStatusCode.NotFound);
            
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));

        }




        //POST /api/customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerdto)
        {

            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);


            var customer = Mapper.Map<CustomerDto, Customer>(customerdto);
            _context.Customers.Add(customer);

            _context.SaveChanges();

            customerdto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/"+ customer.Id),customerdto);//customerdto ;
        }


        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id==id);

            if (customerInDb == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto,customerInDb);

            return Ok(_context.SaveChanges());



        }



        // DELETE /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
          

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id==id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }
    }
}