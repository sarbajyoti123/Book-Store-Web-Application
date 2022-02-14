using System.Threading.Tasks;
using testingGithub.Data;
using testingGithub.Models;

namespace testingGithub.Repository
{
    public class ContactRepo
    {

        BookStoreContext db = new BookStoreContext();

        public async Task<int> ContactFormUser(ContactFormModel model)
        {
            ContactPage cp = new ContactPage();
            cp.Name = model.Name;
            cp.Email = model.Email;
            cp.Subject = model.Subject;
            cp.Message = model.Message;



            await db.ContactPage.AddAsync(cp);
            await db.SaveChangesAsync();

            return cp.Id;
        }


    }
}
