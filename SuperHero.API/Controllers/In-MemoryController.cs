using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.API.Models;
using System.Linq;
using System.Net;

namespace SuperHero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class In_MemoryController : ControllerBase
    {
        private readonly List<SuperHeroModel_InMemory> model;
        private readonly ILogger<In_MemoryController> _logger;
        public In_MemoryController(ILogger<In_MemoryController> logger)
        {

            model = new List<SuperHeroModel_InMemory>
            {
                new SuperHeroModel_InMemory{ Id = 1, Name ="deji", FirstName ="fati", LastName="fri", Place="Kano", Department="IT", Gender="Male",Salary=9837.36},
                new SuperHeroModel_InMemory{ Id = 2, Name ="bola", FirstName ="deit", LastName="stepho", Place="brazil", Department="IT", Gender="Male",Salary=9087.65},
                new SuperHeroModel_InMemory{ Id = 3, Name ="smaoade", FirstName ="taju", LastName="samson", Place="mali", Department="QA", Gender="Female",Salary=6524.3},
                new SuperHeroModel_InMemory{ Id = 4, Name ="prio", FirstName ="longe", LastName="jindo", Place="london", Department="HR", Gender="Female",Salary=56432.54},
                new SuperHeroModel_InMemory{ Id = 5, Name ="deji", FirstName ="fati", LastName="fri", Place="Kano", Department="IT", Gender="Male",Salary=9857.36},
                new SuperHeroModel_InMemory{ Id = 6, Name ="bola", FirstName ="deit", LastName="stepho", Place="brazil", Department="IT", Gender="Male",Salary=23453.23},
                new SuperHeroModel_InMemory{ Id = 7, Name ="smaoade", FirstName ="taju", LastName="samson", Place="mali", Department="QA", Gender="Female",Salary=654.43},
                new SuperHeroModel_InMemory{ Id = 8, Name ="prio", FirstName ="longe", LastName="jindo", Place="london", Department="HR", Gender="Female",Salary=565.34},
                  new SuperHeroModel_InMemory{ Id = 6, Name ="bola", FirstName ="deit", LastName="stepho", Place="brazil", Department="IT", Gender="Female",Salary=2345.13},
                new SuperHeroModel_InMemory{ Id = 7, Name ="smaoade", FirstName ="taju", LastName="samson", Place="mali", Department="QA", Gender="Male",Salary=6543.23},
                new SuperHeroModel_InMemory{ Id = 8, Name ="prio", FirstName ="longe", LastName="jindo", Place="london", Department="HR", Gender="Male",Salary=5654.23}

            };
            _logger = logger;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroModel_InMemory>> Get([FromRoute] int id)
        {

            var data = model.Find(x => x.Id == id);


            return data switch
            {
                null => NotFound(new { data = "id not exist", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHero()
        {

            var data = model.OrderBy(x => x.Name)
                            .ThenBy(x => x.FirstName)
                            .ThenBy(x => x.LastName)
                            .ToList();


            return data switch
            {
                null => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }
        [HttpGet("dept/{dept}")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartmentId([FromRoute]string dept)
        {

            var data = model.Where(x => x.Department == dept.ToUpper());
                             


            return data.Count() switch
            {
                < 1  => NotFound(new { data = "record null",dataCount=data.Count() , code = "99", description = "fail" }),

                _ => Ok(new { data, dataCount = data.Count(), code = "00", description = "succes" })
            };



        }
        [HttpGet("groupByDepartment")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartment()
        {

            var data = model.GroupBy(x => x.Department);
            List<string> Counts = new List<string>();

            foreach (var group in data)
            {

                Counts.Add(group.Key + ":" + group.Count().ToString());


            }

            return data.Count() switch
            {
                < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data = new { Counts,data}, code = "00", description = "succes" })
            };



        }
        [HttpGet("groupByDepartmentCount")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartments()
        {

            var result = model.GroupBy(x => x.Department).AsEnumerable();
            List<string> data=new List<string>();
            List<string> child = new List<string>();
            foreach (var group in result)
            {

                data.Add(group.Key + ":" + group.Count().ToString());

                foreach (var item in group)
                {
                    child.Add("Name"+": "+item.Name +"-"+item.FirstName +" Dept:"+item.Department);
                }

            }


            return result.Count() switch
            {
                < 1 => NotFound(new { data = "record null",  code = "99", description = "fail" }),

                _ => Ok(new {data=new {data,child} , code = "00", description = "succes" })
            };



        }

        [HttpGet("groupByDepartmentCount/{gender}")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartmentgender([FromRoute]string gender)
        {

            var result = model.GroupBy(x => x.Department).AsEnumerable();
            List<string> data = new List<string>();
            foreach (var group in result)
            {

                data.Add(group.Key + ":" + group.Count(x=>x.Gender.ToUpper()==gender.ToUpper()).ToString());



            }


            return result.Count() switch
            {
                < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }

        [HttpGet("groupByDepartment/maxSalary")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartmentMaxSalary()
        {

            var result = model.GroupBy(x => x.Department).AsEnumerable();
            List<string> data = new List<string>();
            foreach (var group in result)
            {

                data.Add(group.Key + ":" + group.Max(x=>x.Salary));



            }


            return result.Count() switch
            {
                < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }
        [HttpGet("groupByDepartment/sumSalary")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartmentSumSalary()
        {

            var result = model.GroupBy(x => x.Department).AsEnumerable();
            List<string> data = new List<string>();
            foreach (var group in result)
            {

                data.Add(group.Key + ":" + group.Sum(x => x.Salary));



            }


            return result.Count() switch
            {
                < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }


        [HttpGet("groupByDepartmentandgender")]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> GetAllHeroByDepartmentandgender()
        {
            _logger.LogInformation("Suphero logging....");
            var data = model.AsEnumerable()
                            .Where(s=>s.Salary > 5000)
                            .OrderByDescending(s=>s.Salary)
                            .Take(5) //.TakeLast(5)  .TakeWhile()
                            .GroupBy(x => new { x.Department, x.Gender }) //.GroupBy(x=> new { x.Department})
                            .OrderBy(g => g.Key.Department)
                            .ThenBy(g => g.Key.Gender)
                            .Select(g => new
                            {
                                g.Key.Department,
                                g.Key.Gender,
                                count = g.Count(),
                                TotalSalary = g.Sum(x => x.Salary),
                                AverageSalary = g.Average(x => x.Salary),
                                MaxSalary = g.Max(x => x.Salary),
                                MinSalary = g.Min(x => x.Salary),
                                Employee = g.OrderBy(g => g.Name)
                                          .ThenBy(g => g.FirstName)
                                          .Select(d => new
                                          {
                                              d.Department,
                                              d.Gender,
                                              d.Place,
                                              Fullname = d.FirstName + " " + d.LastName + " " + d.Name,
                                              d.Salary,

                                              Bonus = (d.Salary * .10)
                                          })

                            });
          
            return data.Count() switch
            {
                < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }


        [HttpGet("All_Department_With_or_without_Employee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                #region MyRegion

                #endregion


                _logger.LogInformation("Suphero logging....");
                var data = Department.GetAllDepartment() //outer sequence this output all the outer property even without innser sequence(Left outer join in sql script)
                                     .OrderBy(g => g.Name)
                                     .GroupJoin(Employee.GetAllEmployee(),//.Where(a=>a.Salary > 500), //inner sequence
                                     d => d.ID,
                                     e => e.DepartmentID,
                                     (department,employees) => new
                                     {
                                        

                                         Department = department.Name,

                                         #region TO achieve below we need to set aggregate function to 0 for Department without Employee

                                         Sum = employees.Sum(e => e?.Salary),
                                         Average = employees.Average(e => e?.Salary) == null ? 0 : employees.Average(e => e.Salary),
                                         MaximumSalary = employees.Max(e => e?.Salary) == null ? 0 : employees.Max(e => e.Salary),
                                         MinimumSalary =employees.Min(e => e?.Salary)   == null ? 0 : employees.Min(e => e.Salary),
                                         TotalCount = employees.Count(),

                                         #endregion

                                         Emmployee = employees.OrderBy(s => s.Salary)
                                                              .ThenBy(s => s.Name)
                                                              .Select(s => new
                                                              {
                                                                  s.ID,
                                                                  s.Salary,
                                                                  s.Name,
                                                                  Bonus = s.Salary * .10
                                                              })


                                     }
                                     );

                return data.Count() switch
                {
                    < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                    _ => Ok(new { data, code = "00", description = "succes" })
                };

            }
            catch (Exception ex)
            {

                return StatusCode(500,new { ex.Message,code="500",description="fail"});
            }

        }

        [HttpGet("All_Employee_With_or_without_Department")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                #region MyRegion

                #endregion


                _logger.LogInformation("Suphero logging....");
                var data = Employee.GetAllEmployee() //outer sequence this output all the outer property even without inner sequence (Left outer join in sql script)
                                     .OrderBy(g => g.Name)
                                     .GroupJoin(Department.GetAllDepartment(),//.Where(a=>a.Salary > 500), //inner sequence
                                    e => e.DepartmentID,
                                     d => d.ID,

                                     (employees, department) => new
                                     { 
                                             employees.ID,
                                             employees.Name,
                                             employees.Salary,
                                             Department = employees?.DepartmentID==0 ? "no department" : department.FirstOrDefault(d =>d.ID==employees.DepartmentID).Name,


                                     }
                                     );

                return data.Count() switch
                {
                    < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                    _ => Ok(new { data, code = "00", description = "succes" })
                };

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { ex.Message, code = "500", description = "fail" });
            }

        }





        [HttpGet("only_Department_With_Employee")]
        public async Task<IActionResult> GetEmployeeonly()
        {
            try
            {
                _logger.LogInformation("Suphero logging....");
                var data = Department.GetAllDepartment() //outer sequence this output only the outer property that has  innser sequence
                                     .OrderBy(g => g.Name)
                                     .Join(Employee.GetAllEmployee(),//.Where(a=>a.Salary > 500), //inner sequence
                                     d => d.ID,
                                     e => e.DepartmentID,
                                     (department, employees) => new
                                     {
                                         Dept = department.Name,
                                         Staff = employees.Name

                                         //Department = department.Name,
                                         //#region TO achieve below we need to remove Department without Employee
                                         //Sum = employees.Sum(e => e.Salary),
                                         //Average = employees.Average(e => e.Salary),
                                         //MaximumSalary = employees.Max(e => e.Salary),
                                         //MinimumSalary = employees.Min(e => e.Salary),
                                         //TotalCount = employees.Count(),
                                         //#endregion
                                         //Emmployee = employees.OrderBy(s => s.Salary)
                                         //                     .ThenBy(s => s.Name)
                                         //                     .Select(s => new
                                         //                     {
                                         //                         s.ID,
                                         //                         s.Salary,
                                         //                         s.Name,
                                         //                         Bonus = s.Salary * .10
                                         //                     })


                                     }
                                     );

                return data.Count() switch
                {
                    < 1 => NotFound(new { data = "record null", code = "99", description = "fail" }),

                    _ => Ok(new { data, code = "00", description = "succes" })
                };

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { ex.Message, code = "500", description = "fail" });
            }


        }



        [HttpGet("app")]
        public async Task<IActionResult> Index()
        {
            int[] numArray = { 23, 7, 8,10,43 ,50,60};
            var result = numArray.Where(a => a % 2 == 0)
                                 .OrderByDescending(a => a)
                                 .Take(2);



            return Ok(new { data = result });
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> POST([FromBody] SuperHeroModel_InMemory superHeroModel)
        {


           
            model.Add(superHeroModel); 
           
            return Created("",new {superHeroModel});

        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> PuT([FromBody] SuperHeroModel_InMemory superHeroModel)
        {
            var m = model.Find(model => model.Id == superHeroModel.Id);

            if (m == null) return NotFound(new { data = "user doesnt exist", code = "99", description = "fail" });

            m.Name = superHeroModel.Name;
            m.FirstName = superHeroModel.FirstName;
            m.LastName = superHeroModel.LastName;
            m.Place = superHeroModel.Place;
            m.Gender = superHeroModel.Gender;
            m.Department=superHeroModel.Department;
            m.Salary = superHeroModel.Salary;


            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHeroModel_InMemory>>> Delete([FromQuery] int id)
        {
           
            var m = model.Find(model => model.Id == id);
            if (m == null) return NotFound(new { data = "user doesnt exist", code = "99", description = "fail" });

            model.Remove(m);
            return NoContent();

        }


    }
}
