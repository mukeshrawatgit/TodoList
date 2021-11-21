<script>
    var app = angular.module('myApp', ['ui.grid', 'ui.grid.edit', 'ui.grid.cellNav']);
    app.controller('myController', function ($scope, $http) {

        $scope.gridOptions = {};

    // REQUEST JSON DATA FROM FILE.
    var request = {
        method: 'get',
    url: 'sample.json',
    dataType: 'json',
    contentType: "application/json"
         };

    $http(request)
    .success(function (jsonData) {
        $scope.gridOptions.data = jsonData;     // BIND JSON TO GRID.
            })
    .error(function () { });
     });
</script>
</html >