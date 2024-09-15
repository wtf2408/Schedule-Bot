using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScheduleBot;

public partial class ScheduleBotContext : DbContext
{
    public ScheduleBotContext(DbContextOptions options) : base (options)
    {
        
    }
    public DbSet<Direction> Directions { get; set; }
}
