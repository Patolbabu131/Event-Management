using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Events.Web.Models;

public partial class User
{
    [DisplayName("ID")]
    public long Id { get; set; }

    [DisplayName("Full Name")]
    public string FullName { get; set; } = null!;

    [DisplayName("Designation")]
    public string Designation { get; set; } = null!;

    [DisplayName("Appointed On")]
    public DateTime? AppointedOn { get; set; }

    [DisplayName("Dutties")]
    public string Duties { get; set; } = null!;

    [DisplayName("Created by")]
    public long? CreatedBy { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime ModifiedOn { get; set; }

    [DisplayName("Active")]
    public bool Active { get; set; }

    public string LoginName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    //public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Event> EventCreatedByNavigations { get; } = new List<Event>();

    public virtual ICollection<Event> EventModifiedByNavigations { get; } = new List<Event>();

    public virtual ICollection<Eventattendee> EventattendeeCreatedByNavigations { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventattendee> EventattendeeModifiedByNavigations { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventattendee> EventattendeeUserNavigations { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentCreatedByNavigations { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentModifiedByNavigations { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcouponassignment> EventcouponassignmentUserNavigations { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; } = new List<Eventcouponassignmentmapping>();

    public virtual ICollection<Eventcoupontype> EventcoupontypeCreatedByNavigations { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventcoupontype> EventcoupontypeModifiedByNavigations { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventexpense> EventexpenseCreatedByNavigations { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventexpense> EventexpenseModifiedByNavigations { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventsponsor> EventsponsorCreatedByNavigations { get; } = new List<Eventsponsor>();

    public virtual ICollection<Eventsponsor> EventsponsorModifiedByNavigations { get; } = new List<Eventsponsor>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; } = new List<User>();

    public virtual ICollection<User> InverseModifiedByNavigation { get; } = new List<User>();

    //public virtual User? ModifiedByNavigation { get; set; }
}
