using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DoctorAppointmentSystem.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TimeSlot
    {
        SLOT_09AM_TO_10AM,
        SLOT_10AM_TO_11AM,
        SLOT_11AM_TO_12PM,
        SLOT_02PM_TO_03PM,
        SLOT_03PM_TO_04PM,
        SLOT_04PM_TO_05PM
    }
}
