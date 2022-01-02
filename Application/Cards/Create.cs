namespace Application.Cards
{
    public class Create
    {
        public class Request : IRequest
        {
            public List<string> names { get; set; } = new List<string>();
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            { 
                List<Card> newCards = new List<Card>();

                for(int i = 0; i < request.names.Count; i++)
                {
                    string formattedName = char.ToUpper(request.names[i][0]) + request.names[i].Substring(1);
                    if(!_context.Cards.Where(x => x.Name == formattedName).Any())
                        newCards.Add(new Card { Name = formattedName});
                }

                await _context.Cards.AddRangeAsync(newCards);

                bool success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving card to database");
            }
        }
    }
}
