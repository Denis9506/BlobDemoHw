using BlobDemo;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();

string connectionString = config.GetConnectionString("Default") ?? throw new NullReferenceException("Connection string not found");

var blobService = new BlobService(connectionString);

var container = await blobService.GetContainer("temp");
//string path = "C:\\Users\\den95\\OneDrive\\Рабочий стол\\скрины";
//await blobService.AddBlob(container, Path.Combine(path, "1.png"));

//await blobService.AddBlob(container, Path.Combine(path, "bear.jpg"));
//await blobService.SetBlobAccessTier(container, "bear.jpg", AccessTier.Cold);
/*await blobService.SetBlobMetadata(container, "bear.jpg", new Dictionary<string, string>()
    { 
        { "Name", "Bear" }, 
        { "Location", "Forest" }, 
    }
//);*/
//await blobService.DeleteBlob(container, "bear.jpg");

//var sas = await blobService.GetSAS(container,"fox.jpg");
//Console.WriteLine(sas);
//await blobService.DownloadBlob(container, "fox.jpg");
//for (int i = 0; i < 5; i++) {
//    File.Copy(Path.Combine(path,"bear.jpg"), Path.Combine(path, $"bear{i}.jpg"));
//    await blobService.AddBlob(container, Path.Combine(path,$"bear{i}.jpg"));
//}
//await blobService.DisplayBlobs(container);
//await blobService.DeleteMultipleBlobs(container, Enumerable.Range(0, 5).Select(i => $"bear{i}.jpg"));
//Console.WriteLine(new string('-',40));
//await blobService.DisplayBlobs(container);

var product = new Product
{
    Name = "Laptop",
    Price = 1200.99,
    Count = 50
};

string productBlobName = $"{product.Name}.json";
//await blobService.UploadObjectWithTypeAsync(container, product, productBlobName);
object downloadedObject = await blobService.DownloadObjectWithTypeAsync(container, productBlobName);


Console.WriteLine(downloadedObject is Product downloadedProduct ? downloadedProduct.ToString() : "Unknown object type.");

var user = new User
{
    Name = "Test_u",
    Password = "123"
};
productBlobName = $"{user.Name}.json";
await blobService.UploadObjectWithTypeAsync(container, user, productBlobName);

var downloadedObject2 = await blobService.DownloadObjectWithTypeAsync(container, productBlobName);

Console.WriteLine(downloadedObject2 is User downloadedProduct2 ? downloadedObject2.ToString() : "Unknown object type.");


public class Product
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Count { get; set; }

    public override string ToString()
    {
        return
        $"Product Name: {Name}, Price: {Price}, Count: {Count}";
    }
}

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public override string ToString()
    {
        return
        $"User Name: {Name}, Password: {Password}";
    }
}
