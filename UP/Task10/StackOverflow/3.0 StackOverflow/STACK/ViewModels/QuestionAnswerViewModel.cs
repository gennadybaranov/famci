using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STACK.Models;

namespace STACK.ViewModels
{
    public class QuestionAnswerViewModel
    {
        public Question Question { get; set; }
        public Answer NewAnswer { get; set; }
    }
}