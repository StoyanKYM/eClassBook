using eClassBook.Services.DTOs;
using eClassBook.ViewModels.Home;

namespace eClassBook.Services.HomeServices
{
    // 1. Use ViewModels
    // 2. Create DTO -> View Model
    public interface IGetCountService
    {
        //IndexViewModel GetCount();
        GetCountDTO GetCount();
    }
}
