using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STACK.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Question()
        {
            Date = DateTime.Now;
        }
    }
}