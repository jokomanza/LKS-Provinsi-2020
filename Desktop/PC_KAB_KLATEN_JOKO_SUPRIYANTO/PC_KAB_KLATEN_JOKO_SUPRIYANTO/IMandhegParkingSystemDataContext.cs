using System.Data.Linq;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    public interface IMandhegParkingSystemDataContext
    {
        Table<Employee> Employees { get; }
        Table<HourlyRate> HourlyRates { get; }
        Table<Member> Members { get; }
        Table<Membership> Memberships { get; }
        Table<ParkingData> ParkingDatas { get; }
        Table<Vehicle> Vehicles { get; }
        Table<VehicleType> VehicleTypes { get; }
    }
}