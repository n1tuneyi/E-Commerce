using Ecommerce.Domain.Entities;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class StockPresenter
{

    private readonly ProductService _productService;

    //private readonly AdminPresenter _adminPresenter;
    private readonly ProductPresenter _productPresenter;

    private readonly StockView _stockView;

    public StockPresenter(ProductService productService, ProductPresenter productPresenter)
    {
        _productService = productService;
        //_adminPresenter = adminPresenter;
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

                //_adminPresenter.AdminControlMenu();
                return;
        }

        StockOptions();
    }

    private void AddToStock()
    {
        string name, description;
        decimal price;
        int quantity;

        name = IView.GetInput("Enter name of the product: ");

        description = IView.GetInput("Enter Description of the product: ");

        price = decimal.Parse(IView.GetInput("Enter Price: "));

        quantity = int.Parse(IView.GetInput("Enter Stock Quantity: "));

        Product addedProduct = new Product()
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = quantity
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

        _productService.RemoveProduct(prodID);

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
                string newName = IView.GetInput("Enter new name: ");
                _productService.UpdateName(prodID, newName);
                break;
            case 2:
                string newDescription = IView.GetInput("Enter new description: ");
                _productService.UpdateDescription(prodID, newDescription);
                break;
            case 3:
                decimal newPrice = decimal.Parse(IView.GetInput("Enter new price: "));
                _productService.UpdatePrice(prodID, newPrice);
                break;
            case 4:
                int newQuantity = int.Parse(IView.GetInput("Enter new quantity: "));
                _productService.UpdateQuantity(prodID, newQuantity);
                break;
        }

        StockView.ShowSuccessfulUpdatedProductMessage();
    }
}
