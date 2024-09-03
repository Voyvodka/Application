namespace App.Data.ViewModels.Dto;

public class ClientDtoBase
{
    public string Name { get; set; }
}

public class ClientDto : ClientDtoBase
{
    public int Id { get; set; }
}