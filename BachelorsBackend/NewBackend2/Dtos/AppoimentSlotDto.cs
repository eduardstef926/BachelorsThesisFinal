namespace NewBackend2.Dtos
{
    public class AppoimentSlotDto
    {
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string Location { get; set; }
        public string HospitalName { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }   
        public DateTime Date { get; set; }   
        public int Price { get; set; }  
        public float Rating { get; set; }
    }
}
