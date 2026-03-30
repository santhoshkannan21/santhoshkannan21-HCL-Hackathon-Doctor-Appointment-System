using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DoctorAppointmentSystem.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole 
    { 
        Patient, 
        Admin, 
        Doctor 
    }
}
