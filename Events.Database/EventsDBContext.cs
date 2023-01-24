using System;
using Microsoft.EntityFrameworkCore;

namespace Events.Database
{
    public partial class eventdbContext : DbContext
    {
        public eventdbContext()
        {
        }

        public eventdbContext(DbContextOptions<eventdbContext> options)
            : base(options)
        {
        }
    }

}

