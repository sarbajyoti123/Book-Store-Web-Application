using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testingGithub.Repository;

namespace testingGithub.Components
{
    public class SimilarBooksViewComponent : ViewComponent
    {
        BookRepo br = new BookRepo();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await br.GetSimilarBooks(id);
            return View(data);
        }

    }
}
