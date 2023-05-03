export class AppointmentDto {
    appointmentId!: number;
    doctorFirstName!: string;
    doctorLastName!: string;
    cookieId!: number;
    hospitalName!: string;
    location!: string;
    appointmentDate!: Date;
    price!: number;
    isReviewed!: boolean;
}