using Domain.Models;

namespace webapi.Models
{
    public class NewEmployee : Employee
    {
        public new int? Id { get; set; }
    }
}
