namespace NewBackend2.Dtos
{
    public class AppoimentSlotDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }   
        public DateTime Date { get; set; }   
        public string HospitalName { get; set; }   
        public int ConsultPrice { get; set; }  
        public float Rating { get; set; }
    }
}
