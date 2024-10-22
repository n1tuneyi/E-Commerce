using Ecommerce.Domain;
using Ecommerce.Services;
using Ecommerce.Views;

namespace Ecommerce.Presenters;

public class StockPresenter
{
    public static void StockOptions()
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
                AdminPresenter.AdminControlMenu();
                return;
        }

        StockOptions();
    }

    private static void AddToStock()
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

        ProductService.Add(addedProduct);

        StockView.ShowSuccessfulAddedProductMessage();
    }

    private static void RemoveFromStock()
    {
        long prodID;

        do
        {
            string input = StockView.GetProductIDToRemove();

            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    ProductPresenter.ProductsMenu();
                    StockOptions();
                    return;
                }

                if (!ProductService.Exists(prodID))
                    Console.WriteLine("The Product ID you entered is invalid!");

                else
                    break;
            }
            else
                Console.WriteLine("Please enter valid ID: ");
        } while (true);

        ProductService.RemoveProduct(prodID);

        StockView.ShowSuccessfulRemovedProductMessage();
    }


    private static void UpdateStock()
    {
        long prodID;

        do
        {
            string input = StockView.GetProductIDToUpdate();

            if (long.TryParse(input, out prodID))
            {
                if (prodID == -1)
                {
                    ProductPresenter.ProductsMenu();
                    StockOptions();
                    return;
                }

                if (!ProductService.Exists(prodID))
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
                ProductService.UpdateName(prodID, newName);
                break;
            case 2:
                string newDescription = IView.GetInput("Enter new description: ");
                ProductService.UpdateDescription(prodID, newDescription);
                break;
            case 3:
                decimal newPrice = decimal.Parse(IView.GetInput("Enter new price: "));
                ProductService.UpdatePrice(prodID, newPrice);
                break;
            case 4:
                int newQuantity = int.Parse(IView.GetInput("Enter new quantity: "));
                ProductService.UpdateQuantity(prodID, newQuantity);
                break;
        }

        StockView.ShowSuccessfulUpdatedProductMessage();
    }
}
