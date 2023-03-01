using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Executivemember
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public DateTime? AppointedOn { get; set; }

    public string Duties { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public bool Active { get; set; }

    public virtual Executivemember? CreatedByNavigation { get; set; }

    public virtual ICollection<Event> EventCreatedByNavigations { get; } = new List<Event>();

    public virtual ICollection<Event> EventModifiedByNavigations { get; } = new List<Event>();

    public virtual ICollection<Eventattendee> EventattendeeCreatedByNavigations { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventattendee> EventattendeeModifiedByNavigations { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentCreatedByNavigations { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentExecutiveMembers { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentModifiedByNavigations { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcoupontype> EventcoupontypeCreatedByNavigations { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventcoupontype> EventcoupontypeModifiedByNavigations { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventexpense> EventexpenseCreatedByNavigations { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventexpense> EventexpenseModifiedByNavigations { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventsponsor> EventsponsorCreatedByNavigations { get; } = new List<Eventsponsor>();

    public virtual ICollection<Eventsponsor> EventsponsorModifiedByNavigations { get; } = new List<Eventsponsor>();

    public virtual ICollection<Executivemember> InverseCreatedByNavigation { get; } = new List<Executivemember>();

    public virtual ICollection<Executivemember> InverseModifiedByNavigation { get; } = new List<Executivemember>();

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
