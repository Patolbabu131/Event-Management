﻿using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Event
{
    public long Id { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string EventVenue { get; set; } = null!;

    public DateTime EventStartTime { get; set; }

    public DateTime EventEndTime { get; set; }

    public int EventYear { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string FoodMenu { get; set; } = null!;

    public virtual Executivemember? CreatedByNavigation { get; set; }

    public virtual ICollection<Eventattendee> Eventattendees { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignment> Eventcouponassignments { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcoupontype> Eventcoupontypes { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventexpense> Eventexpenses { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventsponsor> Eventsponsors { get; } = new List<Eventsponsor>();

    public virtual ICollection<Eventsponsorsimage> Eventsponsorsimages { get; } = new List<Eventsponsorsimage>();

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
