namespace Application.DTOs.RequestFlight
{
    public class RequestFlightDto
    {
        public string? departureStation { get; set; }
        public string? arrivalStation { get; set; }
        public string? flightCarrier { get; set; }
        public string? flightNumber { get; set; }
        public double? price { get; set; }
    }
}
