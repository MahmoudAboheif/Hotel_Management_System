using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Hotel_Management__Debugging_
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            int Role = 0;
            int Operation = 0;

            Console.WriteLine("Please identify your role and enter its number whether you are: 1.Manager, 2.Receptionist or 3.Food Service");
            Role = int.Parse(Console.ReadLine());

            if (Role == 1) //manager
            {
                while (true)
                {
                    Reservation G1 = new Reservation();
                    Console.WriteLine("What do you desire to do: 1.Edit");
                    Operation = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the Id");
                    G1.Id = Console.ReadLine();

                    //Edit
                    if (Operation == 1)
                    {
                        Console.WriteLine("Enter the operation:\n 1-Delete Reservation \n 2-Edit Reservation \n 3-Guest Info");
                        int Edit = int.Parse(Console.ReadLine());


                        //Delete
                        if (Edit == 1)
                        {
                            G1.DeleteClient(G1.Id);
                        }

                        //Edit Reservation date
                        else if (Edit == 2)
                        {
                            G1.EditCheckInDate(G1.Id);
                            G1.EditCheckOutDate(G1.Id);
                            G1.GetVacancy(G1.Id);
                        }

                        else if (Edit == 3)
                        {
                            G1.UpdateGuestInfo(G1.Id);
                        }

                     
                    }

                    //Bill
                    //else if (Operation == 2)
                    //{
                    //    G1.Bill(G1.Id);
                    //}

                }
            }
        

            else if (Role == 2) //receptionist
            {
                while (true)
                {
                    Reservation G1 = new Reservation();
                    Console.WriteLine("What do you desire to do: 1.Booking, 2.Check-in, 3.Check-out or 4.Edit");
                    Operation = int.Parse(Console.ReadLine());

                   
                    
                    //Booking
                    if (Operation == 1)
                    {
                        G1.InsertGuestInfo();
                        G1.CheckInDate(G1.Id);
                        G1.CheckOutDate(G1.Id);
                        G1.GetVacancy(G1.Id);
                    }

                    //Check-In
                    else if (Operation == 2)
                    {
                        Console.WriteLine("Enter the Id");
                        G1.Id = Console.ReadLine();

                        G1.CheckInTime(G1.Id);
                        
                    }

                    //Check-Out
                    else if (Operation == 3)
                    {
                        Console.WriteLine("Enter the Id");
                        G1.Id = Console.ReadLine();

                        G1.CheckOutTime(G1.Id);
                      //  G1.Bill(G1.Id);
                    }

                    //Edit
                    else if (Operation == 4)
                    {
                        Console.WriteLine("Enter the operation:\n 1-Delete Reservation \n 2-Edit Reservation \n 3-Guest Info");
                        int Edit = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the Id");
                        G1.Id = Console.ReadLine();

                        //Delete
                        if (Edit == 1)
                        {
                            G1.DeleteClient(G1.Id);
                        }

                        //Edit Reservation date
                        if (Edit == 2)
                        {
                            G1.EditCheckInDate(G1.Id);
                            G1.EditCheckOutDate(G1.Id);
                            G1.GetVacancy(G1.Id);
                        }

                        if (Edit == 3)
                        {
                            G1.UpdateGuestInfo(G1.Id);
                        }


                    }


                    else
                    {
                        Console.WriteLine("INVALID DATA ENTERED PLEASE START AGAIN");
                    }
                }
            }

            else if (Role == 3) // food service
            {

                Console.WriteLine("Enter your ID");

                string guest = Console.ReadLine();


                // Define a list of available meals with their prices
                List<Tuple<string, decimal>> availableMeals = new List<Tuple<string, decimal>>()
                           {
                             new Tuple<string, decimal>("Koshary",2.00m),
                             new Tuple<string, decimal>("Burger", 10.99m),
                             new Tuple<string, decimal>("Pizza", 12.99m),
                             new Tuple<string, decimal>("Salad", 8.99m),
                             new Tuple<string, decimal>("Spaghetti with Meat Balls", 11.99m),
                             new Tuple<string, decimal>("Spaghetti with Seafood", 13.00m),
                             new Tuple<string, decimal>("Sushi", 45.00m),
                             new Tuple<string, decimal>("Millefeuille", 30.00m),
                             new Tuple<string, decimal>("Brrito", 15.00m),
                             new Tuple<string, decimal>("Chicken Shawrma", 14.00m),
                             new Tuple<string, decimal>("Meat Shawrma", 19.00m),
                             new Tuple<string, decimal>("Mombaar", 9.00m),
                             new Tuple<string, decimal>("Kawar3", 30.00m),

                           };

                MealTracker mealTracker = new MealTracker();

                while (true)
                {
                    Console.WriteLine("Available Meals:");
                    for (int i = 0; i < availableMeals.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {availableMeals[i].Item1} - ${availableMeals[i].Item2}");
                    }

                    Console.WriteLine("Select a meal (enter the corresponding number):");
                    int mealChoice = int.Parse(Console.ReadLine());

                    if (mealChoice < 1 || mealChoice > availableMeals.Count)
                    {
                        Console.WriteLine("Invalid choice! Please select a valid meal.");
                        continue;
                    }

                    string selectedMealName = availableMeals[mealChoice - 1].Item1;
                    decimal selectedMealPrice = availableMeals[mealChoice - 1].Item2;

                    Console.WriteLine($"Selected meal: {selectedMealName} - ${selectedMealPrice}");

                    Console.WriteLine("Enter quantity:");
                    int quantity = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter payment amount:");
                    decimal paymentAmount = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Enter payment type visa or cash:");

                    string selected = Console.ReadLine();


                    Meal meal = new Meal(selectedMealName, selectedMealPrice);
                    MealPurchase mealPurchase = new MealPurchase(meal, quantity);

                    Console.WriteLine($"Total price for {quantity} {selectedMealName}(s):\n ${mealPurchase.GetTotalPrice()} \npaid by {selected}");


                    decimal totalPrice = mealPurchase.GetTotalPrice();  // Calculate total price

                    if (paymentAmount < totalPrice)
                    {
                        Console.WriteLine("The payment amount is less than the total price of the meal.");
                        return;
                    }


                    // If the payment amount is greater than the total price, calculate and display the change
                    if (paymentAmount > totalPrice)
                    {
                        decimal change = paymentAmount - totalPrice;
                        Console.WriteLine($"Payment received.\n Your change is: ${change}");
                    }
                    else
                    {
                        Console.WriteLine("Payment received. Thank you.");
                    }

                    mealPurchase.RecordPayment(paymentAmount);


                    Console.WriteLine("Meal purchase recorded successfully!");

                    Console.WriteLine("Do you want to make another purchase? (yes/no)");

                    string anotherPurchase = Console.ReadLine();
                    ;
                    if (anotherPurchase == "no")
                    {
                        break;
                    }
                    else if (anotherPurchase == "yes")
                        continue;
                }
            }

            else
            {
                Console.WriteLine("INVALID DATA ENTERED PLEASE START AGAIN");
            }

            return;

        }

    }
}