using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Decks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeckController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Deck>> GetDeckList()
        {
            return await _mediator.Send(new List.Request());
        }

        [HttpPost]
        public async Task<Deck> CreateDeck(int? deckSize)
        {
            return await _mediator.Send(new Create.Request() { DeckSize = deckSize});
        }
    }
}
