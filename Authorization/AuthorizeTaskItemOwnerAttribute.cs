using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PTN_BackendAssignment.Services;
using System.Security.Claims;

namespace PTN_BackendAssignment.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeTaskItemOwnerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var taskItemService = context.HttpContext.RequestServices.GetService<TaskItemService>();

            var userId = GetUserIdFromContext(context);
            var taskId = GetTaskIdFromRoute(context);

            if (userId.HasValue && taskId.HasValue) 
            {
                if (!IsTaskItemOwner(taskId.Value, userId, taskItemService))
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }

        private int? GetUserIdFromContext(AuthorizationFilterContext context)
        {
            var userIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (userIdentity != null)
            {
                var userIdClaim = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }
            }
            return null;
        }

        private int? GetTaskIdFromRoute(AuthorizationFilterContext context)
        {
            if (context.RouteData.Values.TryGetValue("taskId", out var taskIdValue) && int.TryParse(taskIdValue?.ToString(), out var taskId))
            {
                return taskId;
            }

            return null;
        }

        private bool IsTaskItemOwner(int taskId, int? userId, TaskItemService taskItemService)
        {
            // Implement your logic to check if the user is the owner of the task item
            if (userId.HasValue)
            {
                var taskItem = taskItemService.GetTaskItemById(taskId).Result;
                return taskItem != null && taskItem.Owner.Id == userId;
            }

            return false;
        }
    }
}
