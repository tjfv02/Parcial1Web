using System;
using System.Collections.Generic;

namespace Parcial1_1201619.Models;

public partial class PersonalInformation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<Appointment> AppointmentPersonalInformationDoctors { get; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentPersonalInformationPatients { get; } = new List<Appointment>();
}
