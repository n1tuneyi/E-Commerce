using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;
using Presentation.Authentication;

namespace Ecommerce.Presenters;

public class CartPresenter
{
    private readonly ProductService _productService;
    private readonly CartService _cartService;

    private readonly ProductPresenter _productPresenter;
    private readonly OrderPresenter _orderPresenter;

    public CartPresenter(ProductService productService, CartService cartService,
                         ProductPresenter productPresenter, OrderPresenter orderPresenter)
    {
        _productService = productService;
        _cartService = cartService;
        _productPresenter = productPresenter;
        _orderPresenter = orderPresenter;
    }

    public void CartOptions()
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
                UpdateCart();
                break;

            case 5:
                return;
        }

        CartOptions();
    }

    void UpdateCart()
    {
        long prodID;

        do
        {
            string input = CartView.GetProductIDToUpdate();
            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    _productPresenter.ProductsMenu();
                    return;
                }

                if (!_productService.Exists(prodID) || !_cartService.HasInCart(prodID, UserSession.CurrentUser.Id))
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
            Console.Write("Enter the new Quantity: ");

            if (int.TryParse(Console.ReadLine()!, out int input) && input > 0)
            {
                quantity = input;

                if (!_cartService.CanAddToCart(prodID, quantity, UserSession.CurrentUser.Id))
                    Console.WriteLine("Not Enough Stock");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid Quantity!");

        } while (true);

        _cartService.UpdateItem(prodID, quantity, userID: UserSession.CurrentUser.Id);

        CartView.ShowSuccessfulUpdatedItemMessage();
    }

    public void ViewCart()
    {
        ShoppingCart cart = _cartService.GetByUserId(UserSession.CurrentUser.Id);
        if (cart is null || cart.Items is null || cart.Items.Count == 0)
        {
            CartView.ShowEmptyCart();
            _productPresenter.ProductsMenu();
        }
        else
        {
            CartView.ShowCart(cart);
            _orderPresenter.Checkout();
        }
    }

    public void AddToCart()
    {
        long prodID;

        do
        {
            string input = CartView.GetProductIDToAdd();
            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    _productPresenter.ProductsMenu();
                    return;
                }

                if (!_productService.Exists(prodID))
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

                if (!_cartService.CanAddToCart(prodID, quantity, UserSession.CurrentUser.Id))
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
            Quantity = quantity,
            CreatedBy = UserSession.CurrentUser.Id,
            //Product = _productService.FindById(prodID)
        };
        Console.WriteLine(item.CreatedDate);

        _cartService.AddToCart(item, userId: UserSession.CurrentUser.Id);

        CartView.ShowSuccessfulAddedToCartMessage();
    }

    public void RemoveFromCart()
    {
        long prodID;

        do
        {
            string input = CartView.GetProductIDToRemove();
            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    _productPresenter.ProductsMenu();
                    return;
                }

                if (!_productService.Exists(prodID) || !_cartService.HasInCart(prodID, UserSession.CurrentUser.Id))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID");
        } while (true);

        _cartService.RemoveFromCart(prodID, UserSession.CurrentUser.Id);

        CartView.ShowSuccessfulRemovedFromCartMessage();
    }
}
