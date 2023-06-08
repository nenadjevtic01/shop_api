using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.Implementation.UseCases.Commands
{
    public class FillDatabaseCommandEf : UseCaseEf, IFillDatabaseCommand
    {
        public int UseCaseId => 18;

        public string UseCaseName => "Fill database";

        public string UseCaseDescription => "Initial fill database";

        public FillDatabaseCommandEf(ProjekatAspDbContext context):base(context)
        {

        }

        public void Execute(int request)
        {
            if(Context.AuditLogs.Any(x=>x.UseCaseName==UseCaseName))
            {
                throw new Exception("Database already filled.");
            }

            var carts = new List<Cart>
            {
                new Cart(),
                new Cart()
            };

            var infos = new List<Info>
            {
                new Info
                {
                    Address="Adresa 1",
                    City="Beograd",
                    Country="Srbija",
                    PostalCode="11000"
                },
                new Info
                {
                    Address="Adresa 2",
                    Country = "Srbija",
                    City="Novi Sad",
                    PostalCode="12000"
                }
            };

            var users = new List<User>
            {
                new User
                {
                    FirstName="Admin",
                    LastName="Admirovic",
                    Email="admin@gmail.com",
                    Username="admin123",
                    Password="$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",//Sifra123
                    IsBanned=false,
                    Cart=carts[0],
                    Info=infos[0],
                    Role=1
                },
                new User
                {
                    FirstName="User",
                    LastName="Useric",
                    Email="user@gmail.com",
                    Username="user123",
                    Password="$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",//Sifra123
                    IsBanned=false,
                    Cart=carts[1],
                    Info=infos[1]
                },
                new User
                {
                    FirstName="Anon",
                    LastName="Anonic",
                    Email="anonimous@gmail.com",
                    Username="anonimous123",
                    Password="$2a$11$Ahl8NDzdxDryw1hhJdck/OSJk8W6fZtCiiZbpDt2nRjyDdmHTJLyW",//Sifra123
                    IsBanned=false,
                    Cart=new Cart(),
                    Info=new Info()
                }
            };

            var sizes = new List<Size>
            {
                new Size
                {
                    SizeName="Size 1"
                },
                new Size
                {
                    SizeName="Size 2"
                },
                new Size
                {
                    SizeName="Size 3"
                },
                new Size
                {
                    SizeName="Size 4"
                },
                new Size
                {
                    SizeName="Size 5"
                },
                new Size
                {
                    SizeName="Size 6"
                },
                new Size
                {
                    SizeName="Size 7"
                },
                new Size
                {
                    SizeName="Size 8"
                },
                new Size
                {
                    SizeName="Size 9"
                },
                new Size
                {
                    SizeName="Size 10"
                }
            };

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryName="Category 1"
                },
                new Category
                {
                    CategoryName="Category 2"
                },
                new Category
                {
                    CategoryName="Category 3"
                },
                new Category
                {
                    CategoryName="Category 4"
                },
                new Category
                {
                    CategoryName="Category 5"
                }
            };

            var brands = new List<Brand>
            {
                new Brand
                {
                    BrandName="Brand 1"
                },
                new Brand
                {
                    BrandName="Brand 2"
                },
                new Brand
                {
                    BrandName="Brand 3"
                },
                new Brand
                {
                    BrandName="Brand 4"
                },
                new Brand
                {
                    BrandName="Brand 5"
                }
            };

            var gender = new List<Gender>
            {
                new Gender
                {
                    GenderName="Male"
                },
                new Gender
                {
                    GenderName="Female"
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    ProductName="Product 1",
                    Category=categories[0],
                    Brand=brands[0],
                    Gender=gender[0],
                    Sale=true,
                    InStock=false,
                    Material="Material 1",
                    CountryOfOrigin="Country 1"
                },
                new Product
                {
                    ProductName="Product 2",
                    Category=categories[1],
                    Brand=brands[1],
                    Gender=gender[1],
                    Sale=true,
                    InStock=true,
                    Material="Material 2",
                    CountryOfOrigin="Country 2"
                },
                new Product
                {
                    ProductName="Product 3",
                    Category=categories[2],
                    Brand=brands[2],
                    Gender=gender[1],
                    Sale=false,
                    InStock=false,
                    Material="Material 3",
                    CountryOfOrigin="Country 3"
                },
                new Product
                {
                    ProductName="Product 4",
                    Category=categories[3],
                    Brand=brands[3],
                    Gender=gender[1],
                    Sale=true,
                    InStock=false,
                    Material="Material 4",
                    CountryOfOrigin="Country 4"
                },
                new Product
                {
                    ProductName="Product 5",
                    Category=categories[4],
                    Brand=brands[4],
                    Gender=gender[0],
                    Sale=true,
                    InStock=false,
                    Material="Material 5",
                    CountryOfOrigin="Country 5"
                }
            };

            var pictures = new List<Picture>
            {
                new Picture
                {
                    Product=products[0],
                    Src="image1.jpg",
                    Alt="Alt1"
                },
                new Picture
                {
                    Product=products[1],
                    Src="image2.jpg",
                    Alt="Alt2"
                },
                new Picture
                {
                    Product=products[2],
                    Src="image3.jpg",
                    Alt="Alt3"
                },
                new Picture
                {
                    Product=products[3],
                    Src="image4.jpg",
                    Alt="Alt4"
                },
                new Picture
                {
                    Product=products[4],
                    Src="image5.jpg",
                    Alt="Alt5"
                }
            };

            var productSizes = new List<ProductSize>
            {
                new ProductSize
                {
                    Product=products[0],
                    Size=sizes[0]
                },
                new ProductSize
                {
                    Product=products[0],
                    Size=sizes[1]
                },
                new ProductSize
                {
                    Product=products[0],
                    Size=sizes[2]
                },
                new ProductSize
                {
                    Product=products[1],
                    Size=sizes[0]
                },
                new ProductSize
                {
                    Product=products[1],
                    Size=sizes[1]
                },
                new ProductSize
                {
                    Product=products[1],
                    Size=sizes[2]
                },
                new ProductSize
                {
                    Product=products[2],
                    Size=sizes[0]
                },
                new ProductSize
                {
                    Product=products[2],
                    Size=sizes[1]
                },
                new ProductSize
                {
                    Product=products[2],
                    Size=sizes[2]
                },
                new ProductSize
                {
                    Product=products[3],
                    Size=sizes[0]
                },
                new ProductSize
                {
                    Product=products[3],
                    Size=sizes[1]
                },
                new ProductSize
                {
                    Product=products[3],
                    Size=sizes[2]
                },
                new ProductSize
                {
                    Product=products[4],
                    Size=sizes[0]
                },
                new ProductSize
                {
                    Product=products[4],
                    Size=sizes[1]
                },
                new ProductSize
                {
                    Product=products[4],
                    Size=sizes[2]
                }
            };

            var prices = new List<Price>
            {
                new Price
                {
                    Product=products[0],
                    NewPrice=99.99m,
                    OldPrice=69,
                    ActiveFrom=DateTime.Parse("2023-06-07")
                },
                new Price
                {
                    Product=products[0],
                    NewPrice=107.99m,
                    OldPrice=99.99m,
                    ActiveFrom=DateTime.Parse("2023-06-14")
                },
                new Price
                {
                    Product=products[1],
                    NewPrice=79.99m,
                    ActiveFrom=DateTime.Parse("2023-05-08")
                },
                new Price
                {
                    Product=products[2],
                    NewPrice=70.99m,
                    ActiveFrom=DateTime.Parse("2023-04-07")
                },
                new Price
                {
                    Product=products[3],
                    NewPrice=120.99m,
                    ActiveFrom=DateTime.Parse("2023-06-01")
                },
                new Price
                {
                    Product=products[4],
                    NewPrice=59,
                    ActiveFrom=DateTime.Parse("2023-03-07")
                }
            };

            var useCases = new List<UseCase>
            {
                new UseCase
                {
                    Name="Get categories",
                    Description="Get categories with/without keyword."
                },
                new UseCase
                {
                    Name="Create category",
                    Description="Creating new category"
                },
                new UseCase
                {
                    Name="Get users",
                    Description="Get users with/without keyword."
                },
                new UseCase
                {
                    Name="Create new user",
                    Description="Create new user description"
                },
                new UseCase
                {
                    Name="Get products",
                    Description="Search and filter products."
                },
                new UseCase
                {
                    Name="Update user info",
                    Description="Update user information(Address, City, etc.)"
                },
                new UseCase
                {
                    Name="Get single product",
                    Description="Detailed product"
                },
                new UseCase
                {
                    Name="Get single cart",
                    Description="Get single cart details."
                },
                new UseCase
                {
                    Name="Add to cart.",
                    Description="Add product to cart."
                },
                new UseCase
                {
                    Name="Update cart item.",
                    Description="Update cart item details."
                },
                new UseCase
                {
                    Name="Delete cart item",
                    Description="Delete cart item with provided id"
                },
                new UseCase
                {
                    Name="Create product.",
                    Description="Create new product."
                },
                new UseCase
                {
                    Name="Delete product",
                    Description="Delete product with pictures, prices included."
                },
                new UseCase
                {
                    Name="Get single receipt.",
                    Description="Get single receipt informations."
                },
                new UseCase
                {
                    Name="Confirm order",
                    Description="Confirm order using user id"
                },
                new UseCase
                {
                    Name="Search receipts",
                    Description="Search receipts using dates and user id"
                },
                new UseCase
                {
                    Name="Delete receipt",
                    Description="Delete receipt with id provided."
                },
                new UseCase
                {
                    Name="Fill database",
                    Description="Initial fill database"
                },
                new UseCase
                {
                    Name="Add product price",
                    Description="Add new product price"
                }
            };

            var userUseCase = new List<UserUseCase>
            {
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[0]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[1]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[2]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[3]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[4]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[5]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[6]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[7]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[8]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[9]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[10]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[11]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[12]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[13]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[14]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[15]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[16]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[17]
                },
                new UserUseCase
                {
                    User=users[0],
                    UseCase=useCases[18]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[4]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[5]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[6]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[7]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[8]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[9]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[10]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[13]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[14]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[15]
                },
                new UserUseCase
                {
                    User=users[1],
                    UseCase=useCases[16]
                },
                new UserUseCase
                {
                    User=users[2],
                    UseCase=useCases[4]
                },
                new UserUseCase
                {
                    User=users[2],
                    UseCase=useCases[6]
                },
                new UserUseCase
                {
                    User=users[2],
                    UseCase=useCases[17]
                },
            };

            Context.Brands.AddRange(brands);
            Context.Categories.AddRange(categories);
            Context.Genders.AddRange(gender);
            Context.Sizes.AddRange(sizes);
            Context.Carts.AddRange(carts);
            Context.Products.AddRange(products);
            Context.Infos.AddRange(infos);
            Context.Users.AddRange(users);
            Context.Pictures.AddRange(pictures);
            Context.ProductSizes.AddRange(productSizes);
            Context.Prices.AddRange(prices);
            Context.UseCases.AddRange(useCases);
            Context.UserUseCases.AddRange(userUseCase);

            Context.SaveChanges();
        }
    }
}
