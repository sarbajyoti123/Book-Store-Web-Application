using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testingGithub.Repository;

namespace testingGithub.Components
{
    public class TopBooksViewComponent : ViewComponent
    {

        BookRepo br = new BookRepo();
        public async Task<IViewComponentResult>  InvokeAsync(int count)
        {
            var data = await br.GetTopBooks(count);
            return View(data);
        }

    }
}
