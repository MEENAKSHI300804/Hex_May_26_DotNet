using System;
using EcommerceBillingApp.Models;
using EcommerceBillingApp.Services;

namespace EcommerceBillingApp
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter Product Price: ");
            decimal price =
                Convert.ToDecimal(Console.ReadLine());

            if (price <= 0)
            {
                Console.WriteLine("Price must be greater than 0");
                return;
            }

            Console.Write("Enter Quantity: ");
            int quantity =
                Convert.ToInt32(Console.ReadLine());

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0");
                return;
            }

            Order order = new Order
            {
                ProductPrice = price,
                Quantity = quantity
            };

            OrderBillingService billing =
                new OrderBillingService();

            decimal subTotal =
                billing.CalculateSubTotal(
                    order.ProductPrice,
                    order.Quantity);

            decimal discount =
                billing.CalculateDiscount(subTotal);

            decimal amountAfterDiscount =
                subTotal - discount;

            decimal deliveryCharge =
                billing.CalculateDeliveryCharge(
                    amountAfterDiscount);

            decimal finalAmount =
                amountAfterDiscount + deliveryCharge;

            Console.WriteLine("\n----- BILL -----");
            Console.WriteLine($"Sub Total       : {subTotal}");
            Console.WriteLine($"Discount        : {discount}");
            Console.WriteLine($"Delivery Charge : {deliveryCharge}");
            Console.WriteLine($"Final Amount    : {finalAmount}");
        }
    }
}