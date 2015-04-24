using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using LMP.Core.Security.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMP.QuestionSystem.Domain.Questions
{
    [Table("Questions")]
    public class Question : CreationAuditedEntity<int, User>
    {
        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual int VoteCount { get; set; }

        public virtual int AnswerCount { get; set; }

        public virtual int ViewCount { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {

        }

        public Question(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}
