// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Navbar Change on Scroll//
$(function () {
	var navbar = $(".navbar");
	$(window).scroll(function () {
		var scroll = $(window).scrollTop();

		if (scroll >= 50) {
			navbar.addClass("custom-navbar");
		} else {
			navbar.removeClass("custom-navbar");
		}
	});
});