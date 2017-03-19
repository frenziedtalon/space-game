/// <binding BeforeBuild='sass-compile' ProjectOpened='watch' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp");
var sourcemaps = require("gulp-sourcemaps");

// Files
var del = require("del");
var extReplace = require("gulp-ext-replace");

// Styles
var autoprefixer = require("gulp-autoprefixer");
var minifyCss = require("gulp-clean-css");
var sass = require("gulp-sass");

var paths = {
    styleOut: "./css/",
    mainSassFile: "./sass/index.scss"
}

gulp.task("default", ["sass-compile"], function () { });

gulp.task("watch", ["default"], function () {
    gulp.watch("./sass/**/*.scss", ["sass-compile"]);
});

gulp.task("sass-clean", function () {
    const toDelete = [
        paths.styleOut + "**/*.*"
    ];

    return del(toDelete);
});

gulp.task("sass-compile", ["sass-clean"], function () {
    return gulp.src(paths.mainSassFile)
        .pipe(sourcemaps.init())
        .pipe(sass())
        .on("error", displayErrorAndEnd)
        .pipe(autoprefixer())
        .pipe(sourcemaps.write(""))
        .pipe(gulp.dest(paths.styleOut))
        .pipe(extReplace(".min.css"))
        .pipe(minifyCss())
        .pipe(gulp.dest(paths.styleOut));
});

function displayErrorAndEnd(error) {
    console.log(error.toString());
    this.emit("end");
}
