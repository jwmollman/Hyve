var config = require("./gulpfile-config");
var gulp = require("gulp");
var sass = require("gulp-sass");
var concat = require("gulp-concat");
var cleanCss = require("gulp-clean-css");
var uglify = require("gulp-uglify");
var babel = require("gulp-babel");
var imagemin = require("gulp-imagemin");
var cache = require("gulp-cache");
var autoprefixer = require("gulp-autoprefixer");
var del = require("del");
var runSequence = require("run-sequence");
var browserSync = require("browser-sync").create();

gulp.task("sass", function () {
    return gulp.src(config.srcPath + "/scss/**/*.scss")
        .pipe(sass())
        .pipe(autoprefixer())
        .pipe(concat(config.concattedCssFilename))
        .pipe(gulp.dest(config.destPath + "/css/"))
        .pipe(browserSync.reload({ stream: true }));
});

gulp.task("sass-min", function () {
    return gulp.src(config.srcPath + "/scss/**/*.scss")
        .pipe(sass())
        .pipe(autoprefixer())
        .pipe(concat(config.concattedMinifiedCssFilename))
        .pipe(cleanCss())
        .pipe(gulp.dest(config.destPath + "/css/"));
});

gulp.task("js", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(babel())
        .pipe(concat(config.concattedJsFilename))
        .pipe(gulp.dest(config.destPath + "/js/"));
});

gulp.task("js-min", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(babel())
        .pipe(concat(config.concattedMinifiedJsFilename))
        .pipe(uglify())
        .pipe(gulp.dest(config.destPath + "/js/"));
});

gulp.task("images", function () {
    return gulp.src(config.srcPath + "/img/**/*.+(png|jpg|jpeg|gif|svg)")
        .pipe(cache(imagemin()))
        .pipe(gulp.dest(config.destPath + "/img/"));
});

gulp.task("fonts", function () {
    return gulp.src(config.srcPath + "/fonts/**/*")
        .pipe(gulp.dest(config.destPath + "/fonts/"));
});

gulp.task("sync", function () {
    browserSync.init({
        proxy: config.browserSyncSettings.proxy,
        host: config.browserSyncSettings.host
    });
});

gulp.task("watch", ["clean", "sass", "js", "fonts", "sync"], function () {
    gulp.watch(config.watchPaths.scss, ["sass"]);
    gulp.watch(config.watchPaths.js, browserSync.reload);
    gulp.watch(config.watchPaths.cshtml, browserSync.reload);
});

gulp.task("clean", function () {
    return del.sync(config.destPath);
});

//gulp.task("build", ["clean", "sass-min", "js-min", "images", "fonts"]);
gulp.task("build", function (cb) {
    // Make sure 'clean' runs first, then all the others
    runSequence("clean", ["sass-min", "js-min", "images", "fonts"], cb);
});

gulp.task("default", ["watch"]);
