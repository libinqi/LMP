﻿(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.task.list';
    app.controller(controllerId, [
        '$scope', '$location', 'abp.services.tasksystem.task',
        function ($scope,$location, taskService) {
            var vm = this;

            vm.localize = abp.localization.getSource('TaskSystem');

            vm.tasks = [];

            $scope.selectedTaskState = 0;

            $scope.$watch('selectedTaskState', function (value) {
                vm.refreshTasks();
            });

            vm.refreshTasks = function () {
                abp.ui.setBusy( //Set whole page busy until getTasks complete
                    null,
                    taskService.getTasks({ //Call application service method directly from javascript
                        state: $scope.selectedTaskState > 0 ? $scope.selectedTaskState : null
                    }).success(function (data) {
                        vm.tasks = data.tasks;
                    })
                );
            };

            vm.changeTaskState = function (task) {
                var newState;
                if (task.state == 1) {
                    newState = 2; //Completed
                } else {
                    newState = 1; //Active
                }

                taskService.updateTask({
                    id: task.id,
                    state: newState
                }).success(function () {
                    task.state = newState;
                    abp.notify.info(vm.localize('TaskUpdatedMessage'));
                });
            };

            vm.getTaskCountText = function () {
                return abp.utils.formatString(vm.localize('Xtasks'), vm.tasks.length);
            };

            vm.newTask = function () {
                $location.path('/tasks/new');
            };
        }
    ]);
})();