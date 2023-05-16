using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Activities;
using Application;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<IActionResult> GetActivities() 
        {
        return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")] //api/activities/023d0c8a-37e0-42d1-a3df-f8c93173f218
        public async Task<IActionResult> GetActivity(Guid id) 
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult>  CreateActivity(Activity activity) 
        {
            return HandleResult(await Mediator.Send(new Create.Command {Activity = activity}));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")] 
        public async Task<IActionResult> EditActivity(Guid id, Activity activity) 
        {
            activity.Id = id;
            return HandleResult( await Mediator.Send(new Edit.Command {Activity = activity}));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id) 
        {
            return HandleResult(await Mediator.Send(new Delete.Command {Id = id}));

        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command {Id = id}));
        }
    }
}