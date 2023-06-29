using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IDegreeRepository
    {
        Task<List<DegreeEntity>> GetDegreesByFirstNameAndLastNameAsync(string firstName, string lastName);
    }
}
