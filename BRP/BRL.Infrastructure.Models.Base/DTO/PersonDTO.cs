
namespace BRL.Infrastructure.Models.Base.DTO
{
    public class PersonDTO
    {
        public string? DocumentNumberPerson { get; set; }

        public string? NamePerson { get; set; }

        public string? LastNamePerson { set; get; }

        public string? CellPhonePerson { set; get; }

        public bool Active { set; get; }
    }
}
