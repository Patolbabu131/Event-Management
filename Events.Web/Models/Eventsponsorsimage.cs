using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Web.Models;

public partial class Eventsponsorsimage
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string SponsorImage { get; set; } = null!;
    [NotMapped]
    public IFormFile File { get; set; }
    
    public virtual Event Event { get; set; } = null!;
}
