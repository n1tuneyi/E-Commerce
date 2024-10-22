using Ecommerce.Domain;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class CartPresenter
{
    public static void CartOptions()
    {
        CartView.ShowCartOptions();
        int choice = CartView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                ViewCart();
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
                return;
        }

        CartOptions();
    }

    public static void ViewCart()
    {
        ShoppingCart cart = CartRepository.FindByUserId(UserRepository.currentUser.Id)!;
        if (cart is null || cart.Items?.Count == 0)
        {
            CartView.ShowEmptyCart();
            ProductPresenter.ProductsMenu();
        }
        else
        {
            CartView.ShowCart(cart);
            OrderPresenter.Checkout();
        }
    }

    public static void AddToCart()
    {
        long prodID;

        do
        {
            string input = CartView.GetProductIDToAdd();
            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    ProductPresenter.ProductsMenu();
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

        CartService.AddToCart(item, UserRepository.currentUser.Id);

        CartView.ShowSuccessfulAddedToCartMessage();
    }

    public static void RemoveFromCart()
    {
        long prodID;

        do
        {
            string input = CartView.GetProductIDToRemove();
            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    ProductPresenter.ProductsMenu();
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

        CartService.RemoveFromCart(prodID, UserRepository.currentUser.Id);

        CartView.ShowSuccessfulRemovedFromCartMessage();
    }
}
