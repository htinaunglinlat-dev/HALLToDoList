using System;
using System.Collections.Generic;

namespace HALLToDoList.Database.Models;

public partial class TblList
{
    public int TaskId { get; set; }

    public string TaskTitle { get; set; } = null!;

    public string Status { get; set; } = null!;
}
