namespace Application.Decks
{
    public class List
    {
        public class Request : IRequest<List<Deck>> {}

        public class Handler : IRequestHandler<Request, List<Deck>> 
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Deck>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _context.Decks.ToListAsync();
            }
        }
    }
}
