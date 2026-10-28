namespace SmartCollegeMS.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalFaculty { get; set; }
        public int PresentToday { get; set; }
        public int AbsentToday { get; set; }
        public double AttendancePercentageToday { get; set; }
        
        // For the Chart.js bar chart
        public List<string> ChartLabels { get; set; } = new List<string>();
        public List<int> ChartDataPresent { get; set; } = new List<int>();
        public List<int> ChartDataAbsent { get; set; } = new List<int>();
    }
}