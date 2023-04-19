using System;
using System.Collections.Generic;

namespace Events.Web.shakila;

public partial class Eventcouponassignment
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public long User { get; set; }

    public int CouponsFrom { get; set; }

    public int CouponsTo { get; set; }

    public long TotalCoupons { get; set; }

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public long CouponTypeId { get; set; }

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
