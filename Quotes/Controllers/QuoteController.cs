using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes.Models;
using SocialApp.Services;

namespace Quotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        public static List<QuoteModels> quotesList = new List<QuoteModels>
        {
            new QuoteModels()
            {
                Id = 1,
                Author = "Умар Хайям",
                Quote = "Меняем реки, страны, города... Иные двери... Новые года... А никуда нам от себя не деться, А если деться - только в никуда.",
                Category="Жизнь",
                 Date = DateTime.Now.AddHours(-23)

            },

            new QuoteModels()
            {
                Id =2,
                Author = "",
                Quote="Тот, кто с юности верует в собственный ум, " +
                      "Стал, в погоне за истиной, сух и угрюм. " +
                      "Притязающий с детства на знание жизни, " +
                      "Виноградом не став, превратился в изюмю",
                Category="Мудрость",
                 Date = DateTime.Now.AddHours(-24)
            }
        };

        public static List<Subscribers> subscribersList = new List<Subscribers>();

        [HttpGet]
        [Route("GetCategory/{category}")]
        public IActionResult GetCategory([FromRoute]string category)
        {
            if (string.IsNullOrEmpty(category)) return NotFound();
            return Ok(quotesList.Where(t => t.Category.Contains(category, StringComparison.InvariantCultureIgnoreCase)));
        }

        [HttpGet]
        [Route("RandomQuotes")]
        public IActionResult RandomQuotes()
        {
            var quote = quotesList[new Random().Next(quotesList.Count)];
            return Ok(quote);
        }

        [HttpGet]
        [Route("GetAllQuotes")]
        public IActionResult GetAllQuotes()
        {
            return Ok(quotesList);
        }


        [Route("Quote")]
        [HttpPost]
        public IActionResult Quote([FromBody] QuoteModels quoteModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Id = quotesList.Select(t => t.Id).Max();
            Id++;
            quoteModels.Id = Id;
            quoteModels.Date = DateTime.Now;
            quotesList.Add(quoteModels);
            return Ok(new { massage = "Добавлено" });
        }

        [Route("DeleteById")]
        [HttpDelete]
        public IActionResult DeleteByID(int id)
        {
            quotesList.Remove(quotesList.Find(t => t.Id == id));
            return Ok(new { massage = "Удалено" });
        }

        [Route("Edit")]
        [HttpPut]
        public IActionResult Edit([FromRoute]int id, [FromBody] QuoteModels quoteModels)
        {
            quotesList.ForEach(t =>
            {
                if (t.Id == id)
                {
                    t.Quote = quoteModels.Quote;
                    t.Author = quoteModels.Author;
                    t.Category = quoteModels.Category;
                }
            });
            return Ok(new { massage = "Изменено" });
        }
        [Route("Subscribe")]
        [HttpPost]
        public IActionResult Edit([FromBody] Subscribers subscribers)
        {
            subscribers.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            subscribersList.Add(subscribers);
            return Ok(new { massage = "Успешно подписанны" });
        }
    }
}