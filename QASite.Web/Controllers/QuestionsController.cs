using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QASite.Data;
using QASite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QASite.Web.Controllers
{
  
    public class QuestionsController : Controller
    {
        private readonly string _connectionString;

        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(Question question, List<string> tags)
        {
            var repo = new QARepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            question.UserId = user.Id;
            question.Date = DateTime.Now;
            repo.AddQuestion(question, tags);
            return Redirect($"/Questions/ViewQuestion/{question.Id}");
        }

        public IActionResult ViewQuestion(int id)
        {
            var repo = new QARepository(_connectionString);
            var question = repo.GetQuestionById(id);
            var vm = new ViewQuestionViewModel
            {
                Question = question,
            };
            if (User.Identity.IsAuthenticated)
            {
                vm.User = repo.GetByEmail(User.Identity.Name);
            }
            if (question == null)
            {
                return Redirect("/");
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult AddAnswer(Answer answer)
        {
            var repo = new QARepository(_connectionString);
            var user = repo.GetByEmail(User.Identity.Name);
            answer.UserId = user.Id;
            answer.Date = DateTime.Now;
            repo.AddAnswer(answer);
            return Redirect($"/Questions/ViewQuestion/{answer.QuestionId}");
        }
    }
}
