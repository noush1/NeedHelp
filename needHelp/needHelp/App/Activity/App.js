
var customerModule = angular.module('activity', ['common'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/activity', { templateUrl: '/App/Activity/Views/CustomerHomeView.html', controller: 'activityHomeViewModel' });
        $routeProvider.when('/activity/list', { templateUrl: '/App/Activity/Views/ActivityListView.html', controller: 'activityListViewModel' });
        $routeProvider.when('/activity/show/:activityId', { templateUrl: '/App/Activity/Views/ActivityView.html', controller: 'activityViewModel' });
        $routeProvider.otherwise({ redirectTo: '/activity' });
        $locationProvider.html5Mode(true);
    });

customerModule.factory('customerService', function ($rootScope, $http, $q, $location, viewModelHelper) { return MyApp.customerService($rootScope, $http, $q, $location, viewModelHelper); });

(function (myApp) {
    var activityService = function ($rootScope, $http, $q, $location, viewModelHelper) {

        var self = this;

        self.activityId = 0;

        return this;
    };
    myApp.activityService = activityService;
}(window.MyApp));
