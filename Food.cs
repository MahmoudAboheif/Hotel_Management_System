using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management__Debugging_
{
    public class Meal
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }


        public Meal(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
    public class MealPurchase
    {
        public Meal Meal { get; set; }
        public int Quantity { get; set; }
        public Payment Payment { get; private set; }


        public MealPurchase(Meal meal, int quantity)
        {
            Meal = meal;
            Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return Meal.Price * Quantity;
        }
        public void RecordPayment(decimal amount, PaymentType paymentType)
        {
            if (Payment == null)  // Ensures that a payment can only be recorded once
            {
                Payment = new Payment(amount, paymentType);
            }
            else
            {
                Console.WriteLine("Payment has already been recorded for this purchase.");
            }

        }
        public void BillCurrentRoom()
        {

            // Check if payment has been recorded
            if (Payment == null)
            {
                // Logic to bill the current room (or guest)
                // For demonstration, let's assume there's a method to bill the room
                BillRoom(GetTotalPrice());
            }
            else
            {
                Console.WriteLine("Payment has already been recorded for this purchase.");
            }
        }
        private void BillRoom(decimal amount)
        {

            Console.WriteLine($"Room billed for {amount} for {Quantity} {Meal.Name} ");
        }
    }
    public class MealTracker
    {
        private List<MealPurchase> purchases;

        public MealTracker()
        {
            purchases = new List<MealPurchase>();
        }

        public void AddPurchase(MealPurchase purchase)
        {
            purchases.Add(purchase);
        }

        public void PrintAllPurchases()
        {
            foreach (var purchase in purchases)
            {
                Console.WriteLine($"Meal: {purchase.Meal.Name}, Quantity: {purchase.Quantity}, Total: {purchase.GetTotalPrice()}");

                if (purchase.Payment != null)
                {
                    Console.WriteLine($"Paid {purchase.Payment.Amount:C2} via {purchase.Payment.PaymentType}");
                }
                else
                {
                    Console.WriteLine("Payment has not been recorded.");
                }
            }
        }
    }
    public class RestaurantReservation
    {
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; }

        public RestaurantReservation(int numberOfGuests, string specialRequests)
        {

            NumberOfGuests = numberOfGuests;
            SpecialRequests = specialRequests;
        }
    }
    public class RoomServiceReservation
    {
        public int RoomNumber { get; set; }
        public List<Meal> RequestedItems { get; set; }

        public RoomServiceReservation(int roomNumber, List<Meal> requestedItems)
        {
            RoomNumber = roomNumber;
            RequestedItems = requestedItems;

        }
    }

    public enum PaymentType
    {
        Cash,
        CreditCard,
        DebitCard,

    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }


        public Payment(decimal amount, PaymentType paymentType)
        {
            Amount = amount;
            PaymentType = paymentType;
        }
    }

}
