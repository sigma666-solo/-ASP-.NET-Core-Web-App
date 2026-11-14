using Lesson4.Data;
using Lesson4.Models;

namespace Lesson4.Services;

public class CatService(IRepository _repository)
{
    private const int MinPage = 1;
    private const int CountElementsOnPage = 20;

    public async Task<ResponceIndexModel<Cat>> GetCatsAsync(RequestIndexModel indexModel)
    {
        FormatInputRequest(indexModel);

        //2. Логика постраничного получения данных
        var allCountElementsOnPage = await _repository.GetCountAsync(indexModel.SearchString);

        var responceIndexModel = new ResponceIndexModel<Cat>(
            indexModel.SearchString,
            indexModel.Page,
            allCountElementsOnPage,
            CountElementsOnPage);

        ValidatePageNumber(indexModel, responceIndexModel);

        var countSkip = CountElementsOnPage * (responceIndexModel.CurrentPage - 1);

        List<Cat>? cats;
        if (indexModel != null && !string.IsNullOrEmpty(indexModel.SearchString))
        {
            cats = _repository.Filtered(indexModel.SearchString, countSkip, CountElementsOnPage);
        }
        else
        {
            cats = await _repository.GetAllAsync(countSkip, CountElementsOnPage);
        }

        responceIndexModel.Pets = cats;

        return responceIndexModel;
    }

    private void FormatInputRequest(RequestIndexModel indexModel)
    {
        if (indexModel?.SearchString != null)
        {
            indexModel.SearchString = indexModel.SearchString.Trim();
        }
    }

    private static void ValidatePageNumber(RequestIndexModel indexModel, ResponceIndexModel<Cat> responceIndexModel)
    {
        if (indexModel.Page > responceIndexModel.TotalPages || indexModel.Page < MinPage)
        {
            throw new ArgumentOutOfRangeException("Вышли за диапазон страницы");
        }
    }
}