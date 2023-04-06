using System;
using System.Collections.Generic;

namespace Events.Web.sakila;

public partial class Eventsponsor
{
    public int Id { get; set; }

    public long EventId { get; set; }

    public string SponsorName { get; set; } = null!;

    public string SponsorOrganization { get; set; } = null!;

    public decimal AmountSponsored { get; set; }

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
