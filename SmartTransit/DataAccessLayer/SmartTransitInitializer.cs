using SmartTransit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace SmartTransit.DataAccessLayer
{
    public class SmartTransitInitializer : DropCreateDatabaseIfModelChanges<SmartTransitContext>
    {

        protected override void Seed(SmartTransitContext context)
        {
            // Client
            var clients = new List<Client>
            {
            new Client{  ClientID = "CL1001", FirstName ="Alexander", LastName =" Flemming ", PhoneNo = "0834158504" , Address = "Tallaght, Dublin" },
            new Client{ ClientID = "CL1002" ,FirstName ="James", LastName =" Joyce ", PhoneNo = "0874158504" , Address = "Clondalkin, Dublin" },
            new Client{ ClientID = "CL1003", FirstName ="Sheeda", LastName =" Talli ", PhoneNo = "0874158504" , Address = "Rathgar, Dublin" }
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            // Driver
            var drivers = new List<Driver>
            {
            new Driver{   DriverID = "DR1001", FirstName ="James", LastName =" McDwyer ", PhoneNo = "0834158504" , Address = "Tallaght, Dublin" , LoginStatus = "Yes"},
            new Driver{ DriverID = "DR1002" ,FirstName ="Peter", LastName =" Jawener ", PhoneNo = "0874158504" , Address = "Clondalkin, Dublin" , LoginStatus = "Yes" },

            };

            drivers.ForEach(c => context.Drivers.Add(c));
            context.SaveChanges();

            // Deliveries

            var deleries = new List<Delivery>
            {
            new Delivery{  DeliveryID = "UN1001",  ClientID = "CL1001",  Date = DateTime.Today,  DriverID = "DR1001",  PickUpLocation = "Tallaght" ,DeliverTo = "Sword" ,  CurrentStatus = "Delivery" },

            };

            deleries.ForEach(c => context.Deliveries.Add(c));
            context.SaveChanges();

            // Log History
            var loghosteries = new List<LogHistory>
            {
            new LogHistory{  DeliveryID = "UN1001", Date = DateTime.Today,  Status = "Ready for delivery"  },

            };

            loghosteries.ForEach(c => context.LogsHistory.Add(c));
            context.SaveChanges();

        }


    }
}