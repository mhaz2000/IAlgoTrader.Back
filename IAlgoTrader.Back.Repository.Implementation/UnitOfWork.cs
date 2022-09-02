using IAlgoTrader.Back.Repository.Implementation.Implementations;
using IAlgoTrader.Back.Repository.Repositories;

namespace IAlgoTrader.Back.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private ContactUsRepository _contactUsRepository;
        private ContactUsFormRepository _contactUsFormRepository;
        private OrderRepository _orderRepository;
        private SymbolTransactionRepository _symbolTransactionRepository;
        private UserRepository _userRepository;
        private TradeRepository _tradeRepository;
        private SymbolRepository _symbolRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);

        public ISymbolTransactionRepository SymbolTransactionRepository => _symbolTransactionRepository = _symbolTransactionRepository ?? new SymbolTransactionRepository(_context);

        public IContactUsRepository ContactUsRepository => _contactUsRepository ?? new ContactUsRepository(_context);

        public IContactUsFormRepository ContactUsFormRepository => _contactUsFormRepository ?? new ContactUsFormRepository(_context);

        public ISymbolRepository SymbolRepository => _symbolRepository ?? new SymbolRepository(_context);

        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(_context);

        public ITradeRepository TradeRepository => _tradeRepository ?? new TradeRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
