using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleBot.Entities;

[Table("schedule")]
public class Entry
{
    [Key]
    public int Id { get; set; }

    [Column("day")]
    public DayOfWeek Day { get; set; }

    [Column("time")]
    public TimeOnly Time { get; set; }

    [Column("directionId")]
    public int DirectionId { get; set; }

    [ForeignKey("DirectionId")]
    public Direction? Direction { get; set; }
}
