using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
namespace DALCLASS.DBContact
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //public DbSet<CountryMaster> CountryMaster { get; set; }

        public DbSet<TrackingWebAPI.Models.CountryMaster> CountryMaster { get; set; }   
        public DbSet<TrackingWebAPI.Models.StateMaster> StateMaster { get; set; }

        public DbSet<TrackingWebAPI.Models.CompanyMaster> CompanyMaster { get; set; }
        public DbSet<TrackingWebAPI.Models.ZoneMaster> ZoneMaster { get; set; }
        public DbSet<TrackingWebAPI.Models.CityMaster> CityMaster { get; set; }
        //Added by Arun for Master Menu on 21-03-2026
        public DbSet<TrackingWebAPI.Models.OfficeRequestMaster> OfficeRequestMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.AirlineMaster> AirlineMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.TransportMaster> TransportMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.VendorMasters> VendorMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.VehicleMasters> vehicleMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.DeliveryBoyMasters> deliveryBoyMasters { get; set; }
        public DbSet<TrackingWebAPI.Models.MobileAlertMessages> mobileAlertMessages { get; set; }
        public DbSet<TrackingWebAPI.Models.CustomerWiseReBookDeliveryAddress> customerWiseReBookDeliveryAddress { get; set; }
        public DbSet<TrackingWebAPI.Models.StockPurchaseMaster> stockPurchaseMaster { get; set; }
        public DbSet<TrackingWebAPI.Models.StockOut> stockOut { get; set; }
        public DbSet<TrackingWebAPI.Models.StockPaymentVoucher> stockPaymentVoucher { get; set; }
        public DbSet<TrackingWebAPI.Models.StockIssue> stockIssue { get; set; }
        public DbSet<TrackingWebAPI.Models.StockIssueToEmployeeMaster> StockIssueToEmployee { get; set; }
        public DbSet<TrackingWebAPI.Models.AWBStockRequest> aWBStockRequest { get; set; }
        public DbSet<TrackingWebAPI.Models.StockReturn> stockReturn { get; set; }

        public DbSet<TrackingWebAPI.Models.StockTransfer> stockTransfer { get; set; }

        public DbSet<TrackingWebAPI.Models.CancelAWB> cancelAWB { get; set; }
        public DbSet<TrackingWebAPI.Models.CashBooking> cashBooking { get; set; }
        public DbSet<TrackingWebAPI.Models.CashBookingItemDetails> cashBookingItemDetails { get; set; }
        public DbSet<TrackingWebAPI.Models.BULKVIRTUALBOOKING> bULKVIRTUALBOOKING { get; set; }

        public DbSet<TrackingWebAPI.Models.BookingScanNormal> bookingScanNormal { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingSelf> bookingSelf { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingSelfItemDetails> bookingSelfItemDetails { get; set; }
        public DbSet<TrackingWebAPI.Models.ReturnBooking> returnBooking { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingScanNormalEdit> bookingScanNormalEdit { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingScanLogisticEdit> bookingScanLogisticEdit { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingScanLogisticItemDetails> bookingScanLogisticItemDetails { get; set; }
        public DbSet<TrackingWebAPI.Models.EDPEditing> eDPEditing { get; set; }
        public DbSet<TrackingWebAPI.Models.BookingMPPS> bookingMPPS { get; set; }
        public DbSet<TrackingWebAPI.Models.DataImportWithoutAWB> dataImportWithoutAWB { get; set; }
        public DbSet<TrackingWebAPI.Models.CustomerDataUpdateAWB> customerDataUpdateAWB { get; set; }
        public DbSet<TrackingWebAPI.Models.UrgentDeliveryAlertEntry> urgentDeliveryAlertEntry { get; set; }
        public DbSet<TrackingWebAPI.Models.AWBPrint> aWBPrint { get; set; }
        public DbSet<TrackingWebAPI.Models.DRS> dRS { get; set; }

        public DbSet<TrackingWebAPI.Models.NonDelivery> nonDelivery { get; set; }
        public DbSet<TrackingWebAPI.Models.ReturnToCustomerManifesting> returnToCustomerManifesting { get; set; }
        public DbSet<TrackingWebAPI.Models.FirstMileRTO> firstMileRTO { get; set; }
        //Added by Arun for Master Menu on 21-03-2026
    }
}
