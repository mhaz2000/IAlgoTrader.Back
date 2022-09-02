using IAlgoTrader.Back.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IAlgoTrader.Back.Repository.Implementation;
using System.Text;
using ExcelDataReader;

namespace IAlgoTrader.Back
{
    public static class WebHostExtension
    {
        private static readonly MyDbContextFactory MyDbContextFactory = new MyDbContextFactory();
        private static IUserStore<User> _userStore;
        private static UserManager<User> _userManager;
        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dataContext = MyDbContextFactory.CreateDbContext(new[] { string.Empty });
                _userStore = new UserStore<User>(dataContext);
                _userManager = new UserManager<User>(_userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);
                dataContext.Database.Migrate();
                SeedSymbols(dataContext);
                CreateRolesSeed(dataContext);
                CreateAdminSeed(dataContext, _userManager);
            }

            return host;
        }

        private static void SeedSymbols(DataContext dataContext)
        {
            if (!dataContext.Symbols.Any())
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding(1252);

                DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Mohammad\Desktop\بورس");
                var files = directory.GetFiles();

                List<Symbol> symbols = new List<Symbol>();
                List<Transaction> transactions = new List<Transaction>();
                foreach (var file in files)
                {
                    var symbolName = file.Name.Split('.')[0];
                    Symbol symbol = new Symbol()
                    {
                        Name = symbolName
                    };
                    symbols.Add(symbol);

                    FileStream stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var reader = ExcelReaderFactory.CreateReader(stream);
                    if (reader.Read()) { }
                    int counter = -1;
                    while (reader.Read())
                    {
                        var yesterday = DateTime.Today.AddDays(counter);
                        if (reader[3].ToString() == "0")
                            continue;

                        transactions.Add(new Transaction()
                        {
                            ClosePrice = double.Parse(reader[6].ToString()),
                            LastPrice = double.Parse(reader[7].ToString()),
                            NumberTrade = int.Parse(reader[3].ToString()),
                            PriceFirst = double.Parse(reader[10].ToString()),
                            PriceMax = double.Parse(reader[9].ToString()),
                            PriceMin = double.Parse(reader[8].ToString()),
                            Symbol = symbol,
                            SymbolId = symbol.Id,
                            Date = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 12, 30, 0),
                        });
                        counter--;
                    }

                }
                dataContext.Symbols.AddRange(symbols);
                dataContext.Transactions.AddRange(transactions);
                dataContext.SaveChanges();
            }
        }

        private static void CreateAdminSeed(DataContext context, UserManager<User> userManager)
        {
            string username = "admin";

            if (!context.Users.Any(u => u.UserName == username))
            {
                var newUser = new User()
                {
                    IsAdmin = true,
                    FirstName = "محمد",
                    LastName = "حبیب اله زاده",
                    UserName = "admin",
                    Email = "IAlgoTrader@gmail.com",
                    PhoneNumber = "09207831300",
                    NormalizedUserName = "admin"
                };

                var done = userManager.CreateAsync(newUser, "123456");

                if (done.Result.Succeeded)
                    userManager.AddToRoleAsync(newUser, "Admin");
            }
        }

        private static void CreateRolesSeed(DataContext context)
        {
            var role = context.Roles.AnyAsync(c => c.Name == "Admin");

            if (!role.Result)
            {
                var newAdminRole = new IdentityRole()
                {
                    Name = "Admin",
                    Id = Guid.NewGuid().ToString(),
                    NormalizedName = "admin"
                };

                context.Roles.Add(newAdminRole);

                var newUserRole = new IdentityRole()
                {
                    Name = "User",
                    Id = Guid.NewGuid().ToString(),
                };
                context.Roles.Add(newUserRole);

                context.SaveChanges();
            }
        }

        private static void CreateContactUsSeed(DataContext context)
        {
            var contactUs = new ContactUs()
            {
                Addresses = "تهران - خیابان شریعتی - ضلع جنوب شرقی پل سید خندان\r\nکدپستی: ۱۶۳۱۷۱۴۱۹۱",
                Emails = "78mhaz78@gmail.com",
                PhoneNumbers = "09207831300 - 09109828926"
            };

            context.ContactUs.Add(contactUs);
            context.SaveChanges();
        }

    }
}
