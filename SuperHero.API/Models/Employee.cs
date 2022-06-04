namespace SuperHero.API.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double Salary { get; set; }
        public int DepartmentID { get; set; }

        public static List<Employee> GetAllEmployee()
        {
            return new List<Employee>()
            {
                new Employee(){ ID = 1, Name ="Ope", DepartmentID=1, Salary=345.67},
                new Employee(){ ID = 2, Name ="Abe",DepartmentID=2,Salary=546.66},
                new Employee{ ID = 3, Name ="Wale",DepartmentID=3,Salary=7689.90},
                   new Employee(){ ID = 4, Name ="Edun", DepartmentID=1,Salary=123.67},
                new Employee(){ ID = 5, Name ="Aruna",DepartmentID=2,Salary=877.87},
                new Employee{ ID = 6, Name ="Omo",DepartmentID=3,Salary=654.78},
                   new Employee(){ ID =7, Name ="TOpe", DepartmentID=1,Salary=980.76},
                new Employee(){ ID =8, Name ="Abiola",DepartmentID=2,Salary=677.88},
                new Employee{ ID =9, Name ="Gabrel",DepartmentID=3,Salary=564.67},
                   new Employee(){ ID = 10, Name ="Kings", DepartmentID=1,Salary=656.77},
                new Employee(){ ID =11, Name ="Longe",DepartmentID=2,Salary=5345.65},
                new Employee{ ID =12, Name ="Jide",DepartmentID=3,Salary=456.89},
                 new Employee{ ID =13, Name ="Jide",Salary=456.89},
                      new Employee{ ID =14, Name ="Wale",DepartmentID=3,Salary=564.67},
                            new Employee{ ID =15, Name ="Fati",Salary=564.67}
            };
        }
    }
}
