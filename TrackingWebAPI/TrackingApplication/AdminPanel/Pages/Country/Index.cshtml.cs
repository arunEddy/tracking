using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.Pages.Country
{
    public class IndexModel : PageModel
    {
        public List<CountryViewModel> CountryList = new List<CountryViewModel>();
        public void OnGet()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        public class CountryViewModel
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
            public string CountryType { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
