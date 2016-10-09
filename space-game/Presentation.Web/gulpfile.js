/// <binding BeforeBuild='copy-assets' Clean='clean' />

var gulp = require('gulp');
var del = require('del');
var _ = require('lodash');

var paths = {
    scripts: ['scripts/**/*.js', 'scripts/**/*.ts', 'scripts/**/*.map']
};

gulp.task('clean', ["clean:js", "clean:css"]);

gulp.task("clean:js", function (cb) {
    return del(['scripts/**/*']);
});

gulp.task("clean:css", function (cb) {
    return del(['css/**/*']);
});

gulp.task('default', function () {
    //gulp.src(paths.scripts).pipe(gulp.dest('scripts'));
});

gulp.task('copy-assets', function() {
    var assets = {
        scripts: [
            './node_modules/jquery/dist/jquery.min.js',
            './external/scripts/babylon-2.5-preview.max.js',
            './external/scripts/hand.minified-1.2.js'
        ],
        css: []
    };
    _(assets).forEach(function(assets, type) {
        gulp.src(assets).pipe(gulp.dest('./' + type));
    });
});