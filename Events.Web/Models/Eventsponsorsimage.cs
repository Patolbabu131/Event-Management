using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Web.Models;

public partial class Eventsponsorsimage
{
    [DisplayName("ID")]
    public long Id { get; set; }

    [DisplayName("Event Id")]
    public long EventId { get; set; }
    [NotMapped]

    [DisplayName("File")]
    public IFormFile File { get; set; }

    [DisplayName("Sponsor Image")]
    public string SponsorImage { get; set; } = null!;

    [DisplayName("Event")]
    public virtual Event Event { get; set; } = null!;
}
