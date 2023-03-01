using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Eventattendee
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public string AttendeeName { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public int CouponsPurchased { get; set; }

    public DateTime PurchasedOn { get; set; }

    public decimal TotalAmount { get; set; }

    public string Remarks { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public long? InvitedBy { get; set; }

    public long CouponTypeId { get; set; }

    public int RemainingCoupons { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? ModeOfPayment { get; set; }

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Eventattendee> InverseInvitedByNavigation { get; } = new List<Eventattendee>();

    public virtual Eventattendee? InvitedByNavigation { get; set; }

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
