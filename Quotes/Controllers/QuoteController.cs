using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes.Models;

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
                Category="Жизнь"
            
            },

            new QuoteModels()
            {
                Id =1,
                Author = "",
                Quote="Тот, кто с юности верует в собственный ум, " +
                      "Стал, в погоне за истиной, сух и угрюм. " +
                      "Притязающий с детства на знание жизни, " +
                      "Виноградом не став, превратился в изюмю",
                Category="Мудрость"
            }
        };

        [HttpGet]
        [Route("GetCategory")]
        public IActionResult GetCategory(string category)
        {
            return Ok(quotesList.Where(t => t.Category.ToLower().Contains(category.ToLower())));
        }


        [Route("Quote")]
        [HttpPost]
        public IActionResult Quote([FromBody] QuoteModels quoteModels)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Id = quotesList.Select(t => t.Id).Max();
            Id++;
            quoteModels.Id = Id;
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
        public IActionResult Edit([FromRoute]int id,[FromBody] QuoteModels quoteModels)
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
    }
}