(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ngMaterial',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/tasks');
            $stateProvider
                 .state('tasklist', {
                     url: '/tasks',
                     templateUrl: '/App/Main/views/task/list.cshtml',
                     menu: 'TaskList' //Matches to name of 'TaskList' menu in SimpleTaskSystemNavigationProvider
                 })
                .state('newtask', {
                    url: '/tasks/new',
                    templateUrl: '/App/Main/views/task/new.cshtml',
                    menu: 'TaskList' //Matches to name of 'NewTask' menu in SimpleTaskSystemNavigationProvider
                })
                .state('questions', {
                    url: '/questions',
                    templateUrl: abp.appPath + 'App/Main/views/questions/index.cshtml',
                    menu: 'Questions' //Matches to name of 'Questions' menu in LMPNavigationProvider
                })
                .state('questionDetail', {
                    url: '/questions/:id',
                    templateUrl: abp.appPath + 'App/Main/views/questions/detail.cshtml',
                    menu: 'Questions' //Matches to name of 'Questions' menu in LMPNavigationProvider
                })
                .state('users', {
                    url: '/users',
                    templateUrl: abp.appPath + 'App/Main/views/users/index.cshtml',
                    menu: 'Users' //Matches to name of 'Users' menu in LMPNavigationProvider
                });
        }
    ]);
})();