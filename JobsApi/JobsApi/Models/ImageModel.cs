namespace JobsApi.Models;

public class ImageModel
{
    public ImageModel(string id, string mime, byte[] data)
    {
        Id = id;
        Mime = mime;
        Data = data;
    }

    public string Id { get; set; }
    public string Mime { get; set; }
    public byte[] Data { get; set; }
}