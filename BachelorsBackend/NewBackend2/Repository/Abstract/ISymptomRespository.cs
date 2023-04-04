using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface ISymptomRespository
    {
        Task AddSymptomAsync(SymptomEntity symptom);
    }
}
