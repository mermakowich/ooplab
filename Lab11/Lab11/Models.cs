namespace Lab11
{
    // Ответ сервера на POST /auth
    public class AuthResponse
    {
        public string token { get; set; }
    }

    // Элемент списка GET /booking — содержит только id
    public class BookingId
    {
        public int bookingid { get; set; }
    }

    // Даты заезда/выезда — вложенный объект
    public class BookingDates
    {
        public string checkin  { get; set; }
        public string checkout { get; set; }
    }

    // Полная запись бронирования (GET /booking/{id})
    public class Booking
    {
        public string       firstname       { get; set; }
        public string       lastname        { get; set; }
        public int          totalprice      { get; set; }
        public bool         depositpaid     { get; set; }
        public BookingDates bookingdates    { get; set; }
        public string       additionalneeds { get; set; }
    }
}
