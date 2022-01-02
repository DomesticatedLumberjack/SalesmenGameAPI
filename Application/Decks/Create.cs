namespace Application.Decks
{
    public class Create
    {
        public class Request : IRequest<Deck> 
        {
            public int? DeckSize { get; set; }
        }

        public class Handler : IRequestHandler<Request, Deck>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Deck> Handle(Request request, CancellationToken cancellationToken)
            {
                Deck newDeck = new Deck();
                List<Card> newDeckCards = new List<Card>();
                
                //Generate random List of ints for a list of card indexes
                List<int> cardIndexes = RandomGen.GenerateRandomUniqueIntList(
                    (int)(request.DeckSize == null ? Constants.DefaultDeckSize : request.DeckSize),
                    _context.Cards.Count());

                //Add each card to deck
                for(int i = 0; i < cardIndexes.Count; i++)
                {
                    Card randomCard = _context.Cards.Skip(cardIndexes[i]).First();
                    newDeckCards.Add(randomCard);
                }

                //Assign cards to deck
                newDeck.Cards.AddRange(newDeckCards);

                //Add deck to db
                await _context.Decks.AddAsync(newDeck);

                if (await _context.SaveChangesAsync() <= 0)
                    throw new Exception("Unable to save changes to db");

                return newDeck;
            }
        }
    }
}
