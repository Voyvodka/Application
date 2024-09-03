using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public abstract class GenericController<T, U, Z> : ControllerBase
        where T : class, new()
        where U : class
        where Z : class
{
    protected IGenericRepositoryAsync<T> Repo;
    private readonly IMapper _mapper;

    public GenericController(IMapper mapper, U context)
    {
        _mapper = mapper;
    }

    // Get a single item by ID
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(int id)
    {
        var item = await Repo.GetItemAsync(id);
        if (item == null)
            return NotFound(new ApiGenericResultDto(null, 404, $"Item with ID {id} not found."));

        var itemDto = _mapper.Map<Z>(item);
        return Ok(new ApiGenericResultDto(itemDto, 200, null));
    }

    [NonAction]
    public virtual void OnCreateSaveItem(T item)
    {
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] Z itemDto)
    {
        if (itemDto == null)
            return BadRequest(new ApiGenericResultDto(null, 400, "Invalid item"));

        var item = _mapper.Map<T>(itemDto);
        OnCreateSaveItem(item);
        var success = await Repo.CreateAsync(item);

        if (!success)
            return StatusCode(500, new ApiGenericResultDto(null, 500, "An error occurred while creating the item"));

        var itemId = GetItemId(item);
        return CreatedAtAction(nameof(Get), new { id = itemId }, new ApiGenericResultDto(new { Id = itemId }, 201, null));
    }

    [NonAction]
    public virtual void OnEditSaveItem(T item)
    {
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, [FromBody] Z itemDto)
    {
        if (itemDto == null)
            return BadRequest(new ApiGenericResultDto(null, 400, "Invalid item"));

        var existingItem = await Repo.GetItemAsync(id);
        if (existingItem == null)
            return NotFound(new ApiGenericResultDto(null, 404, $"Item with ID {id} not found"));

        _mapper.Map(itemDto, existingItem);

        OnEditSaveItem(existingItem);

        var success = await Repo.EditAsync(existingItem);
        if (!success)
            return StatusCode(500, new ApiGenericResultDto(null, 500, "An error occurred while updating the item"));

        return Ok(new ApiGenericResultDto(null, 200, null));
    }




    // // Get a list of items
    // [HttpGet]
    // public virtual async Task<IActionResult> GetList([FromQuery] string filter = null)
    // {
    //     Expression<Func<T, bool>> filterExpression = null;
    //     if (!string.IsNullOrEmpty(filter))
    //     {
    //         filterExpression = item => EF.Functions.Like(item.ToString(), $"%{filter}%");
    //     }

    //     var items = Repo.GetList(filterExpression).ToList();
    //     return Ok(new ApiGenericResultDto(items, 200, null));
    // }





    // // Delete an item
    // [HttpDelete("{id}")]
    // public virtual async Task<IActionResult> Delete(int id)
    // {
    //     var item = await Repo.GetItemAsync(id);
    //     if (item == null)
    //         return NotFound(new ApiGenericResultDto(null, 404, $"Item with ID {id} not found"));

    //     var success = await Repo.DeleteAsync(id);
    //     if (!success)
    //         return StatusCode(500, new ApiGenericResultDto(null, 500, "An error occurred while deleting the item"));

    //     return NoContent();
    // }

    // Helper method to get the item's ID (Override this in derived controllers if necessary)
    protected virtual int GetItemId(T item)
    {
        var property = typeof(T).GetProperty("Id");
        if (property != null)
        {
            return (int)property.GetValue(item);
        }
        throw new InvalidOperationException("Item does not have an Id property.");
    }
}
