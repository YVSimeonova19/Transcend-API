namespace Transcend.Common.Models.Order;

public class OrderVM
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime ExpectedDeliveryDate { get; set; }

    public string UserPlaceId { get; set; } = string.Empty;

    public string CarrierId {  get; set; } = string.Empty;
}
