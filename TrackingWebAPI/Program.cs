
using DALCLASS.DBContact;
using DALCLASS.InterfaceModal;
using DALCLASS.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;
using CountryServices = TrackingWebAPI.Services.CountryServices;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/info.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("Logs/error.txt", restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICountry, CountryServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStateMaster, StateServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IZoneMaster, ZoneServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICityMaster, CityServices>();

//Code added by Arun on 21-03-2026 for Master Menu DI
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IOfficeRequestMaster, OfficeRequestMasterServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IAirlineMaster, AirlineMasterServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ITransportMaster, TransportMasterService>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IVendorMaster, VendorMasterServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IVehicleMasters, VehicleMastersServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IDeliveryBoyMasters, DeliveryBoyMastersServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICustomerWiseReBookDeliveryAddress, CustomerWiseReBookDeliveryAddressServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ISTOCKPURCHASE, StockPurchaseServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockOut, StockOutServices>();

builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockPaymentVoucher, StockPaymentVoucherServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockIssue, StockIssueServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IAWBStockRequest, AWBStockRequestServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockReturn, StockReturnServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockTransfer, StockTransferServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICancelAWB, CancelAWBServices>();

builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICashBooking, CashBookingServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICashBookingItemDetails, CashBookingItemDetailsServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IBookingScanNormal, BookingScanNormalServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IBookingSelf, BookingSelfServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IBookingSelfItemDetails, BookingSelfItemDetailsServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IReturnBooking, ReturnBookingServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IEDPEditing, EDPEditingServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IBookingMPPS, BookingMPPSServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IDataImportWithoutAWB, DataImportWithoutAWBServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.ICustomerDataUpdateAWB, CustomerDataUpdateAWBServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IUrgentDeliveryAlertEntry, UrgentDeliveryAlertEntryServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IAWBPrint, AWBPrintServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IDRS, DRSServices>();
builder.Services.AddScoped<TrackingWebAPI.Interfaces.IStockIssueToEmployee, StockIssueToEmployeeServices>();
//Code added by Arun on 21-03-2026 for Master Menu DI   
builder.Services.AddScoped<ICompanyMaster, CompanyServices>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conf")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();


app.UseCors("AllowFrontend");
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseRouting();

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();


