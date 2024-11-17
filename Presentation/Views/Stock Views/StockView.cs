

namespace Ecommerce.Views;

public class StockView : View
{
    public static void ShowStockOptions()
    {
        Console.WriteLine();
        Console.WriteLine("1- Add Products to Stock ");
        Console.WriteLine("2- Remove Products from Stock");
        Console.WriteLine("3- Update Products in Stock");
        Console.WriteLine("4- Back to Control Menu");

        Console.Write("Choose one of the options above: ");
    }

    public static int GetMenuSelection()
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            Console.Write("Invalid choice. Please select a valid choice: ");

        return choice;
    }



    public static string GetProductIDToRemove()
    {
        Console.Write("Type the ID of product you want to remove from the stock or Enter -1 to return to the previous menu: ");
        return Console.ReadLine();
    }
    public static string GetProductIDToUpdate()
    {
        Console.Write("Type the ID of product you want to update from the stock or Enter -1 to return to the previous menu: ");
        return Console.ReadLine();
    }

    public static void ShowUpdateOptions()
    {
        Console.WriteLine();
        Console.WriteLine("What do you want to update?");
        Console.WriteLine("1- Name");
        Console.WriteLine("2- Description");
        Console.WriteLine("3- Price");
        Console.WriteLine("4- Stock Quantity");

        Console.Write("Choose one of the options above: ");
    }

    public static void ShowSuccessfulRemovedProductMessage()
    {
        Notify("Item successfully removed from stock!");
    }

    public static void ShowSuccessfulAddedProductMessage()
    {
        Notify("Item successfully added to stock!");
    }
    public static void ShowSuccessfulUpdatedProductMessage()
    {
        Notify("Item successfully updated in stock!");
    }

}
