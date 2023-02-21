using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QASite.Data
{
    public class QARepository
    {
        private string _connectionString;

        public QARepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            using var context = new QADataContext(_connectionString);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new QADataContext(_connectionString);
            User user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }
        public void AddQuestion(Question question, List<string> tags)
        {
            using var context = new QADataContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();
            foreach(string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if(t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }
            context.SaveChanges();

        }
        public User GetByEmail(string email)
        {
            using var context = new QADataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
        public Question GetQuestionById(int id)
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .ThenInclude(a => a.User)
                .FirstOrDefault(q => q.Id == id);
        }
        public void AddAnswer(Answer answer)
        {
            using var context = new QADataContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
        public List<Question> GetQuestions()
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions
                .Include(q => q.Answers)
                .Include(q => q.QuestionsTags)
                .ThenInclude(q => q.Tag)
                .OrderByDescending(q => q.Date).ToList();
        }
        private Tag GetTag(string name)
        {
            using var context = new QADataContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }
        private int AddTag(string name)
        {
            using var context = new QADataContext(_connectionString);
            var tag = new Tag { Name = name };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }
    }
}
