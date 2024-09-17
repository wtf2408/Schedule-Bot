using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleBot.Entities;

[Table("schedule")]
public class Entry
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("day")] public DayOfWeek Day { get; set; }

    [Column("time")] public TimeOnly StartTime { get; set; }
    [Column("type")] public string LessonType { get; set; }

    [Column("subjectId")] public int SubjectId { get; set; }
    [ForeignKey("SubjectId")] public Subject Subject { get; set; } = null!;
}
