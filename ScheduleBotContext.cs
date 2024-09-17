using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ScheduleBot.Entities;

namespace ScheduleBot;

public partial class ScheduleBotContext : DbContext
{
    public ScheduleBotContext(DbContextOptions options) : base (options) {}
    public DbSet<Subject> Lessons { get; set; }
    public DbSet<Entry> Schedule { get; set; }
}
