// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace thema12.Models
{
    public partial class Account
    {
        public Account()
        {
            Tasks = new HashSet<Task>();
        }

        public int Account_ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string E_mail { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}