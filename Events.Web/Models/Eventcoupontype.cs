using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Events.Web.Models;

public partial class Eventcoupontype
{
    [DisplayName("ID")]
    public long Id { get; set; }
    [DisplayName("Event Id")]

    public long EventId { get; set; }

    [DisplayName("Coupon Name")]
    public string CouponName { get; set; } = null!;

    [DisplayName("Coupon Price")]
    public decimal CouponPrice { get; set; }

    [DisplayName("Total Coupon")]
    public double TotalCoupon { get; set; }

    [DisplayName("Active")]
    public bool Active { get; set; }

    [DisplayName("Created By")]
    public long CreatedBy { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Eventattendee> Eventattendees { get; } = new List<Eventattendee>();
    public virtual ICollection<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; } = new List<Eventcouponassignmentmapping>();
    public virtual ICollection<Eventcouponassignment> Eventcouponassignments { get; } = new List<Eventcouponassignment>();
    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
