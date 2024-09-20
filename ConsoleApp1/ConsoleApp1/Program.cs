using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Order order = new Order();
        order.AddItem(new Item { Name = "Товар 1", Price = 100, Quantity = 2 });
        order.AddItem(new Item { Name = "Товар 2", Price = 200, Quantity = 1 });

        order.SetPaymentMethod(new CreditCardPayment());
        order.SetDeliveryMethod(new CourierDelivery());

        order.ProcessOrder();
    }
}

public class Order
{
    private List<Item> items = new List<Item>();
    private IPayment paymentMethod;
    private IDelivery deliveryMethod;

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public double CalculateTotal(DiscountCalculator discountCalculator)
    {
        double total = items.Sum(item => item.Price * item.Quantity);
        return discountCalculator.ApplyDiscount(total);
    }

    public void SetPaymentMethod(IPayment payment)
    {
        paymentMethod = payment;
    }

    public void SetDeliveryMethod(IDelivery delivery)
    {
        deliveryMethod = delivery;
    }

    public void ProcessOrder()
    {
        double totalAmount = CalculateTotal(new DiscountCalculator());
        paymentMethod.ProcessPayment(totalAmount);
        deliveryMethod.DeliverOrder(this);
    }
}

public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

public interface IPayment
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата картой: {amount}");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal: {amount}");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Банковский перевод: {amount}");
    }
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ доставлен курьером.");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ отправлен почтой.");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ готов к получению в пункте выдачи.");
    }
}

public class DiscountCalculator
{
    public double ApplyDiscount(double total)
    {
        return total; 
    }
}
