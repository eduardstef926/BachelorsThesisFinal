namespace NewBackend2.Dtos
{
    public class AppointmentDto
    {
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string UserEmail { get; set; }
        public string Location { get; set; }    
        public DateTime AppointmentDate { get; set; }
    }
}
