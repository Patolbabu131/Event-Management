﻿using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Eventcouponassignmentmapping
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public long CouponTypeId { get; set; }

    public decimal CouponNumber { get; set; }

    public long? User { get; set; }

    public long? Attendee { get; set; }

    public string Booked { get; set; } = null!;
    public virtual Eventattendee? AttendeeNavigation { get; set; }

    public virtual Eventcoupontype CouponType { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User? UserNavigation { get; set; }
}

