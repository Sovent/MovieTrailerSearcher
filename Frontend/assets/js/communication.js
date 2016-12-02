const backendAddress = "http://localhost:55364";

$(document).ready(function(){
	goToSearch();
	$("#search-input").keypress(function() {
		$.getJSON(`${backendAddress}/movies/search?movieTitle=${$(this).context.value}`, {}, function(response) {
			$("#search-result").html(renderSearchList(response));
			$(".searchitem").click(function() {
				$.getJSON(`${backendAddress}/movies/${$(this).context.id}`, {}, function(response) {
					goToMovie(response);
				})
			});		
		})
	})
	$("#goToSearch").click(goToSearch)
})
let goToSearch = function() {
	$("#movie-screen").hide();
	$("#search-screen").show();
}

let goToMovie = function(movie) {
	$("#movie-title").html(`${movie.MovieInfo.Title} (${movie.MovieInfo.Year === null ? "Year unknown" : movie.MovieInfo.Year})`);
	$("#movie-description").html(movie.MovieInfo.Description);
	$("#trailers").html(renderVideoPreviews(movie));
	$("#search-screen").hide();
	$("#movie-screen").show();	
}

let renderSearchItem = function(movie) {
	return `	
		<div id="${movie.MovieId}" class="searchitem row">
			<div class="col-md-2">${movie.MovieId}</div>
			<div class="col-md-8">${movie.Title}</div>
			<div class="col-md-2">${movie.Year === null ? "Unknown" : movie.Year}</div>
		</div>
	`
}
let renderSearchList = function(moviesList) {
	return `
		<div class="table-head row">
			<div class="col-md-2">MovieId</div>
			<div class="col-md-8">Title</div>
			<div class="col-md-2">Year</div>
		</div>
	<div id="search-result" class="container">
		${moviesList.map(renderSearchItem).reduce((first, second) => first + second, "")}
	</div>
	`
}

let renderVideoPreview = function(video) {
	return `
	<div class="col-lg-6 col-md-6 col-sm-6">
		<div id="${video.Id}" class="thumbnail">
			<img src="${video.PreviewImage}">
			<div class="caption">
				<h3>${video.Title}</h3>
			</div>
		</div>
	</div>
	`
}

let renderVideoPreviews = function(movie) {
	return movie.YoutubeVideos.map(renderVideoPreview).reduce((first, second) => first+second, "");
}