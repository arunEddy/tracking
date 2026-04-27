namespace TrackingWebAPI.DTO
{
    public class StateDTO
    {
        public int StateId { get; set; }

        public string CountryType { get; set; }

        public string CountryName { get; set; }

        public bool isInternational { get; set; }
        public string StateType { get; set; }

        public string StateCode { get; set; }

        public string StateName { get; set; }

        public string GSTIN { get; set; }

        public string EWayBillGSTNumber { get; set; }

        public decimal? EWayBillIntraStateLimit { get; set; }
        public bool IsActive { get; internal set; }
        public int CountryId { get; internal set; }
    }
}
