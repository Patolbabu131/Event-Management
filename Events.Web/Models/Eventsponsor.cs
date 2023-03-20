using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Events.Web.Models;

public partial class Eventsponsor
{
    [DisplayName("ID")]
    public int Id { get; set; }

    [DisplayName("Event Id")]
    public long EventId { get; set; }

    [DisplayName("Sponser Name")]
    public string SponsorName { get; set; } = null!;

    [DisplayName("Sponser Organization")]
    public string SponsorOrganization { get; set; } = null!;

    [DisplayName("Amount Sponsered")]
    public decimal AmountSponsored { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Created By")]
    public long CreatedBy { get; set; }

    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
