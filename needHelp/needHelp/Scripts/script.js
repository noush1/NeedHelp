// Code goes here

// script.js

     // create the module and name it scotchApp
        // also include ngRoute for all our routing needs
var scotchApp = angular.module('scotchApp', ['ngRoute', 'ngResource']);

    // configure our routes
    scotchApp.config(function($routeProvider) {
        $routeProvider

            // route for the home page
            .when('/', {
                templateUrl : 'home.html',
                controller  : 'mainController'
            })
            
            // route for the activities page
            .when('/ActivityModels', {
                templateUrl : 'activities.html',
                controller  : 'activitiesController'
            })
            
            // route for the organizations page
            .when('/organizations', {
                templateUrl : 'organizations.html',
                controller  : 'organizationsController'
            })

            // route for the about page
            .when('/about', {
                templateUrl : 'about.html',
                controller  : 'aboutController'
            })

            // route for the contact page
            .when('/contact', {
                templateUrl : 'contact.html',
                controller  : 'contactController'
            })
            
            // route for the contact page
            .when('/activity_page', {
                templateUrl : 'activity_page.html',
                controller  : 'activityController'
            });
            
    });

    // create the controller and inject Angular's $scope
    scotchApp.controller('mainController', function($scope) {
        // create a message to display in our view
        $scope.message = 'Some more main page bullshit!';
    });
    
    scotchApp.controller('activitiesController', function ($scope, userService, $resource, $http) {
        // create a message to display in our view
         $scope.message = 'Search activities page, Some suggestions.';

         $scope.getActivities = function () {
             $http({
                 url: '/api/APITest',
                 method: "GET"
             })
                  .then(function (result) {
                      $scope.activityList = angular.copy(result.data);
                  })
         }
         getActivities();
    });
    
     scotchApp.controller('organizationsController', function($scope) {
        // create a message to display in our view
        $scope.message = 'Organizations that work with us';
    });
    
     scotchApp.controller('activityController', function ($scope) {
        // create a message to display in our view
        $scope.message = 'This is just one of our many activities';
        $scope.nuph = 'Nuphar';


        
    });

    scotchApp.controller('aboutController', function($scope) {
        $scope.message = 'Look! I am an about page.';
    });

    scotchApp.controller('contactController', function($scope) {
        $scope.message = 'Contact us! JK. This is just a demo.';
    });