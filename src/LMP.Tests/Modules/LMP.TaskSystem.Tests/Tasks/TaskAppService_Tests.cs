using System.Linq;
using Abp.Runtime.Validation;
using Shouldly;
using LMP.TaskSystem.Domain;
using LMP.TaskSystem.Services;
using LMP.TaskSystem.Services.Dtos;
using Xunit;
using LMP.Core.Security.Users;

namespace LMP.TaskSystem.Tests
{
    public class TaskAppService_Tests : TaskSystemTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            //Creating the class which is tested (SUT - Software Under Test)
            _taskAppService = LocalIocManager.Resolve<ITaskAppService>();
        }

        [Fact]
        public void Should_Get_Tasks()
        {
            //Run SUT
            var output = _taskAppService.GetTasks(new GetTasksInput { State = TaskState.Active });

            //Checking results
            output.Tasks.Count.ShouldBe(3);
            output.Tasks.All(t => t.State == (byte)TaskState.Active).ShouldBe(true);
        }

        [Fact]
        public void Should_Create_New_Tasks()
        {
            //Prepare for test
            var initialTaskCount = UsingDbContext(context => context.Tasks.Count());
            var thomasMore = GetUser("libinqi");

            //Run SUT
            _taskAppService.CreateTask(
                new CreateTaskInput
                {
                    Description = "my test task 1"
                });
            _taskAppService.CreateTask(
                new CreateTaskInput
                {
                    Description = "my test task 2",
                    AssignedUserId = thomasMore.Id
                });

            //Check results
            UsingDbContext(context =>
            {
                context.Tasks.Count().ShouldBe(initialTaskCount + 2);
                context.Tasks.FirstOrDefault(t => t.AssignedUserId == null && t.Description == "my test task 1").ShouldNotBe(null);
                var task2 = context.Tasks.FirstOrDefault(t => t.Description == "my test task 2");
                task2.ShouldNotBe(null);
                task2.AssignedUserId.ShouldBe(thomasMore.Id);
            });
        }

        [Fact]
        public void Should_Not_Create_Task_Without_Description()
        {
            //Description is not set
            Assert.Throws<AbpValidationException>(() =>_taskAppService.CreateTask(new CreateTaskInput()));
        }

        //Trying to assign a task of Isaac Asimov to Thomas More
        [Fact]
        public void Should_Change_Assigned_People()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var hezhiwei = GetUser("hezhiwei");
            var dengweifan = GetUser("dengweifan");
            var targetTask = taskRepository.FirstOrDefault(t => t.CreatorUserId == hezhiwei.Id);
            targetTask.ShouldNotBe(null);

            //Run SUT
            _taskAppService.UpdateTask(
                new UpdateTaskInput
                {
                    TaskId = targetTask.Id,
                    AssignedUserId = dengweifan.Id
                });

            //Check result
            taskRepository.Get(targetTask.Id).AssignedUserId.ShouldBe(dengweifan.Id);
        }

        private User GetUser(string name)
        {
            return UsingDbContext(context => context.Users.Single(p => p.Name == name));
        }
    }
}
