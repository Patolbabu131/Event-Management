using System;
using System.Collections.Generic;

namespace Events.Web.shakila;

public partial class Eventsponsorsimage
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string SponsorImage { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
