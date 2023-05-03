namespace NewBackend2.Dtos
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public int CookieId { get; set; }
        public string HospitalName { get; set; }    
        public string Location { get; set; }    
        public DateTime AppointmentDate { get; set; }
        public int Price { get; set; }
        public bool IsReviewed { get; set; }
    }
}
