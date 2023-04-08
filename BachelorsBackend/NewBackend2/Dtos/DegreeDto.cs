using NewBackend2.Enums;

namespace NewBackend2.Dtos
{
    public class DegreeDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public StudyField StudyField { get; set; }
        public StudyProgram StudyProgram { get; set; }

    }
}
