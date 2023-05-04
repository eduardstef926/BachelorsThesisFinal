using NewBackend2.Model;

namespace NewBackend2.Dtos
{
    public class PaginatedSymptomDto
    {
        public List<string> Symptoms { get; set; }
        public int Number { get; set; } 
    }
}
