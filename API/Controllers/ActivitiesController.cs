using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Activities;
using Application;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities() 
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] //api/activities/023d0c8a-37e0-42d1-a3df-f8c93173f218
        public async Task<ActionResult<Activity>> GetActivity(Guid id) 
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult>  CreateActivity(Activity activity) 
        {
            return Ok(await Mediator.Send(new Create.Command {Activity = activity}));
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> EditActivity(Guid id, Activity activity) 
        {
            activity.Id = id;
            return Ok( await Mediator.Send(new Edit.Command {Activity = activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id) 
        {
            return Ok(await Mediator.Send(new Delete.Command {Id = id}));

        }
    }
}