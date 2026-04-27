using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class ReturnBookingServices:IReturnBooking
    {
        private readonly ApplicationDbContext _context;

        public ReturnBookingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.ReturnBooking>> GetAllReturnBooking()
        {
            return await _context.returnBooking
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.ReturnBooking> GetReturnBookingById(int id)
        {
            return await _context.returnBooking
           .Where(x => x.rbid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.ReturnBooking> CreateReturnBooking(Models.ReturnBooking customerDataUpdateAWB)
        {
            await _context.returnBooking.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<ReturnBooking> UpdateReturnBooking(int id, Models.ReturnBooking customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.returnBooking.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.AWBReference = customerDataUpdateAWB.AWBReference;
                existingcustomerDataUpdateAWB.AWBReferenceNo = customerDataUpdateAWB.AWBReferenceNo;
                existingcustomerDataUpdateAWB.BookingOffice = customerDataUpdateAWB.BookingOffice;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.ConsignorName = customerDataUpdateAWB.ConsignorName;
                existingcustomerDataUpdateAWB.DepttPersonName = customerDataUpdateAWB.DepttPersonName;
                existingcustomerDataUpdateAWB.BookingDate = customerDataUpdateAWB.BookingDate;
                existingcustomerDataUpdateAWB.ExpDeliveryDate = customerDataUpdateAWB.ExpDeliveryDate;
                existingcustomerDataUpdateAWB.ExpDeliveryTime = customerDataUpdateAWB.ExpDeliveryTime;
                existingcustomerDataUpdateAWB.Pcs = customerDataUpdateAWB.Pcs;
                existingcustomerDataUpdateAWB.ChargeWeight = customerDataUpdateAWB.ChargeWeight;
                existingcustomerDataUpdateAWB.MachineWeight = customerDataUpdateAWB.MachineWeight;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.CODAmount = customerDataUpdateAWB.CODAmount;
                existingcustomerDataUpdateAWB.bsName = customerDataUpdateAWB.bsName;
                existingcustomerDataUpdateAWB.Address1 = customerDataUpdateAWB.Address1;
                existingcustomerDataUpdateAWB.Address2 = customerDataUpdateAWB.Address2;
                existingcustomerDataUpdateAWB.City = customerDataUpdateAWB.City;
                existingcustomerDataUpdateAWB.Pincode = customerDataUpdateAWB.Pincode;
                existingcustomerDataUpdateAWB.bsState = customerDataUpdateAWB.bsState;
                existingcustomerDataUpdateAWB.Phone = customerDataUpdateAWB.Phone;
                existingcustomerDataUpdateAWB.Mobile = customerDataUpdateAWB.Mobile;
                existingcustomerDataUpdateAWB.ODAChargeApplicable = customerDataUpdateAWB.ODAChargeApplicable;
                existingcustomerDataUpdateAWB.ConsigneeName = customerDataUpdateAWB.ConsigneeName;
                existingcustomerDataUpdateAWB.ConsigneeAddress1 = customerDataUpdateAWB.ConsigneeAddress1;
                existingcustomerDataUpdateAWB.ConsigneeAddress2 = customerDataUpdateAWB.ConsigneeAddress2;
                existingcustomerDataUpdateAWB.ReturnPhone = customerDataUpdateAWB.ReturnPhone;
                existingcustomerDataUpdateAWB.ReturnMobile = customerDataUpdateAWB.ReturnMobile;
                existingcustomerDataUpdateAWB.ReturnPincode = customerDataUpdateAWB.ReturnPincode;
                existingcustomerDataUpdateAWB.ReturnCity = customerDataUpdateAWB.ReturnCity;
                existingcustomerDataUpdateAWB.ReturnAWBNo = customerDataUpdateAWB.ReturnAWBNo;
                existingcustomerDataUpdateAWB.ReturnPickupCity = customerDataUpdateAWB.ReturnPickupCity;
                existingcustomerDataUpdateAWB.ReturnDestinationCity = customerDataUpdateAWB.ReturnDestinationCity;
                existingcustomerDataUpdateAWB.ReturnRemarks = customerDataUpdateAWB.ReturnRemarks;
                existingcustomerDataUpdateAWB.ReturnPieces = customerDataUpdateAWB.ReturnPieces;
                existingcustomerDataUpdateAWB.ReturnWeight = customerDataUpdateAWB.ReturnWeight;
                existingcustomerDataUpdateAWB.ReturnDate = customerDataUpdateAWB.ReturnDate;
                existingcustomerDataUpdateAWB.ReturnTime = customerDataUpdateAWB.ReturnTime;
                existingcustomerDataUpdateAWB.ReturnPickupPincode = customerDataUpdateAWB.ReturnPickupPincode;
                existingcustomerDataUpdateAWB.ReturnDestinationPicode = customerDataUpdateAWB.ReturnDestinationPicode;
                existingcustomerDataUpdateAWB.ReturnProductName = customerDataUpdateAWB.ReturnProductName;
                existingcustomerDataUpdateAWB.ReturnMode = customerDataUpdateAWB.ReturnMode;
                existingcustomerDataUpdateAWB.ReturnProductType = customerDataUpdateAWB.ReturnProductType;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<ReturnBooking> DeleteReturnBooking(int id)
        {
            var customerDataUpdateAWB = await _context.returnBooking.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
