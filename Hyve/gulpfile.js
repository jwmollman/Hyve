var config = require("./gulpfile-config");
var gulp = require("gulp");
var sass = require("gulp-sass");
var concat = require("gulp-concat");
var cleanCss = require("gulp-clean-css");
var uglify = require("gulp-uglify");
var imagemin = require("gulp-imagemin");
var cache = require("gulp-cache");
var browserSync = require("browser-sync").create();

// Compile and concatenate all SCSS files
gulp.task("sass", function () {
    return gulp.src(config.srcPath + "/scss/**/*.scss")
        .pipe(sass())
        .pipe(concat(config.concattedCssFilename))
        .pipe(gulp.dest(config.destPath + "/css/"))
        .pipe(browserSync.reload({ stream: true }));
});

// Compile, concatenate, and minify all SCSS files
gulp.task("sass-minify", function () {
    return gulp.src(config.srcPath + "/scss/**/*.scss")
        .pipe(sass())
        .pipe(concat(config.concattedMinifiedCssFilename))
        .pipe(cleanCss()) // minify
        .pipe(gulp.dest(config.destPath + "/css/"));
});

// Concatenate all JS files
gulp.task("js", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(concat(config.concattedJsFilename))
        .pipe(gulp.dest(config.destPath + "/js/"));
});

// Concatenate and minify all JS files
gulp.task("js-minify", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(concat(config.concattedMinifiedJsFilename))
        .pipe(uglify())
        .pipe(gulp.dest(config.destPath + "/js/"));
});

// Optimize images
gulp.task("images", function () {
    return gulp.src(config.srcPath + "/img/**/*.+(png|jpg|jpeg|gif|svg)")
        .pipe(cache(imagemin()))
        .pipe(gulp.dest(config.destPath + "/img/"));
});

// Move fonts
gulp.task("fonts", function () {
    return gulp.src(config.srcPath + "/fonts/**/*")
        .pipe(gulp.dest(config.destPath + "/fonts/"));
});

// Setup BrowserSync
gulp.task("sync", function () {
    browserSync.init({
        proxy: config.browserSyncSettings.proxy,
        host: config.browserSyncSettings.host
    });
});

// Watch for file changes
gulp.task("watch", ["sass", "js", "fonts", "sync"], function () {
    gulp.watch(config.watchPaths.scss, ["sass"]);
    gulp.watch(config.watchPaths.js, browserSync.reload);
    gulp.watch(config.watchPaths.cshtml, browserSync.reload);
});

// Concatenate + minify everything (ready for release)
gulp.task("release", ["sass-minify", "js-minify", "images", "fonts"]);

// Default
gulp.task("default", ["watch"])
