using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Web.Models;

public partial class Eventsponsorsimage
{
    public long Id { get; set; }

    public long EventId { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
    public string SponsorImage { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
