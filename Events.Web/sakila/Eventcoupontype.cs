using System;
using System.Collections.Generic;

namespace Events.Web.sakila;

public partial class Eventcoupontype
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string CouponName { get; set; } = null!;

    public decimal CouponPrice { get; set; }

    public double TotalCoupon { get; set; }

    public bool Active { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Eventattendee> Eventattendees { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; } = new List<Eventcouponassignmentmapping>();

    public virtual ICollection<Eventcouponassignment> Eventcouponassignments { get; } = new List<Eventcouponassignment>();

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
