/// <reference path="angular.js" />

(function () {
	var app = angular.module("FileBrowser", []);

	var FileBrowserController = function ($scope, $http) {

		/* main handlers */

		var onError = function (reason) {
			//at this place may be even alert - error field is static at top, and page may be long.
			$scope.error = reason.data.Message;
		}

		var onLoadFiles = function (response) {
			$scope.files = response.data;
		}

		var onLoadFilesCounter = function (response) {
			$scope.filesCounter = response.data;
		}

		var onLoadDirectories = function (response) {
			$scope.directories = response.data;
		}


		/* function incapsulates loaders logic within itself */

		//params = { params: { path: path } } in common case, calls Get(string path)
		//and params = null in ROOT case, calls Get()
		var loadDirectoriesFilesAndFilesCounter = function (path, params) {
			$http.get("../api/directories", params)
						 .then(onLoadDirectories, onError);
			$http.get("../api/files", params) 
						 .then(onLoadFiles, onError);
			$http.get("../api/filescounter", params)
						 .then(onLoadFilesCounter, onError);
		}

		/* main loading function */

		//if there is access to directory, invokes loader function 
		//with proper parameter for ROOT or common directory
		$scope.loadDirectoryContent = function (path) {
			$http.get("../api/directoryaccess", { params: { path: path } })
				 .then(function (response) {
				 	var params = (path == "" || path == null) ? null : { params: { path: path } };
				 	loadDirectoriesFilesAndFilesCounter(path, params);

				 	$scope.currentDirectory = path;
				 	$scope.error = "";
				 }, onError);
		}

		/* explicit function call in the start */
		$scope.loadDirectoryContent("");
	}

	app.controller("FileBrowserController", FileBrowserController);
}());