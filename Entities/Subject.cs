using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScheduleBot.Entities;

[Table("subjects")]
public class Subject
{
    [Key] [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;
    public List<Entry> Entries { get; set; }
}
