using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Events.Web.Models;

public partial class Eventcouponassignment
{
    [DisplayName("ID")]
    public long Id { get; set; }

    [DisplayName("Event Id")]
    public long EventId { get; set; }

    [DisplayName("Executive Member Id")]
    public long ExecutiveMemberId { get; set; }

    [DisplayName("Coupons Form")]
    public int CouponsFrom { get; set; }

    [DisplayName("Coupons To")]
    public int CouponsTo { get; set; }

    [DisplayName("Total Coupons")]
    public int TotalCoupons { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Created By")]
    public long CreatedBy { get; set; }

    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }
    public long CouponTypeId { get; set; }
    public virtual Eventcoupontype CouponType { get; set; } = null!;
    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Executivemember ExecutiveMember { get; set; } = null!;

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
