namespace Application.Cards
{
    public class List
    {
        public class Request : IRequest<List<Card>>{}

        public class Handler : IRequestHandler<Request, List<Card>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Card>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _context.Cards.ToListAsync();
            }
        }
    }
}
