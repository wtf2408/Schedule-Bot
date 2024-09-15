using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScheduleBot;

public partial class Direction
{
    [Key]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; } = null!;
}
