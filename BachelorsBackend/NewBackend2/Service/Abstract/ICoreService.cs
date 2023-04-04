namespace NewBackend2.Service.Abstract
{
    public interface ICoreService
    {
        public Task<string> GetSymptomDataAsync(string userEmail, List<string> symptomList);
    }
}
