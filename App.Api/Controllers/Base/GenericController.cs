using App.Data.Repositories.Base;
using App.Services.Models;

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

    [NonAction]
    public async virtual Task<ApiResultPagerModel<Z>> OnGetDataList(ApiListPostModel postModel)
    {
        return await Repo.GetPagedListDto<Z>(postModel);
    }

    [HttpGet]
    public async virtual Task<ActionResult<ApiResultPagerModel<Z>>> GetList(ApiListPostModel postModel)
    {
        var result = new ApiResultModel();
        try
        {
            var list = await OnGetDataList(postModel);
            if (list == null)
            {
                result.Result = 1;
                return Ok(result);
            }
            return Ok(list);
        }
        catch (Exception ex)
        {
            result.Result = -1;
            result.Err = ex.Message;
        }
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var item = await Repo.GetItemAsync(id);
        if (item == null)
            return NotFound(new ApiGenericResultDto(null, 404, $"Item with ID {id} not found"));

        var success = await Repo.DeleteAsync(id);
        if (!success)
            return StatusCode(500, new ApiGenericResultDto(null, 500, "An error occurred while deleting the item"));

        return NoContent();
    }

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
