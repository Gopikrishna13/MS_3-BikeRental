using System;

namespace BikeRentalManagement.IRepository;

public interface IReportRepository
{
 Task<int>TotalUsers();

 Task <int>TotalBikes();
Task<int> TotalBooked();

 Task <int>Revenue();

 Task<object>GetRevenueByMonth();
}
