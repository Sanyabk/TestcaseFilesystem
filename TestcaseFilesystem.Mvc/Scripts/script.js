/// <reference path="angular.js" />

(function () {
	var app = angular.module("FileBrowser", []);

	var FileBrowserController = function ($scope, $http) {

		/* main handlers */

		var onError = function (reason) {
			//may be even alert - error field is static, and page may be long.
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

		/* loaders */

		var loadDirectories = function (path) {
			$http.get("../api/directories", { params: { path: path } })
						 .then(onLoadDirectories, onError);
		}
		var loadFiles = function (path) {
			$http.get("../api/files", { params: { path: path } })
				 .then(onLoadFiles, onError);
		}
		var loadFilesCounter = function (path) {
			$http.get("../api/filescounter", { params: { path: path } })
				 .then(onLoadFilesCounter, onError);
		}

		/* main loading function */

		$scope.loadDirectoryContent = function (path) {
			$http.get("../api/directoryaccess", { params: { path: path } })
				 .then(function (response) {
				 	loadDirectories(path); /*get directories from path*/
				 	loadFiles(path); /*get files from path*/
				 	loadFilesCounter(path); /*get FileCounter for path*/

				 	$scope.currentDirectory = path;
				 	$scope.error = "";
				 }, onError);
		}

		/* explicit function call in the start */
		$scope.loadDirectoryContent("");
	}

	app.controller("FileBrowserController", FileBrowserController);
}());