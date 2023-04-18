using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Events.Web.Models;

public partial class Eventexpense
{
    [DisplayName("ID")]
    public long Id { get; set; }

    [DisplayName("Event Id")]
    public long EventId { get; set; }

    [DisplayName("Expense Name")]
    public string ExpenseName { get; set; } = null!;

    [DisplayName("Expense Subject")]
    public string ExpenseSubject { get; set; } = null!;

    [DisplayName("Amount Spent")]
    public decimal AmountSpent { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }

    [DisplayName("Created By")]
    public long CreatedBy { get; set; }

    [DisplayName("Remarks")]
    public string Remarks { get; set; } = null!;

    [DisplayName("Modified By")]
    public long? ModifiedBy { get; set; }

    [DisplayName("Modified On")]
    public DateTime? ModifiedOn { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }
}
