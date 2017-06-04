﻿angular.
  module('myApp', ['ui.router']).
  config(['$locationProvider', '$stateProvider', '$urlRouterProvider',
    function config($locationProvider, $stateProvider, $urlRouterProvider, $scope, $rootScope) {

        $locationProvider.hashPrefix("");

        $stateProvider.state('main', {
            abstract: true,
            url: '/'
        });
        var userPageState = {
            name: 'User/UserPage',
            url: '/User/UserPage',
            views: {
                'header': {
                    templateUrl: function($scope) {
                        
                        return 'UserPage/UserPageHeader?userId='+window.userId;
                    }
                },             
                'content': {
                    templateUrl: 'UserPage/UserPageContent',
                    controller: 'PostController'
                }
            }
        };

        var userInfo = {
            name: 'User/Info',
            url: '/User/Info',
            views: {
                'header': templateUrl: function($scope) {

                    return 'UserPage/UserPageHeader?userId=' + window.userId;
                },
                'content': { templateUrl: 'UserPage/UserInfo' }
            }
        };

        var userFriends = {
            name: 'User/Friends',
            url: '/User/Friends',
            views: {
                'header': 
                    templateUrl: function($scope) {

                        return 'UserPage/UserPageHeader?userId=' + window.userId;
                    
                },
                'content': { templateUrl: 'UserPage/UserFriends' }
            }
        };

        var userGallery = {
            name: 'User/Gallery',
            url: '/User/Gallery',
            views: {
                'header': 
                    templateUrl: function($scope) {

                        return 'UserPage/UserPageHeader?userId=' + window.userId;
                    },
                'content': { templateUrl: 'UserPage/UserGallery' }
            }
        };

        var userStatistics = {
            name: 'User/Statistics',
            url: '/User/Statistics',
            views: {
                'header': {
                    templateUrl: function($scope) {

                        return 'UserPage/UserPageHeader?userId=' + window.userId;
                    } },
                'content': { templateUrl: 'UserPage/UserStatistics' }
            }
        };

        var addNewGroup = {
            name: 'Groups/AddNewGroup',
            url: '/Groups/AddNewGroup',
            views: {
                'header': { templateUrl: 'Groups/GroupHeader' },
                'content': { templateUrl: 'Groups/AddNewGroup' }
            }
        };

        var groupDetails = {
            name: 'Groups/Details',
            url: '/Groups/Details',
            views: {
                'header': { templateUrl: 'Groups/GroupHeader' },
                'content': { templateUrl: 'Groups/GroupDetails' }
            }
        };

        var pathMachine = function (path, param) {

            var superPath = path + param;
            console.log(superPath);
            return superPath;
        };

        //var groupContent = {
        //    name: 'Groups/Content',
        //    url: '/Groups/Content/:groupId',
        //    controller: function ($scope, $stateParams) {
        //        console.log("RAZ: " + $scope.razrId);
        //        $scope._groupId = $stateParams.groupId;
        //    },
        //    views: {
        //        'header': { templateUrl: 'Groups/GroupHeader' },
        //        'content': { templateUrl: pathMachine("Groups/GroupContent/", /*$stateParams.groupId JAK SIĘ WPISZE Z PALCA TO DZIAŁA, A TAK TO NIE :((*/ 12) }
        //    }
        //};
        var logOff = {
            name: 'User/LogOff',
            url: '/User/LogOff',
            controller: 'LogOffController'
            //views: {
            //    'header': { templateUrl: 'Account/LogOff' },
            //    'content': { templateUrl: 'Account/LogOff' }
            //}
        };


        $stateProvider.state(userPageState);
        $stateProvider.state(userInfo);
        $stateProvider.state(userFriends);
        $stateProvider.state(userGallery);
        $stateProvider.state(userStatistics);
        $stateProvider.state(addNewGroup);
        $stateProvider.state(groupDetails);
        //$stateProvider.state(groupContent);
        $stateProvider.state(logOff);
    }
  ]).controller('LogOffController', ['$scope', function ($scope) {
      $scope.LogOff = function () {
          window.location.replace("/Account/LogOff");
      };

    }]).controller('PostController', ['$scope','$state', function($scope,$state) {
        $scope.PostClicked = function ($event) {
            
            
            window.userId = "17167e46-7333-4edb-8d70-e403a8c18b0a";
            $state.reload();
        }


    }]);