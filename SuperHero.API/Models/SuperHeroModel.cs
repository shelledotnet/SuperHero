using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHero.API.Models
{
    public class SuperHeroModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

    }
}
