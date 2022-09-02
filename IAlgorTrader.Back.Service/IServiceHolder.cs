using IAlgorTrader.Back.Service.Interfaces.AlgorithmService;
using IAlgorTrader.Back.Service.Interfaces.ContactUsFormService;
using IAlgorTrader.Back.Service.Interfaces.ContactUsService;
using IAlgorTrader.Back.Service.Interfaces.OrderService;
using IAlgorTrader.Back.Service.Interfaces.SymbolTransactionService;
using IAlgorTrader.Back.Service.Interfaces.TradeService;
using IAlgorTrader.Back.Service.Interfaces.UserService;


namespace IAlgorTrader.Back.Service
{
    public interface IServiceHolder
    {
        public IAlgorithmService AlgorithmService { get; }
        public IOrderService OrderService { get; }
        public IContactUsService ContactUsService { get; }
        public IContactUsFormService ContactUsFormService { get; }
        public ISymbolTransactionService SymbolTransactionService { get; }
        public ITradeService TradeService { get; }
        public IUserService UserService { get; }
    }
}
