using System;
using System.Collections.Generic;

namespace Events.Web.Models;

public partial class Eventexpense
{
    public long Id { get; set; }

    public string ExpenseName { get; set; } = null!;

    public long EventId { get; set; }

    public string ExpenseSubject { get; set; } = null!;

    public decimal AmountSpent { get; set; }

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public string Remarks { get; set; } = null!;

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Executivemember CreatedByNavigation { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Executivemember? ModifiedByNavigation { get; set; }
}
