using DoAn.Models;
using System.Linq;

namespace DoAn.MigrationSeeder
{
    public static class CContextSeeder
    {
        
        public static void Seed(CContext context)
        {
            Random random = new Random();
            DateTime startDate = new DateTime(2023, 1, 1); // Ngày bắt đầu năm 2023
            DateTime endDate = new DateTime(2023, 12, 26); // Ngày kết thúc năm 2023
            

            // Kiểm tra xem có người dùng nào trong database chưa
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Admin",
                    Email = "Admin@gmail.com",
                    Username = "Admin",
                    Password = "123456",
                    UserType = "admin",
                    Sdt = "0123456789",
                    Address = "Address for Admin"
                };
                context.Users.Add(admin);
                // Thêm 30 người dùng
                for (int i = 1; i <= 100; i++)
                {
                    var user = new User
                    {
                        Name = $"User{i}",
                        Email = $"user{i}@gmail.com",
                        Username = $"user{i}",
                        Password = "123456",
                        UserType = "customer",
                        Sdt = "0123456789",
                        Address = $"Address for User{i}"
                    };
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
            // Seeder categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { CategoryId = 1, Name = "Children", Description = "Books for children" },
                    new Category { CategoryId = 2, Name = "Cook", Description = "Cooking and culinary books" },
                    new Category { CategoryId = 3, Name = "Novel", Description = "Fiction and novels" },
                    new Category { CategoryId = 4, Name = "Mentality", Description = "Books about mentality and self-help" },
                    new Category { CategoryId = 5, Name = "Economy", Description = "Economics helps us live better" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            // Seeder author
            if (!context.Authors.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var author = new Author
                    {
                        Name = $"Author{i}",
                        ImgPath = "https://cand.com.vn/Files/Image/linhchi/2016/09/19/47ca7ab5-f6d4-43ac-a6dd-24c48bd0647c.jpg",
                        Description = $"Description for author{i}"
                    };
                    context.Authors.Add(author);
                }
                context.SaveChanges();
            }
            // Seeding books
            if (!context.Books.Any())
            {
                foreach (var category in context.Categories)
                {
                    int number = random.Next(5, 11); 
                    for (int i = 1; i <= number; i++)
                    {
                        var book = new Book
                        {
                            Name = $"Book{i} - {category.Name}",
                            Description = $"Description for Book{i} in {category.Name}",
                            ImgPath = "https://cdn.bigmall.vn/picture/450/450/22609",
                            AuthorId = random.Next(1, 11), 
                            CategoryId = category.CategoryId,
                            Price = random.Next(10, 101), 
                            Quantity = 1000, 
                            Rate = 0
                        };
                        context.Books.Add(book);
                    }
                }
                context.SaveChanges();
            }

            // Seeding Invoices
            if (!context.Invoices.Any())
            {
                foreach (var user in context.Users)
                {
                    for (int i = 1; i <= 2; i++) // Mỗi người dùng có 2 hóa đơn
                    {
                        int range = (endDate - startDate).Days;
                        DateTime randomDate = startDate.AddDays(random.Next(range));
                        var invoice = new Invoice
                        {
                            UserId = user.UserId,
                            Address = user.Address,
                            Sdt = user.Sdt,
                            Date = randomDate,
                            Note = $"Note for Invoice{i} by {user.Name}"
                        };
                        context.Invoices.Add(invoice);
                    }
                }
                context.SaveChanges();
            }
            // Seeding invoiceDetails
            if (!context.InvoiceDetails.Any())
            {
                int numberBooks = context.Books.Count();
                foreach (var invoice in context.Invoices)
                {
                    var selectedBooks = new List<int>();
                    for (int j = 1; j <= 3; j++)
                    {
                        int id;
                        do
                        {
                            id = random.Next(1, numberBooks + 1);
                        } while (selectedBooks.Contains(id));
                        selectedBooks.Add(id);
                        var invoiceDetail = new InvoiceDetail
                        {
                            InvoiceId = invoice.InvoiceId,
                            BookId = id,
                            Quantity = random.Next(1, 6),
                            UnitPrice = 0,
                        };
                        context.InvoiceDetails.Add(invoiceDetail);
                    }
                }
                context.SaveChanges();
            }
            // Seedign cart
            if (!context.Carts.Any())
            {
                int numberBooks = context.Books.Count();
                foreach (var user in context.Users)
                {
                    var selectedBooks = new List<int>();
                    for (int i = 1; i <= 3; i++) // Mỗi người dùng có 3 cart
                    {
                        int id;
                        do
                        {
                            id = random.Next(1, numberBooks + 1);
                        } while (selectedBooks.Contains(id));

                        selectedBooks.Add(id);

                        var cart = new Cart
                        {
                            UserId = user.UserId,
                            BookId = id,
                            Quantity = random.Next(1, 6) // Số lượng từ 1 đến 5
                        };
                        context.Carts.Add(cart);
                    }
                }
                context.SaveChanges();
            }
            if (!context.Reviews.Any())
            {
                int numberBooks = context.Books.Count();
                foreach (var user in context.Users)
                {
                    var Review = new Review
                    {
                        UserId = user.UserId,
                        BookId = random.Next(1, numberBooks + 1),
                        Content = $"{user.Name} comment for book...",
                        Rate = random.Next(1, 6)
                    };
                    context.Reviews.Add(Review);
                }
                context.SaveChanges();
            }
        }
    }
    
}
