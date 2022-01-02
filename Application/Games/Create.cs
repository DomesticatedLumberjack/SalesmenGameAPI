namespace Application.Games
{
    public class Create
    {
        public class Request : IRequest<Game>{}
        
        public class Handler : IRequestHandler<Request, Game>
        {
            private readonly IMediator _mediator;
            private readonly DataContext _dataContext;
            public Handler(IMediator mediator, DataContext dataContext) 
            {
                _mediator = mediator;
                _dataContext = dataContext;
            }

            public async Task<Game> Handle(Request request, CancellationToken cancellationToken)
            {
                //Create new game and deck
                var newDeck = await _mediator.Send(new Application.Decks.Create.Request());
                Game newGame = new Game();

                //Add deck to game
                if(newDeck != null)
                    newGame.Deck = (Deck)newDeck;

                //Add new game and deck to db
                await _dataContext.Games.AddAsync(newGame);

                if (await _dataContext.SaveChangesAsync() <= 0)
                    throw new Exception("Unable to create game and save to db");

                //Return Game obj
                return newGame;
                
            }
        }
    }
}
