using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STACK.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Answer()
        {
            Date = DateTime.Now;
        }
    }
}