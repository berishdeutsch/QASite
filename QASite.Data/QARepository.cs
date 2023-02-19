using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(user == null)
            {
                return null;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }
        public void AddQuestion(Question question)
        {
            using var context = new QADataContext(_connectionString);
            question.Date = DateTime.Now;
            
            context.Questions.Add(question);
            context.SaveChanges();
        }
        public User GetByEmail(string email)
        {
            using var context = new QADataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
