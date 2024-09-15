using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ScheduleBot.Entities;

namespace ScheduleBot;

public partial class ScheduleBotContext : DbContext
{
    public ScheduleBotContext(DbContextOptions options) : base (options)
    {
        
    }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
}
