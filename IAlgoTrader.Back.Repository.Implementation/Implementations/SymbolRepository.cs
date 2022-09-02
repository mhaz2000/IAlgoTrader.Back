using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class SymbolRepository : Repository<Symbol>, ISymbolRepository
    {
        public SymbolRepository(DataContext context) : base(context)
        {

        }
    }
}
