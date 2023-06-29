namespace NewBackend2.Dtos
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HospitalName { get; set; }
        public string CurrentPosition { get; set; } 
        public string Specialization { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var dto = (DoctorDto)obj;

            return FirstName == dto.FirstName &&
                   LastName == dto.LastName &&
                   Location == dto.Location &&
                   Specialization == dto.Specialization &&
                   Email == dto.Email &&
                   HospitalName == dto.HospitalName &&
                   Rating == dto.Rating;
        }
    }
}
