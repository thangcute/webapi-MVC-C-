using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: api/Employees
        [HttpGet]
        [Route("api/GetAllEmployees")]
        public List<Employee> GetAllEmployee()
        {
            return db.Employees.ToList();
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employees/5
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult DeleteEmployee(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Employees.Remove(employee);
        //    db.SaveChanges();

        //    return Ok(employee);
        //}
        [HttpGet]
        [Route("api/Employees/DeleteEmployee/{id}")]
        public List<Employee> DeleteEmployee(int id)
        {
            db.Employees.Remove(db.Employees.FirstOrDefault(e => e.EmployeeID == id));
            db.SaveChanges();
            return db.Employees.ToList();

        }
        [HttpGet]
        [Route("api/Employees/Search")]
        public List<Employee> Search(string FullName)
        {
            using (ProjectDBEntities db = new ProjectDBEntities())
            {
                try
                {
                    return db.Employees.Where(e => e.FullName.Contains(FullName)).ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}