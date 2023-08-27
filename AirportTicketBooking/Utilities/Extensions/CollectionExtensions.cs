namespace AirportTicketBooking.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        public static bool AnySafe<T>(this IEnumerable<T> collection)
        {
            return collection?.Any() ?? false;
        }
    }
}
