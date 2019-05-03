using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AppProject.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    [Authorize]
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
        [HttpPost,Authorize]
        [Route("api/Values/saveEmployee1")]
        public IActionResult SaveEmployee1(Employee model)
        {


            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            _db.Employees.Add(model);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Route("EditEmployee")]
        public IActionResult EditEmployee(Employee model)
        {
            var getEmployee = _db.Employees.Where(x => x.id == model.id).FirstOrDefault();
            getEmployee.Name = model.Name;
            getEmployee.ModifiedOn = DateTime.Now;
            getEmployee.JoiningDate = model.JoiningDate;
            getEmployee.Salary = model.Salary;
            getEmployee.Department = model.Department;
            _db.Update(getEmployee);
            _db.SaveChanges();
            return Ok();

        }
        [HttpPost]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(Guid Id)
        {
            var getEmployee = _db.Employees.Where(x => x.id == Id).FirstOrDefault();
            if (getEmployee == null)
            {
                return Ok();
            }
            _db.Remove(getEmployee);
            _db.SaveChanges();
            return Ok();

        }
        [HttpPost]
        [Route("UploadFile")]
        public IActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
