using Ecommerce.Domain;
using Ecommerce.Repositories;
using Ecommerce.Services;

namespace Ecommerce;

public class ProgramManager
{
    public static User? currentUser { get; set; }
    public static void Run()
    {
        MainMenu();
    }

    static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Login");
        Console.WriteLine("2- Signup");
        Console.WriteLine("3- Exit the Application");

        Console.Write("Choose one of the options above: ");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                LoginMenu();
                break;

            case 2:
                SignupMenu();
                break;

            case 3:
                Console.WriteLine("Exiting Application ...");
                Thread.Sleep(1000);
                Environment.Exit(0);
                break;

            default:
                InvalidInput();
                MainMenu();
                break;
        }

    }

    static void LoginMenu()
    {
        Console.Clear();
        Console.WriteLine("LOGIN MENU");
        Console.WriteLine(new string('-', count: 15));
        Console.Write("Please enter your username: ");
        string username = Console.ReadLine()!;
        Console.Write("Please enter your password: ");
        string password = Console.ReadLine()!;

        try
        {
            currentUser = AuthenticationService.Login(username, password);

            if (currentUser.Username == "admin")
                AdminControlMenu();

            else
                UserControlMenu();

        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1000);
            LoginMenu();
        }
    }

    static void SignupMenu()
    {
        Console.Clear();
        Console.WriteLine("SIGNUP MENU");
        Console.WriteLine(new string('-', count: 15));
        string? username, password, email, address;

        Console.Write("Please Enter your Username: ");
        username = Console.ReadLine();

        Console.Write("Please Enter your Password: ");
        password = Console.ReadLine();

        Console.Write("Please Enter your Email: ");
        email = Console.ReadLine();

        Console.Write("Please Enter your Address: ");
        address = Console.ReadLine();

        User user = new User()
        {
            Username = username,
            Password = password,
            Email = email,
            Address = address
        };

        try
        {
            currentUser = UserRepository.Create(user);

            UserControlMenu();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Occurred: {e.Message}");
            Thread.Sleep(1500);
            SignupMenu();
        }
    }
    static void AdminControlMenu()
    {
        Console.Clear();
        Console.WriteLine("CONTROL MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- View Current Products");
        Console.WriteLine("2- Log out");

        Console.Write("Choose one of the options above ");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                ProductsMenu();
                break;

            case 2:
                MainMenu();
                break;
        }

        AdminControlMenu();
    }

    static void UserControlMenu()
    {
        Console.Clear();
        Console.WriteLine("CONTROL MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Browse Products");
        Console.WriteLine("2- View Order History");
        Console.WriteLine("3- Log out");

        Console.Write("Choose one of the options above ");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                ProductsMenu();
                break;

            case 2:
                ViewOrderHistory();
                break;

            case 3:
                MainMenu();
                break;

            default:
                InvalidInput();
                break;
        }

        UserControlMenu();
    }


    static void ProductsMenu()
    {
        Console.Clear();
        Console.WriteLine("Available Products");

        List<Product> availableProducts = ProductService.GetProducts();

        if (availableProducts.Count == 0)
        {
            Console.WriteLine("There are no available products yet.");
            Thread.Sleep(1000);
        }
        else
        {
            foreach (Product product in availableProducts)
            {
                Console.WriteLine(new string('-', count: 10));
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Available Stock: {product.StockQuantity}");
            }

            if (currentUser.Username == "admin")
                StockOptionsMenu();
            else
                CartOptionsMenu();
        }
    }

    static void StockOptionsMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1- Add Products to Stock ");
        Console.WriteLine("2- Remove Products from Stock");
        Console.WriteLine("3- Update Products in Stock");
        Console.WriteLine("4- Back to Control Menu");

        Console.Write("Choose one of the options above: ");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                AddToStock();
                break;

            case 2:
                RemoveFromStock();
                break;

            case 3:
                UpdateStock();
                break;

            case 4:
                AdminControlMenu();
                break;

            default:
                InvalidInput();
                break;
        }

        ProductsMenu();
    }


    static void CartOptionsMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1- View Cart");
        Console.WriteLine("2- Add Product to Cart");
        Console.WriteLine("3- Remove Products from Cart");
        Console.WriteLine("4- Update Cart");
        Console.WriteLine("5- Back to Control Menu");
        Console.Write("Choose one of the options above: ");
        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                ViewCartMenu();
                CheckoutMenu();
                break;

            case 2:
                AddToCart();
                break;

            case 3:
                RemoveFromCart();
                break;

            case 4:
                break;

            case 5:
                UserControlMenu();
                return;

            default:
                InvalidInput();
                break;
        }

        ProductsMenu();
    }



    static void ViewCartMenu()
    {
        Console.WriteLine();
        Console.WriteLine();

        ShoppingCart cart = CartRepository.FindByUserId(currentUser.Id)!;

        if (cart is null || cart.Items?.Count == 0)
        {
            Console.WriteLine("Your Cart is empty!");
            Thread.Sleep(1000);
            ProductsMenu();
        }
        else
        {
            Console.WriteLine("Your Cart");
            Console.WriteLine(new string('-', count: 12));

            DisplayCart(cart.Items);

            Console.WriteLine($"Total Amount to pay is {cart.TotalPrice:C}");
        }
    }

    static void CheckoutMenu()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("CHECKOUT MENU");
        Console.WriteLine(new string('-', count: 10));

        Console.WriteLine("1- Proceed to Checkout");
        Console.WriteLine("2- Back to Previous Menu");

        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                PlaceOrder();
                break;

            case 2:
                break;

            default:
                InvalidInput();
                break;
        }
    }

    static void PlaceOrder()
    {
        Console.WriteLine("Order is being processed ...");
        Thread.Sleep(1000);
        OrderService.PlaceOrder(currentUser.Id);
        Console.WriteLine("Order successfully created!");
    }


    static void ViewOrderHistory()
    {
        List<Order> orders = OrderRepository.GetAll(currentUser.Id);

        if (orders.Count == 0)
        {
            Console.WriteLine("You haven't placed orders yet.");
            Thread.Sleep(1000);
            return;
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("ORDER HISTORY");
        Console.WriteLine(new string('-', count: 10));


        Console.WriteLine();
        foreach (Order order in orders)
        {
            Console.WriteLine($"Order ID: {order.Id}");
            Console.WriteLine($"Order Date: {order.Date}");
            Console.WriteLine();
            Console.WriteLine("Products Bought: ");
            DisplayCart(order.Items);
            Console.WriteLine($"Total Amount Paid: {order.TotalAmount:C}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        Console.WriteLine($"Press any key to return to previous menu");
        Console.ReadKey();
    }

    static void AddToStock()
    {
        Console.WriteLine();
        string name, description;
        decimal price;
        int quantity;

        Console.Write("Enter name of the product: ");
        name = Console.ReadLine();

        Console.Write("Enter Description of the product: ");
        description = Console.ReadLine();

        Console.Write("Enter Price: ");
        price = int.Parse(Console.ReadLine()!);

        Console.Write("Enter Stock Quantity: ");
        quantity = int.Parse(Console.ReadLine()!);

        Product addedProduct = new Product()
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = quantity
        };

        ProductService.Add(addedProduct);

        Console.WriteLine();
        Console.WriteLine("Item successfully added to stock!");
        Thread.Sleep(1000);
    }


    static void RemoveFromStock()
    {
        long prodID;

        do
        {
            Console.Write("Type the ID of product you want to remove from the stock or Enter -1 to return to the previous menu: ");
            if (long.TryParse(Console.ReadLine()!, out long input))
            {
                prodID = input;
                if (prodID == -1)
                {
                    ProductsMenu();
                    return;
                }

                if (!ProductService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID");
        } while (true);

        ProductService.RemoveProduct(prodID);
    }

    public static void UpdateStock()
    {
        long prodID;

        do
        {
            Console.Write("Type the ID of product you want to update from the stock or Enter -1 to return to the previous menu: ");
            if (long.TryParse(Console.ReadLine()!, out long input))
            {
                prodID = input;
                if (prodID == -1)
                {
                    ProductsMenu();
                    return;
                }

                if (!ProductService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID");
        } while (true);

        Console.WriteLine();

        Console.WriteLine("What do you want to update?");
        Console.WriteLine("1- Name");
        Console.WriteLine("2- Description");
        Console.WriteLine("3- Price");
        Console.WriteLine("4- Stock Quantity");

        Console.Write("Choose one of the options above: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine();
                ProductService.ChangeName(prodID, newName);
                break;

            case 2:
                Console.Write("Enter new description: ");
                string newDescription = Console.ReadLine();
                ProductService.ChangeDescription(prodID, newDescription);
                break;

            case 3:
                Console.Write("Enter new price: ");
                decimal newPrice = decimal.Parse(Console.ReadLine()!);
                ProductService.ChangePrice(prodID, newPrice);
                break;

            case 4:
                Console.Write("Enter new Quantity: ");
                int newQuantity = int.Parse(Console.ReadLine()!);
                ProductService.ChangeQuantity(prodID, newQuantity);
                break;

            default:
                break;
        }


    }

    static void AddToCart()
    {
        long prodID;

        do
        {
            Console.Write("Type the ID of product you want to add to your cart or Enter -1 to return to the previous menu: ");
            if (long.TryParse(Console.ReadLine()!, out long input))
            {
                prodID = input;
                if (prodID == -1)
                {
                    ProductsMenu();
                    return;
                }

                if (!ProductService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID");
        } while (true);

        int quantity = 0;

        do
        {
            Console.Write("Enter Quantity of the product you want to add: ");

            if (int.TryParse(Console.ReadLine()!, out int input) && input > 0)
            {
                quantity = input;

                if (!ProductService.HasEnoughStock(id: prodID, quantity))
                    Console.WriteLine("Not Enough Stock");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid Quantity!");

        } while (true);

        CartItem item = new CartItem()
        {
            ProductId = prodID,
            Quantity = quantity
        };

        CartService.AddToCart(item, currentUser.Id);
        Console.WriteLine();
        Console.WriteLine("Item added succesfully to your cart");
        Thread.Sleep(1000);
    }

    private static void RemoveFromCart()
    {
        long prodID;

        do
        {
            Console.Write("Type the ID of product you want to Remove from the cart or Enter -1 to return to the previous menu: ");
            if (long.TryParse(Console.ReadLine()!, out long input))
            {
                prodID = input;
                if (prodID == -1)
                {
                    ProductsMenu();
                    return;
                }

                if (!ProductService.Exists(prodID) || !CartService.HasInCart(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID");
        } while (true);

        CartService.RemoveFromCart(prodID, currentUser.Id);

        Console.WriteLine("Item removed successfully from your cart");
        Thread.Sleep(1000);
    }

    static void DisplayCart(List<CartItem> items)
    {
        foreach (CartItem item in items)
        {
            Product product = ProductService.Get(item.ProductId)!;
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Product name: {product.Name}");
            Console.WriteLine($"Quantity: {item.Quantity}");
            Console.WriteLine($"Total Price: {item.TotalPrice:C}");
            Console.WriteLine(new string('-', count: 10));
        }
    }

    static void InvalidInput()
    {
        Console.WriteLine("Invalid Input");
        Thread.Sleep(1000);
        Console.Clear();
    }

}
