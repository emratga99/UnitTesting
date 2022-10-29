using mvc1.Models;

namespace mvc1.Services
{
    public interface IPersonService
    {
        List<PersonModel> GetAll();
        public PersonModel? GetOne(int index);
        public PersonModel? Create(PersonModel person);
        public PersonModel? Update(int index, PersonModel person);
        public PersonModel? Delete(int index);
    }
}