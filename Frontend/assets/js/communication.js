const backendAddress = "http://85.143.104.47:1877";

$(document).ready(function(){
	goToSearch();
	$("#search-input").on("input", function() {
		if ($(this).context.value === "") return;
		$.getJSON(`${backendAddress}/movies/search?movieTitle=${$(this).context.value}`, {}, function(response) {
			$("#search-result").html(renderSearchList(response));
			$(".searchitem").click(function() {
				$.getJSON(`${backendAddress}/movies/${$(this).context.id}`, {}, function(response) {
					goToMovie(response);
				})
			});		
		})
	});
	$("#goToSearch").click(goToSearch);
})
let goToSearch = function() {
	$("#movie-screen").hide();
	$("#search-screen").show();
}

let goToMovie = function(movie) {
	$("#movie-title").html(`${movie.MovieInfo.Title} (${movie.MovieInfo.Year === null ? "Year unknown" : movie.MovieInfo.Year})`);
	$("#movie-description").html(movie.MovieInfo.Description);
	$("#trailers").html(renderVideoPreviews(movie));	
	$(".thumbnail").click(function() {
		$("#youtube-trailer")[0].src = `https://www.youtube.com/embed/${$(this).context.id}`;
	});
	$(".modal").on("hidden.bs.modal", function(e) {
		$("#youtube-trailer")[0].src = "";
	})
	$("#search-screen").hide();
	$("#movie-screen").show();
}

let renderSearchItem = function(movie) {
	return `	
		<div id="${movie.MovieId}" class="searchitem row">
			<div class="col-md-2 col-sm-1">${movie.MovieId}</div>
			<div class="col-md-8 col-sm-10">${movie.Title}</div>
			<div class="col-md-2 col-sm-1">${movie.Year === null ? "Unknown" : movie.Year}</div>
		</div>
	`
}
let renderSearchList = function(moviesList) {
	return `
	<div id="search-result" class="container">
		<div class="table-head row">
			<div class="col-md-2 col-sm-1">MovieId</div>
			<div class="col-md-8 col-sm-10">Title</div>
			<div class="col-md-2 col-sm-1">Year</div>
		</div>
		${moviesList.map(renderSearchItem).reduce((first, second) => first + second, "")}
	</div>
	`
}

let renderVideoPreview = function(video) {
	return `
	<div class="col-lg-6 col-md-6 col-sm-6">
		<div id="${video.Id}" class="thumbnail" data-toggle="modal" data-target=".trailer-modal">
			<img class="preview-image" src="${video.PreviewImage}">
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