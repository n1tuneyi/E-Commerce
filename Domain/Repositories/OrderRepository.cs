using Ecommerce.Domain;
namespace Ecommerce.Repositories;

public static class OrderRepository
{
    private static readonly List<Order> Orders = new List<Order>();

    public static List<Order> GetAll(long userId)
    {
        return Orders.FindAll(order => order.UserId == userId);
    }

    public static Order Create(Order order)
    {
        order.Id = Orders.Count + 1;

        Orders.Add(order);

        return order;
    }




}
