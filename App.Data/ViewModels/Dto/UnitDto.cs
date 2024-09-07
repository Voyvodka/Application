namespace App.Data.ViewModels.Dto;

public class UnitDtoBase
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class UnitDto : UnitDtoBase
{
    public int Id { get; set; }
}
