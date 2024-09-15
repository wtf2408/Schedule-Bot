using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleBot.Entities;

public class Schedule
{
    [Key]
    public int Id { get; set; }

    [Column("day")]
    public DayOfWeek Day { get; set; }

    [Column("time")]
    public DateTime Time { get; set; }

    [Column("directionId")]
    public int DirectionId { get; set; }

    [ForeignKey("DirectionId")]
    public Direction? Direction { get; set; }
}
