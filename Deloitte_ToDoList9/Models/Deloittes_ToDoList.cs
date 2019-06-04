using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Deloitte_ToDoList9.Models
{
    public class Deloittes_ToDoList
    {
        [Key]
        public int Task_Id { get; set; }

        public DateTime Task_Date { get; set;}
        
        public string Task { get; set; }

        public string Task_Description { get; set; }

        public bool Task_IsChecked { get; set; }

        public DateTime Task_LastUpdated_Date { get; set; }

        public virtual ApplicationUser ToDoList_User { get; set; }
    }
}