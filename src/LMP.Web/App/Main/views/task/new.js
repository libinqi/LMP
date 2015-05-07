(function() {
    var app = angular.module('app');

    var controllerId = 'app.views.task.new';
    app.controller(controllerId, [
        '$scope', '$location', 'abp.services.tasksystem.task', 'abp.services.users.user',
        function($scope, $location, taskService, personService) {
            var vm = this;

            vm.task = {
                description: '',
                assignedUserId: null
            };

            vm.localize = abp.localization.getSource('TaskSystem');

            vm.people = []; //TODO: Move Person combo to a directive?

            personService.getUsers().success(function (data) {
                vm.people = data.items;;
            });

            vm.saveTask = function() {
                abp.ui.setBusy(
                    null,
                    taskService.createTask(
                        vm.task
                    ).success(function() {
                        abp.notify.info(abp.utils.formatString(vm.localize("TaskCreatedMessage"), vm.task.description));
                        $location.path('/tasks/list');
                    })
                );
            };
        }
    ]);
})();