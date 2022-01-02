using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Cards;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Card>> GetCardsList(int? cardsNum)
        {
            return await _mediator.Send(new List.Request());
        }

        [HttpPost]
        public async Task<Unit> AddCard(List<string> cardNames)
        {
            return await _mediator.Send(new Create.Request { names = cardNames });
        }
    }
}
