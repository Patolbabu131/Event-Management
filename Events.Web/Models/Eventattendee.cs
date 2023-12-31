﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Events.Web.Models;

public partial class Eventattendee
{
    [DisplayName("ID")]
    public long Id { get; set; }

    [DisplayName("Event Id")]
    public long EventId { get; set; }
    [Required(ErrorMessage = "lakh")]

    [DisplayName("Attendee Name")]
    public string AttendeeName { get; set; } = null!;

    [DisplayName("Contact No")]
    public string ContactNo { get; set; } = null!;

    [DisplayName("Coupons Purchased")]
    public string CouponsPurchased { get; set; }

    [DisplayName("Purchased On")]
    public DateTime PurchasedOn { get; set; }

    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; }

    [DisplayName("Remarks")]
    public string Remarks { get; set; } = null!;

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Created By")]
    public long CreatedBy { get; set; }

    [DisplayName("(Executive member)Invited By ")]
    public long? User { get; set; }

    [DisplayName("Coupon Type Id")]
    public long? CouponTypeId { get; set; }



    [DisplayName("Moldified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }

    [DisplayName("Payment Status")]
    public PaymentStatus PaymentStatus { get; set; }

    [DisplayName("Mode Of Payment")]
    public ModeOfPayment ModeOfPayment { get; set; }

    [DisplayName("PaymentReference")]
    public string PaymentReference { get; set; } = null!;

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User? UserNavigation { get; set; }

    public virtual ICollection<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; } = new List<Eventcouponassignmentmapping>();
    public virtual User? ModifiedByNavigation { get; set; }
}

public enum ModeOfPayment
{
    Cash = 1,
    UPI = 2,
    Bank_Transfer = 3,
    Others = 4,
}
public enum PaymentStatus
{
    Paid = 1,
    Pending = 0
}