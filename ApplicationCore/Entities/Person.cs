using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EonixWebApi.ApplicationCore.Entities
{
    public class Person : EntityBase
    {

        [Required(ErrorMessage = "Person lastname is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Person first name is a required field.")]
        public string FirstName { get; set; }
    }
}
