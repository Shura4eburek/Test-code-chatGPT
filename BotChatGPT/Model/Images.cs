using System;
namespace BotChatGPT.Model;

//{
//    "created": 1680544112,
//  "data": [
//    {
//        "url": "https://oaidalleapiprodscus.blob.core.windows.net/private/org-aoP9JT4fxFu54WE4N6o2R59y/user-mymILDsOr4J9iVQJL5nFkVuB/img-U1iB8hSTF1ZwlAci1rF76Hup.png?st=2023-04-03T16%3A48%3A32Z&se=2023-04-03T18%3A48%3A32Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-04-03T16%3A10%3A16Z&ske=2023-04-04T16%3A10%3A16Z&sks=b&skv=2021-08-06&sig=zsNwcy04H5Bz%2B67Y%2Bd5CcJfErjbIKFTTLWYHcRdkSXY%3D"
//    }
//  ]
//}

public class Images
{
    public long Created { get; set; }
    public ImageUrl[]? Data { get; set; }
}

public class ImageUrl
{
    public string? Url { get; set; }
}