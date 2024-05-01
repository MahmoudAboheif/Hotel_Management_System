public class Meal
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Meal()
    {

    }
    public Meal(string name, decimal price)
    {
        Name = name;
        Price = price;

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
    public void RecordPayment(decimal amount)
    {
        if (Payment == null)  // Ensures that a payment can only be recorded once
        {
            Payment = new Payment(amount);
        }
        else
        {
            Console.WriteLine("Payment has already been recorded for this purchase.");
        }

    }



}
public class MealTracker
{
    private List<MealPurchase> purchases;

    public MealTracker()
    { }

    public void AddPurchase(MealPurchase purchase)
    {
        purchases.Add(purchase);

    }


}

public class Payment
{
    public decimal Amount { get; set; }

    public Payment(decimal amount)
    {
        Amount = amount;
    }

}