namespace SuperHero.API.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string? Name { get; set; }



        public static List<Department> GetAllDepartment()
        {
            return new List<Department>()
            {
                new Department(){ ID = 1, Name ="IT"},
                new Department(){ ID = 2, Name ="HR"},
                  new Department(){ ID = 3, Name ="Risk"},
               new Department{ ID = 4, Name ="Payroll"}

            };
        }
    }
}
