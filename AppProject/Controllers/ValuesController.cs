using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AppProject.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Entities _db;
        public ValuesController(Entities Db) {
            _db = Db;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {

            //var gety = _Db.Employees;
            //Employee emp = new Employee();
            //emp.Name = "Test2";
            //emp.JoiningDate = DateTime.Now;
            //emp.Salary = "5000";
            //emp.Department = "KT";
            //emp.CreatedOn = DateTime.Now;
            //emp.IsDeleted = false;
            //Random generator = new Random();
            //String r = generator.Next(0, 999999).ToString("D6");
            //emp.EmployeeCode = r;

            //_Db.Employees.Add(emp);
            //_Db.SaveChanges();
            var EmployeeList= _db.Employees.ToList();
            return EmployeeList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("api/Values/saveEmployee")]
        public IActionResult SaveEmployee(string name)
        {
            Employee model = new Employee();
            model.Name = name;
            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;
            
            _db.Employees.Add(model);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Route("api/Values/saveEmployee1")]
        public IActionResult SaveEmployee1(Employee model)
        {


            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            _db.Employees.Add(model);
            _db.SaveChanges();
            return Ok();
        }
    }
}
