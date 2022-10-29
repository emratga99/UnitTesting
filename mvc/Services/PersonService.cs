using mvc1.Models;

namespace mvc1.Services
{
    public class PersonService : IPersonService
    {

        private List<PersonModel> _people = new List<PersonModel>{
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
            new PersonModel{
                FirstName = "abc",
                LastName = "abc xyz",
                Gender = "mlafs",
                DateOfBirth = new DateTime(2000,03,20),
                PhoneNumber = "231313",
                BirthPlace = "hanoi",
                IsGraduated = false
            },
        };

        public List<PersonModel> GetAll()
        {
            return _people;
        }

        public PersonModel? GetOne(int index)
        {
            if (_people.Count() > index && index >= 0)
            {
                return _people[index];
            }
            else
            {
                return null;
            }
        }

        public PersonModel? Create(PersonModel person)
        {
            _people.Add(person);
            return person;
        } 
        public PersonModel? Update(int index, PersonModel person)
        {
            if (index > -1 && index < _people.Count())
            {
                _people[index] = person;
                return person;
            }
            else
            {
                return null;
            }
        }
        public PersonModel? Delete(int index)
        {
            if (index > -1 && index < _people.Count())
            {
                var person = _people[index];
                _people.RemoveAt(index);
                return person;
            }
            else
            {
                return null;
            }
        }
    }
}