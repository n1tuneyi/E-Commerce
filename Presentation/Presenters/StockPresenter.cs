using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;
using Presentation.Authentication;

namespace Ecommerce.Presenters;

public class StockPresenter
{

    private readonly ProductService _productService;

    private readonly ProductPresenter _productPresenter;

    public StockPresenter(ProductService productService, ProductPresenter productPresenter)
    {
        _productService = productService;
        _productPresenter = productPresenter;
    }

    public void StockOptions()
    {
        StockView.ShowStockOptions();
        int choice = StockView.GetMenuSelection();

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
                return;
        }

        StockOptions();
    }

    private void AddToStock()
    {
        string name, description;
        decimal price;
        int quantity;

        name = View.GetInput("Enter name of the product: ");

        description = View.GetInput("Enter Description of the product: ");

        price = decimal.Parse(View.GetInput("Enter Price: "));

        quantity = int.Parse(View.GetInput("Enter Stock Quantity: "));

        Product addedProduct = new Product()
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = quantity,
            CreatedBy = UserSession.CurrentUser.Id,
        };

        _productService.Add(addedProduct);

        StockView.ShowSuccessfulAddedProductMessage();
    }

    private void RemoveFromStock()
    {
        long prodID;

        do
        {
            string input = StockView.GetProductIDToRemove();

            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    _productPresenter.ProductsMenu();
                    StockOptions();
                    return;
                }

                if (!_productService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID: ");
        } while (true);

        _productService.Remove(prodID);

        StockView.ShowSuccessfulRemovedProductMessage();
    }


    private void UpdateStock()
    {
        long prodID;

        do
        {
            string input = StockView.GetProductIDToUpdate();

            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    _productPresenter.ProductsMenu();
                    StockOptions();
                    return;
                }

                if (!_productService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID: ");
        } while (true);


        StockView.ShowUpdateOptions();

        int choice = StockView.GetMenuSelection();

        switch (choice)
        {
            case 1:
                string newName = View.GetInput("Enter new name: ");
                _productService.UpdateName(prodID, newName);
                break;
            case 2:
                string newDescription = View.GetInput("Enter new description: ");
                _productService.UpdateDescription(prodID, newDescription);
                break;
            case 3:
                decimal newPrice = decimal.Parse(View.GetInput("Enter new price: "));
                _productService.UpdatePrice(prodID, newPrice);
                break;
            case 4:
                int newQuantity = int.Parse(View.GetInput("Enter new quantity: "));
                _productService.UpdateStockQuantity(prodID, newQuantity);
                break;
        }

        StockView.ShowSuccessfulUpdatedProductMessage();
    }
}
