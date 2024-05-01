using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace Hotel_Management__Debugging_
{

    public class Reservation : Check
    {
        protected string firstname;
        protected string lastName;
        protected string phone_number;
        protected string id;
        protected int room_number;
        protected string roomtype;
     

        // Creating Properties
        /*--------------------------------------------------------------------------------*/

        public string Id
        {
            get { return id; }
            set
            {
                //if (value.Length == 14) // Checking the length of the national id
                //{
                id = value;
                //}
            }
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string PhoneNumber
        {
            get { return phone_number; }

            set
            {
                if (value.Length == 11) //Cheacking the Length of the Phone Number is correct
                    phone_number = value;
            }

        }

        public int RoomNumber
        {
            get { return room_number; }
            set
            {
                if (value > 0 || value < 1000)
                    room_number = value;
            }
        }

        public string RoomType
        {
            get { return roomtype; }
            set
            {
                if (value == "Single" || value == "Double" || value == "Suite")
                {
                    roomtype = value;
                }
            }
        }

        /*--------------------------------------------------------------------------------*/

        //Default Constructor
        public Reservation() 
        { }

        //parameterized Constructor
        public Reservation(string firstname, string lastName, string phone_number, string id, int room_number, string roomtype)
        {
            this.firstname = firstname;
            this.lastName = lastName;
            this.phone_number = phone_number;
            this.id = id;
            this.room_number = room_number;
            this.roomtype = roomtype;
        }

        /*--------------------------------------------------------------------------------------------------------*/


        DBAccess dbAccess = new DBAccess();

        //A Method to Read the ClientInfo
        public void InsertGuestInfo()
        {
            Console.WriteLine("Please Enter Your First Name");
            firstname = Console.ReadLine();

            Console.WriteLine("Please Enter Your Last Name");
            lastName = Console.ReadLine();

            Console.WriteLine("Please Enter Your Phone Number");
            phone_number = Console.ReadLine();

            Console.WriteLine("Enter your Id: ");
            id = Console.ReadLine();

            DBAccess dbAccess1 = new DBAccess();
            dbAccess.InsertGuestInfo(firstname,lastName,phone_number,id);
            Console.WriteLine("Guest Information Added Successfully");


        }

        public void UpdateGuestInfo(string id)
        {
            Console.WriteLine("Please Enter Your First Name");
            firstname = Console.ReadLine();

            Console.WriteLine("Please Enter Your Last Name");
            lastName = Console.ReadLine();

            Console.WriteLine("Please Enter Your Phone Number");
            phone_number = Console.ReadLine();

            dbAccess.UpdateGuestInfo(id, firstname, lastName, phone_number);
            Console.WriteLine("Guest Information Updated Successfully");

        }

        public void DeleteClient(string id)
        {
            dbAccess.DeleteGuestInfo(id);
            dbAccess.DeleteReservation(id);
            Console.WriteLine("Reservation Deleted Successfully");
        }


        //Searching int the client info table and returning values by calling the method inside the database 
        //public Reservation SearchCustomerById(string id)
        //{
        //    DBAccess dbAccess1 = new DBAccess();

        //    try
        //    {
        //        return dbAccess1.SearchCustomerById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during the search
        //        Console.WriteLine("An error occurred while searching for the customer: " + ex.Message);
        //        return null;
        //    }
        //}


        /*--------------------------------------------------------------------------------------------------------*/


        public string GetVacancy(string id)
        {

            Console.WriteLine("Enter the room type the Guest desires, whether Single, Double, or Suite:");
            RoomType = Console.ReadLine();

            int startRoomNumber = 0;
            int endRoomNumber = 0;
            switch (RoomType)
            {
                case "Single":
                    startRoomNumber = 1;
                    endRoomNumber = 600;
                    Price_Per_Night = 100;
                    break;

                case "Double":
                    startRoomNumber = 601;
                    endRoomNumber = 900;
                    Price_Per_Night = 150;
                    break;

                case "Suite":
                    startRoomNumber = 901;
                    endRoomNumber = 1000;
                    Price_Per_Night = 250;
                    break;

                default:
                    return "Invalid room type !!";
            }

            DateTime arrivalDateTime = new DateTime(ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day);
            DateTime departureDateTime = new DateTime(DepartureDate.Year, DepartureDate.Month, DepartureDate.Day);

            for (int i = startRoomNumber; i <= endRoomNumber; i++)
            {
                if (!IsRoomOccupied(i, arrivalDateTime, departureDateTime))
                {
                    RoomNumber = i;
                    BookRoom(RoomNumber, id); // Book the room if it's available
                    Bill(id);
                    return $"Room number {i} is vacant";
                }
            }

            return $"All rooms of type {RoomType} are occupied";
        }

        private bool IsRoomOccupied(int roomNumber, DateTime checkInDateTime, DateTime checkOutDateTime)
        {

            DBAccess dbAccess = new DBAccess(); // Initialize DBAccess
            return dbAccess.IsRoomOccupiedForDates(roomNumber, checkInDateTime, checkOutDateTime);
        }

        private void BookRoom(int roomNumber, string id)
        {
            DateTime arrivalDateTime = new DateTime(ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day);
            DateTime departureDateTime = new DateTime(DepartureDate.Year, DepartureDate.Month, DepartureDate.Day);


            DBAccess dbAccess = new DBAccess(); // Initialize DBAccess

            if (!dbAccess.IsRoomOccupiedForDates(roomNumber, arrivalDateTime, departureDateTime))
            {
                dbAccess.UpdateRoomNumber(id, roomNumber, RoomType); // Book the room in the database
                dbAccess.UpdateRoomNumber_Guestinfo(id, roomNumber);
                Console.WriteLine($"Room {roomNumber} is now booked successfully");
            }
            else
            {
                Console.WriteLine($"Room {roomNumber} is already occupied");
            }

        }


    }//End of Class

}//End of Program