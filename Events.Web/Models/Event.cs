using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Events.Web.Models;

public partial class Event
{
    
    [DisplayName("ID")]
    public long Id { get; set; }  
    //[Required(ErrorMessage = "ID is required...")]
    

    [DisplayName("Event Name")]
    public string EventName { get; set; } = null!;
    //[Required(ErrorMessage = "Event Name is required...")]

    [DisplayName("Event Date")]
    public DateTime EventDate { get; set; }
    
    

    [DisplayName("Event Venue")]
    public string EventVenue { get; set; } = null!;
    //[Required(ErrorMessage = "Event Venue is required...")]
    
    [DisplayName("Event Start Time")]
    public DateTime EventStartTime { get; set; }
    //[Required(ErrorMessage = "Event Start Time is required...")]
    
    [DisplayName("Event End Time")]
    public DateTime EventEndTime { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy}")]
    //[Required(ErrorMessage = "Eventg End Time is required...")]
    
    [DisplayName("Event Status")]
    public string EventStatus { get; set; } = null!;

    [DisplayName("Event Year")]
    public DateTime EventYear { get; set; }
    //[Required(ErrorMessage = "Event Year is required...")]    
    
    [DisplayName("Created By")]
    public long? CreatedBy { get; set; }
    //[Required(ErrorMessage = "Created By is required...")]
    
    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }
    //[Required(ErrorMessage = "Created On is required...")]
   
    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }
    //[Required(ErrorMessage = "Modified By is required...")]
    
    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }
    //[Required(ErrorMessage = "Modified On is required...")]
    
    [DisplayName("Food Menu")]
    public string FoodMenu { get; set; } = null!;
    //[Required(ErrorMessage = "Food Menu is required...")]

    public virtual Executivemember? CreatedByNavigation { get; set; }
    
    public virtual ICollection<Eventattendee> Eventattendees { get; } = new List<Eventattendee>();

    public virtual ICollection<Eventcouponassignment> Eventcouponassignments { get; } = new List<Eventcouponassignment>();

    public virtual ICollection<Eventcoupontype> Eventcoupontypes { get; } = new List<Eventcoupontype>();

    public virtual ICollection<Eventexpense> Eventexpenses { get; } = new List<Eventexpense>();

    public virtual ICollection<Eventsponsor> Eventsponsors { get; } = new List<Eventsponsor>();

    public virtual ICollection<Eventsponsorsimage> Eventsponsorsimages { get; } = new List<Eventsponsorsimage>();

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
