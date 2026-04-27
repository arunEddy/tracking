using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CashBookingServices:ICashBooking
    {
        private readonly ApplicationDbContext _context;

        public CashBookingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.CashBooking>> GetAllCashBooking()
        {
            return await _context.cashBooking
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.CashBooking> GetCashBookingById(int id)
        {
            return await _context.cashBooking
           .Where(x => x.cbid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.CashBooking> CreateCashBooking(Models.CashBooking CashBooking)
        {
            await _context.cashBooking.AddAsync(CashBooking);
            await _context.SaveChangesAsync();
            return CashBooking;
        }

        public async Task<CashBooking> UpdateCashBooking(int id, Models.CashBooking CashBooking)
        {
            var existingCashBooking = await _context.cashBooking.FindAsync(id);
            if (existingCashBooking != null)
            {
                existingCashBooking.BookingOffice = CashBooking.BookingOffice;
                existingCashBooking.ShipperName = CashBooking.ShipperName;
                existingCashBooking.DocketType = CashBooking.DocketType;
                existingCashBooking.AWB = CashBooking.AWB;
                existingCashBooking.BookingDate = CashBooking.BookingDate;
                existingCashBooking.Origin = CashBooking.Origin;
                existingCashBooking.Destination = CashBooking.Destination;
                existingCashBooking.Mode = CashBooking.Mode;
                existingCashBooking.Product = CashBooking.Product;
                existingCashBooking.Pcs = CashBooking.Pcs;
                existingCashBooking.Volumetric = CashBooking.Volumetric;
                existingCashBooking.ChargeWeight = CashBooking.ChargeWeight;
                existingCashBooking.VolWeight = CashBooking.VolWeight;
                existingCashBooking.CODAmount = CashBooking.CODAmount;
                existingCashBooking.ConsignorMobile = CashBooking.ConsignorMobile;
                existingCashBooking.ConsignorName = CashBooking.ConsignorName;
                existingCashBooking.ConsignorAddress1 = CashBooking.ConsignorAddress1;
                existingCashBooking.ConsignorAddress2 = CashBooking.ConsignorAddress2;
                existingCashBooking.ConsignorCity = CashBooking.ConsignorCity;
                existingCashBooking.ConsigneePincode = CashBooking.ConsigneePincode;
                existingCashBooking.ConsigneeState = CashBooking.ConsigneeState;
                existingCashBooking.ConsigneeODAChargeApplicable = CashBooking.ConsigneeODAChargeApplicable;
                existingCashBooking.ConsigneeGSTNo = CashBooking.ConsigneeGSTNo;
                existingCashBooking.PaymentMode = CashBooking.PaymentMode;
                existingCashBooking.ReceivedAmount = CashBooking.ReceivedAmount;
                existingCashBooking.TariffAmount = CashBooking.TariffAmount;
                existingCashBooking.CGST = CashBooking.CGST;
                existingCashBooking.SGST = CashBooking.SGST;
                existingCashBooking.IGST = CashBooking.IGST;
                existingCashBooking.TotalAmount = CashBooking.TotalAmount;
                existingCashBooking.mdfby = CashBooking.mdfby;
                existingCashBooking.mdfon = CashBooking.mdfon;
                existingCashBooking.IsActive = CashBooking.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingCashBooking;
        }

        public async Task<CashBooking> DeleteCashBooking(int id)
        {
            var CashBooking = await _context.cashBooking.FindAsync(id);
            if (CashBooking != null)
            {
                CashBooking.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return CashBooking;
        }
    }
}
