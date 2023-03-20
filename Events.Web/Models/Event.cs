using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Events.Web.Models;

public partial class Event
{
    
    [DisplayName("ID")]
    public long Id { get; set; }  
    [Required]
    

    [DisplayName("Event Name")]
    public string EventName { get; set; } = null!;
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required]

    [DisplayName("Event Date")]
    public DateTime EventDate { get; set; }
    [Required]

    [DisplayName("Event Venue")]
    public string EventVenue { get; set; } = null!;
    [Required]
    
    [DisplayName("Event Start Time")]
    public DateTime EventStartTime { get; set; }
    [Required]
    
    [DisplayName("Event End Time")]
    public DateTime EventEndTime { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy}")]
    [Required]

    [DisplayName("Event Year")]
    public DateTime EventYear { get; set; }
    [Required]
    
    [DisplayName("Created By")]
    public long? CreatedBy { get; set; }
    [Required]
    
    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }
    [Required]
   
    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }
    [Required]
    
    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }
    [Required]
    
    [DisplayName("Food Menu")]
    public string FoodMenu { get; set; } = null!;
    [Required]

    public virtual Executivemember? CreatedByNavigation { get; set; }
    
    public virtual ICollection<Eventattendee> Eventattendees { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignment> Eventcouponassignments { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcoupontype> Eventcoupontypes { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventexpense> Eventexpenses { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventsponsor> Eventsponsors { get; } = new List<Eventsponsor>();

    public virtual ICollection<Eventsponsorsimage> Eventsponsorsimages { get; } = new List<Eventsponsorsimage>();

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
