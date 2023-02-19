using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QASite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QASite.Web.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly string _connectionString;

        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult AskAQuestion()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(Question question)
        {
            var repo = new QARepository(_connectionString);
            question.User = repo.GetByEmail(User.Identity.Name);
            repo.AddQuestion(question);
            return Redirect("/Questions/ViewQuestion");
        }

        public IActionResult ViewQuestion()
        {
            return View();
        }
    }
}
