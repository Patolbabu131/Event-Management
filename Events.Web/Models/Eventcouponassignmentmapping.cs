using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Eventcouponassignmentmapping
{
    public long Id { get; set; }
    public long EventID { get; set; }

    public long CouponTypeId { get; set; }

    public decimal CouponNumber { get; set; }

    public long? ExecutiveMember { get; set; }

    public string? Attendee { get; set; }

    public string Booked { get; set; } = null!;

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual Executivemember? ExecutiveMemberNavigation { get; set; }
}
