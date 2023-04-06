using System;
using System.Collections.Generic;

namespace Events.Web.sakila;

public partial class Eventattendee
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string AttendeeName { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public long CouponsPurchased { get; set; }

    public DateTime PurchasedOn { get; set; }

    public decimal TotalAmount { get; set; }

    public string Remarks { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public long? ExecutiveMember { get; set; }

    public long CouponTypeId { get; set; }

    public int RemainingCoupons { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool PaymentStatus { get; set; }

    public string? ModeOfPayment { get; set; }

    public string PaymentReference { get; set; } = null!;

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Executivemember? ExecutiveMemberNavigation { get; set; }

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
