using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IDegreeRepository
    {
        Task<List<DegreeEntity>> GetDegreeByFirstNameAndLastNameAsync(string firstName, string lastName);
    }
}
