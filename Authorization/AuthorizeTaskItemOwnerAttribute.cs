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
                if (!IsTaskItemOwner(taskId.Value, userId.Value, taskItemService))
                {
                    // If the user is not the owner, forbid access
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                // If user ID or task ID is not available, forbid access
                context.Result = new ForbidResult();
            }
        }

        // Get user ID from the ClaimsPrincipal in the HttpContext
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

        // Get task ID from the route parameters
        private int? GetTaskIdFromRoute(AuthorizationFilterContext context)
        {
            return context.RouteData.Values.TryGetValue("taskId", out var taskIdValue)
                ? int.TryParse(taskIdValue?.ToString(), out var taskId) ? taskId : null
                : null;
        }

        // Check if the user is the owner of the task item
        private bool IsTaskItemOwner(int taskId, int userId, TaskItemService taskItemService)
        {
            // Implement your logic to check if the user is the owner of the task item
            var taskItem = taskItemService.GetTaskItemById(taskId).Result;
            return taskItem != null && taskItem.Owner.Id == userId;
        }
    }
}
