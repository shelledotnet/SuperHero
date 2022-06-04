using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHero.API.Models
{
    public class SuperHeroModel_InMemory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public double Salary { get; set; } 



    }
}
