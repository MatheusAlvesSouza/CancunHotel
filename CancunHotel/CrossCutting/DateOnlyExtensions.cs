namespace CancunHotel.Api.CrossCutting
{
    public static class DateOnlyExtensions
    {
        public static DateOnly Now(int addDays = default) => DateOnly.FromDateTime(DateTime.Now).AddDays(addDays);
        public static int DayNumberBetween(this DateOnly date1, DateOnly date2) => date1.DayNumber - date2.DayNumber;
        public static string ToDatesString(this IEnumerable<DateOnly> dates) => String.Join(", ", dates);
        public static bool IsAfterToday(this DateOnly date)
        {
            if (date > Now())
                return true;

            return false;
        }
        public static List<DateOnly> ListTo(this DateOnly start, DateOnly end)
        {
            var list = new List<DateOnly>();

            for(int i = start.DayNumber; i < end.DayNumber; i++)
            {
                list.Add(DateOnly.FromDayNumber(i));
            }

            return list;
        }
    }
}
