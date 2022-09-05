using System.ComponentModel.DataAnnotations;

namespace EonixWebApi.ApplicationCore.Entities
{
    public class PersonDto : EntityBase
    {
        [Required(ErrorMessage = "Person lastname is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Person first name is a required field.")]
        public string FirstName { get; set; }
    }
}
