// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace thema12.Models
{
    public partial class DeadlineStatus
    {
        public DeadlineStatus()
        {
            Deadlines = new HashSet<Deadline>();
        }

        public int DeadlineStatusID { get; set; }
        public string DeadlineStatusName { get; set; }

        public virtual ICollection<Deadline> Deadlines { get; set; }
    }
}