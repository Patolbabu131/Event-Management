using System.ComponentModel;

namespace Events.Web.ViewModel
{
    public class UserViewModel
    {
        [DisplayName("ID")]
        public long Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; } = null!;

        [DisplayName("Designation")]
        public string Designation { get; set; } = null!;

        [DisplayName("Appointed On")]
        public DateTime? AppointedOn { get; set; }

        [DisplayName("Dutties")]
        public string Duties { get; set; } = null!;

        [DisplayName("Active")]
        public bool Active { get; set; }

        public string LoginName { get; set; } = null!;


        public string Role { get; set; } = null!;
    }
}
