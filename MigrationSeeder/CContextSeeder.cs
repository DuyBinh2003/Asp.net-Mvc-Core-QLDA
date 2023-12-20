using DoAn.Models;
using System.Linq;

namespace DoAn.MigrationSeeder
{
    public static class CContextSeeder
    {
        
        public static void Seed(CContext context)
        {
            
            // Kiểm tra xem có người dùng nào trong database chưa
            if (!context.Users.Any())
            {
                // Thêm 30 người dùng
                for (int i = 1; i <= 30; i++)
                {
                    var user = new User
                    {
                        UserId = i,
                        Name = $"User{i}",
                        Email = $"user{i}@gmail.com",
                        Username = $"user{i}",
                        Password = $"password{i}",
                        UserType = "customer" 
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
                var authors = new List<Author>
                {
                    new Author { AuthorId = 1, Name = "Nguyễn Nhật Ánh", ImgPath = "https://cand.com.vn/Files/Image/linhchi/2016/09/19/47ca7ab5-f6d4-43ac-a6dd-24c48bd0647c.jpg", Description = "Ông là một trong những tác giả nổi tiếng và được yêu thích trong cộng đồng đọc giả trẻ Việt Nam. Những tác phẩm của ông thường mang đến những câu chuyện nhẹ nhàng, hài hước với nhiều thông điệp tích cực." },
                    new Author { AuthorId = 2, Name = "Tô Hoài", ImgPath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdpSJR6GdylQP3pi5hp0oSUapuP3rE9vxQJw&usqp=CAU", Description = "Ông là một nhà văn Việt Nam. Một số tác phẩm đề tài thiếu nhi của ông được dịch ra ngoại ngữ." },
                    new Author { AuthorId = 3, Name = "Ngô Thanh Hòa", ImgPath = "https://dep.com.vn/Uploaded/travt/2015_01_12/dau-bep-thai-hoa-deponline.jpg", Description = "Ngoài những món ăn nhìn là mê – ăn là nghiền, Thanh Hòa còn khiến người ta khâm phục vì hành trình theo đuổi giấc mơ của mình. Để có thể hiểu nhiều hơn về vị Đầu bếp tài hoa này, hãy cùng Chefjob dừng chân tại nhà hàng Góc Á để nghe anh trải nghiệm nhé." },
                    new Author { AuthorId = 4, Name = "Nguyễn Mạnh Hùng", ImgPath = "https://photo-cms-anninhthudo.epicdn.me/w460/Uploaded/2023/170/2016_06_12/hungazit-1.jpg", Description = "Ông là gã đầu bếp này có ngoại hình nửa nghệ sĩ nửa “giang hồ” với bắp tay xăm trổ rắn chắc của một người thường xuyên tập thể thao, nước da ngăm và tai xỏ khuyên, tóc cắt thời trang và lại đeo kính trắng, cả khi đứng bếp." },
                    new Author { AuthorId = 5, Name = "Harper Lee", ImgPath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTVBiNtlV1OJRu7zwaYANAMwsUNgs276BOivA&usqp=CAU", Description = "Nelle Harper Lee, thường được biết tới với tên Harper Lee, là một nữ nhà văn người Mỹ. Bà được biết tới nhiều nhất qua tiểu thuyết đầu tay Giết con chim nhại." },
                    new Author { AuthorId = 6, Name = "Vũ Trọng Phụng", ImgPath = "https://upload.wikimedia.org/wikipedia/commons/e/e5/Vu_Trong_Phung.jpg", Description = "Vũ Trọng Phụng là một nhà văn, nhà báo nổi tiếng của Việt Nam vào đầu thế kỷ 20." },
                    new Author { AuthorId = 7, Name = "Kahneman", ImgPath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMvQKv_F4LgXXA4RqXYD4tCOHJH-tHLeZxuQ&usqp=CAU", Description = "Daniel Kahneman là một nhà tâm lý học và nhà kinh tế học người Mỹ gốc Israel nổi tiếng với công trình nghiên cứu về tâm lý học đánh giá và đưa ra quyết định, cũng như kinh tế học hành vi." },
                    new Author { AuthorId = 8, Name = "Malcolm Gladwell", ImgPath = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSOdkZPXdrsiemduA51x5giBy4nQS-DM1azA&usqp=CAU", Description = "Malcolm Timothy Gladwell CM là một nhà báo, tác giả, và diễn giả gốc Canada sinh ra tại Anh." },
                    new Author { AuthorId = 9, Name = "Đặng Phong", ImgPath = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Dangphong.jpg/330px-Dangphong.jpg", Description = "Ông là một nhà sử học kinh tế người Việt Nam. Ông lần lượt tốt nghiệp Đại học Hà Nội vào năm 1960 rồi sau đó là Đại học Kinh tế quốc dân Hà Nội vào 4 năm sau." },
                    new Author { AuthorId = 10, Name = "Nguyễn Nhật Ánh", ImgPath = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Warren_Buffett_KU_Visit.jpg/405px-Warren_Buffett_KU_Visit.jpg", Description = "Ông là một trùm doanh nhân, nhà đầu tư và nhà từ thiện người Mỹ. Ông là nhà đầu tư thành công nhất thế giới, cổ đông lớn nhất kiêm giám đốc hãng Berkshire Hathaway và được tạp chí Forbes xếp ở vị trí người giàu thứ bảy thế giới với tài sản ước chừng 100,6 tỷ USD tính đến tháng 4/2021." },
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }
            // Seeding books
            if (!context.Books.Any())
            {
                Random random = new Random();

                foreach (var category in context.Categories)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        var book = new Book
                        {
                            Name = $"Book{i} - {category.Name}",
                            Description = $"Description for Book{i} in {category.Name}",
                            ImgPath = "https://cdn.bigmall.vn/picture/450/450/22609",
                            AuthorId = 1, 
                            CategoryId = category.CategoryId,
                            Price = random.Next(10, 101), 
                            Quantity = 100, 
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
                        var invoice = new Invoice
                        {
                            InvoiceId = (user.UserId - 1) * 2 + i,
                            UserId = user.UserId,
                            Address = $"Address{i} for {user.Name}",
                            Date = DateTime.Now.AddDays(-i), // Ngày tạo hóa đơn
                            Note = $"Note for Invoice{i} by {user.Name}",
                            Sdt = $"012345678{i}",
                        };

                        context.Invoices.Add(invoice);
                    }
                }
                context.SaveChanges();
            }
            // Seeding invoiceDetails
            if (!context.InvoiceDetails.Any())
            {
                Random random = new Random();

                foreach (var invoice in context.Invoices)
                {
                    var selectedBooks = new List<int>();
                    for (int j = 1; j <= 3; j++) // Mỗi hoá đơn có 3 chi tiết hoá đơn
                    {
                        int id;
                        do
                        {
                            id = random.Next(1, 21);
                        } while (selectedBooks.Contains(id));

                        selectedBooks.Add(id);

                        var invoiceDetail = new InvoiceDetail
                        {
                            InvoiceId = invoice.InvoiceId,
                            BookId = id,
                            Quantity = random.Next(1, 4),
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
                Random random = new Random();

                foreach (var user in context.Users)
                {
                    var selectedBooks = new List<int>();
                    for (int i = 1; i <= 3; i++) // Mỗi người dùng có 3 cart
                    {
                        int id;
                        do
                        {
                            id = random.Next(1, 21);
                        } while (selectedBooks.Contains(id));

                        selectedBooks.Add(id);

                        var cart = new Cart
                        {
                            UserId = user.UserId,
                            BookId = id,
                            Quantity = random.Next(1, 4) // Số lượng từ 1 đến 3
                        };
                        context.Carts.Add(cart);
                    }
                }
                context.SaveChanges();
            }
            
            if (!context.Reviews.Any())
            {
                Random random = new Random();

                foreach (var user in context.Users)
                {
                    var selectedBooks = new List<int>();
                    for (int i = 1; i <= 3; i++) // Mỗi người dùng có 3 cart
                    {
                        int id;
                        do
                        {
                            id = random.Next(1, 21);
                        } while (selectedBooks.Contains(id));

                        selectedBooks.Add(id);

                        var Review = new Review
                        {
                            UserId = user.UserId,
                            BookId = id,
                            Content = $"{user.Name} comment...",
                            Rate = random.Next(1, 6)
                        };
                        context.Reviews.Add(Review);
                    }
                }
                context.SaveChanges();
            }
        }
    }
    
}
