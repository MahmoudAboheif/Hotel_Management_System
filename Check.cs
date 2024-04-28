using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management__Debugging_
{

    public class Check : LCheckDate, LCheckTime, LUpdateCheckDate, LBill
    {

        //CheckIn Variables
        /*--------------------------------------------------------------------------------------------------------*/

        private int Days_number;

        protected DateOnly ArrivalDate;
        protected TimeSpan ArrivalTime;

        protected DateOnly DepartureDate;
        protected TimeSpan DepartureTime;

        protected string status;
        /*--------------------------------------------------------------------------------------------------------*/


        // Property of Number of Days
        private int Number_of_Days
        {
            get { return Days_number; }
            set { Days_number = value; }
        }

        /*--------------------------------------------------------------------------------------------------------*/
        public int RoomBill;

        DBAccess Db1 = new DBAccess(); //The Variable that will link to the Database

        public void CheckInDate(string Id)
        {
            Console.WriteLine("Enter the Date of the CheckIn in the format YYYY-MM-DD. ");
            string inputDate = Console.ReadLine();
            if (DateOnly.TryParse(inputDate, out ArrivalDate)) //Assigning the Arrival Date
            {
                Console.WriteLine($"You entered: {ArrivalDate}");

                //Storing Database
                Db1.CheckInDate(ArrivalDate, Id);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in the format YYYY-MM-DD.");
            }

        }



        //Storing the Arrival time of CheckIn
        public void CheckInTime(string Id)
        {
            TimeSpan Arrival1 = DateTime.Now.TimeOfDay;

            Console.WriteLine($"Arrival Time is {Arrival1}");
            ArrivalTime = Arrival1;
            PaymentStatus(Id);
            Console.WriteLine($"Payment Status: {status}");
            Db1.UpdateArrivalTime(ArrivalTime, Id, status);
        }


        //Check Out

        public void CheckOutDate(string Id)
        {
            Console.WriteLine("Enter the Date of the Check Out in the format YYYY-MM-DD. ");
            string inputDate = Console.ReadLine();
            if (DateOnly.TryParse(inputDate, out DepartureDate)) //Assigning the Arrival Date
            {
                Console.WriteLine($"You entered: {DepartureDate}");

                //Storing Database
                Db1.CheckOutDate(DepartureDate, Id);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in the format YYYY-MM-DD.");
            }

        }


        //Storing the Departure time of CheckOut
        public void CheckOutTime(string Id)
        {
            TimeSpan Departure = DateTime.Now.TimeOfDay;
            //  Hour = Arrival.Hours;
            //  Minute = Arrival.Minutes;

            //Console.WriteLine($"Arrival Time is {Hour}:{Minute}");
            Console.WriteLine($"Departure Time is {Departure}");
            DepartureTime = Departure;
            Db1.UpdateDepartureTime(Departure, Id);
        }


        //Editing reservation:

        //Edit CheckIn
        public void EditCheckInDate(string id)
        {
            Console.WriteLine("Enter the New Date of the CheckIn in the format YYYY-MM-DD. ");
            string inputDate = Console.ReadLine();

            if (DateOnly.TryParse(inputDate, out ArrivalDate)) //Assigning the Arrival Date
                Console.WriteLine($"You entered: {ArrivalDate}");

            else
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in the format YYYY-MM-DD.");
            }

            DBAccess dbAccess = new DBAccess();
            dbAccess.UpdateCheckInDate(id, ArrivalDate);

        }

        //Edit Checkout
        public void EditCheckOutDate(string id)
        {
            Console.WriteLine("Enter the Date of the Check Out in the format YYYY-MM-DD. ");
            string inputDate = Console.ReadLine();

            if (DateOnly.TryParse(inputDate, out DepartureDate)) //Assigning the Arrival Date
                Console.WriteLine($"You entered: {DepartureDate}");

            else
                Console.WriteLine("Invalid date format. Please enter a valid date in the format YYYY-MM-DD.");


            DBAccess dbAccess = new DBAccess();
            dbAccess.UpdateCheckOutDate(id, DepartureDate);
        }



        //Check the Payment Status
        private void PaymentStatus(string Id)
        {
            TimeSpan Status = new TimeSpan(18, 0, 0);

            if (ArrivalTime >= Status)
            {
                status = "Must Pay";
            }
            else
            {
                status = "No Obligation for Now";
            }
            Db1.UpdateArrivalStatus(status, Id);
        }



        protected int Price_Per_Night;

        public void Bill()
        {
            Days_number = DepartureDate.Day - ArrivalDate.Day;
            RoomBill = Days_number * Price_Per_Night;
            Console.WriteLine($"Room Bill: {RoomBill}");
        }




    }//End of Check

}
