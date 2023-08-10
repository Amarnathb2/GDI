using CMS.Core;
using CMS.CustomTables;
using CMS.CustomTables.Types.GDI;
using GDI.Business.Models;
namespace GDI.Business.Repositories
{
    public class ContactFormRepository: IContactFormRepository
    {
        private readonly IEventLogService _eventLogService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventLogService"></param>
        public ContactFormRepository(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        public  IEnumerable<ContactFormStatesData> GetStatesData()
        {
            var messages = new List<ContactFormStatesData>();
            try
            {
                return GetStatesDetails();
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(ContactFormRepository), nameof(GetStatesData), ex);
                return messages;
            }
        }

             public  IEnumerable<ContactFormStatesData> GetStatesDetails()
        {
            return CustomTableItemProvider.GetItems<StatesItem>().OrderBy("ItemOrder").Select(p => new ContactFormStatesData()
            {
                Options = p.State,
                Values = p.Abbreviation,
               
            }).ToList();
        }

        public IEnumerable<ContactFormCountriesData> GetCountriesData()
        {

            var countries = new List<ContactFormCountriesData>();
            try
            {
                return GetCountriesDetails();
            }

            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(ContactFormRepository), nameof(GetCountriesData), ex);
                return countries;
            }

        }

        public IEnumerable<ContactFormCountriesData> GetCountriesDetails()
        {
            return CustomTableItemProvider.GetItems<CountriesItem>().OrderBy("ItemOrder").Select(p => new ContactFormCountriesData()
            {
                Options = p.Country,
                Values = p.Abbreviation,
                
            }).ToList();
        }
    }
}
