using System;
using System.Collections.Generic;

namespace Events.Web.shakila;

public partial class Eventattendee
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string AttendeeName { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string CouponsPurchased { get; set; } = null!;

    public DateTime PurchasedOn { get; set; }

    public decimal TotalAmount { get; set; }

    public string Remarks { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public long? User { get; set; }

    public long CouponTypeId { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool PaymentStatus { get; set; }

    public string? ModeOfPayment { get; set; }

    public string PaymentReference { get; set; } = null!;

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; } = new List<Eventcouponassignmentmapping>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual User? UserNavigation { get; set; }
}
