using IAlgorTrader.Back.Service;
using IAlgorTrader.Back.Service.Interfaces.AlgorithmService;
using IAlgorTrader.Back.Service.Interfaces.ContactUsFormService;
using IAlgorTrader.Back.Service.Interfaces.ContactUsService;
using IAlgorTrader.Back.Service.Interfaces.OrderService;
using IAlgorTrader.Back.Service.Interfaces.SymbolTransactionService;
using IAlgorTrader.Back.Service.Interfaces.TradeService;
using IAlgorTrader.Back.Service.Interfaces.UserService;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Repository;
using IAlgoTrader.Back.Service.Implementation.Implementations.AlgorithmServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.ContactUsFormServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.ContactUsServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.OrderServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.SymbolTransactionServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.TradeServices;
using IAlgoTrader.Back.Service.Implementation.Implementations.UserServices;
using IAlgoTrader.Back.Service.SeedWorks.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IAlgoTrader.Back.Service.Implementation
{
    public class ServiceHolder : IServiceHolder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ServiceHolder(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }
        private IAlgorithmService _algorithmService;
        private IOrderService _orderService;
        private IContactUsService _contactUsService;
        private IContactUsFormService _contactUsFormService;
        private ISymbolTransactionService _symbolTransactionService;
        private IUserService _userService;
        private ITradeService _tradeService;


        public IAlgorithmService AlgorithmService => _algorithmService ??= new AlgorithmService(_unitOfWork);
        public IOrderService OrderService => _orderService ??= new OrderService(_unitOfWork);
        public IContactUsService ContactUsService => _contactUsService ?? new ContactUsService(_unitOfWork);
        public IContactUsFormService ContactUsFormService => _contactUsFormService ?? new ContactUsFormService(_unitOfWork);
        public ISymbolTransactionService SymbolTransactionService => _symbolTransactionService ??= new SymbolTransactionService(_unitOfWork);
        public ITradeService TradeService => _tradeService ?? new TradeService(_unitOfWork);
        public IUserService UserService => _userService ??= new UserService(_unitOfWork, _userManager, _roleManager, _tokenGenerator);
    }
}
