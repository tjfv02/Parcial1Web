using System;
using System.Collections.Generic;

namespace Parcial1_1201619.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int PersonalInformationPatientId { get; set; }

    public int PersonalInformationDoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public TimeSpan AppointmentTime { get; set; }

    public virtual PersonalInformation PersonalInformationDoctor { get; set; } = null!;

    public virtual PersonalInformation PersonalInformationPatient { get; set; } = null!;
}
