var config = require("./gulpfile-config");
var gulp = require("gulp");
var watch = require("gulp-watch");
var sass = require("gulp-sass");
var concat = require("gulp-concat");
var cleanCss = require("gulp-clean-css");
var uglify = require("gulp-uglify");
var imagemin = require("gulp-imagemin");
var cache = require("gulp-cache");
var rev = require("gulp-rev");
var autoprefixer = require("gulp-autoprefixer");
var del = require("del");
var runSequence = require("run-sequence");
var browserSync = require("browser-sync").create();

gulp.task("sass", function () {
    return gulp.src(config.srcPath + "/scss/main.scss")
        .pipe(sass())
        .on("error", onError)
        .pipe(autoprefixer())
        .pipe(concat(config.concattedCssFilename))
        .pipe(gulp.dest(config.destPath + "/css/"))
        .pipe(browserSync.reload({ stream: true }));
});

gulp.task("sass-min", function () {
    return gulp.src(config.srcPath + "/scss/main.scss")
        .pipe(sass())
        .pipe(autoprefixer())
        .pipe(concat(config.concattedMinifiedCssFilename))
        .pipe(cleanCss())
        .pipe(rev())
        .pipe(gulp.dest(config.destPath + "/css/"))
        .pipe(rev.manifest(config.destPath + "/rev-manifest.json", {
            base: process.cwd() + "/" + config.destPath,
            merge: true,
        }))
        .pipe(gulp.dest(config.destPath));
});

gulp.task("js", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(concat(config.concattedJsFilename))
        .on("error", onError)
        .pipe(gulp.dest(config.destPath + "/js/"));
});

gulp.task("js-min", function () {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(concat(config.concattedMinifiedJsFilename))
        .pipe(uglify())
        .pipe(rev())
        .pipe(gulp.dest(config.destPath + "/js/"))
        .pipe(rev.manifest(config.destPath + "/rev-manifest.json", {
            base: process.cwd() + "/" + config.destPath,
            merge: true,
        }))
        .pipe(gulp.dest(config.destPath));
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

gulp.task("watch", ["sass", "js", "images", "fonts", "sync"], function () {
    //gulp.watch(config.watchPaths.scss, ["sass"]);
    //gulp.watch(config.watchPaths.js, browserSync.reload);
    //gulp.watch(config.watchPaths.cshtml, browserSync.reload);

    // Because gulp.watch() can't watch for new/deleted files, use gulp-watch instead
    watch(config.watchPaths.scss, function () {
        gulp.start("sass");
    });
    watch(config.watchPaths.js, function () {
        gulp.start("js");
        browserSync.reload();
    });
    watch(config.watchPaths.cshtml, function () {
        browserSync.reload();
    });
});

gulp.task("clean", function () {
    return del.sync(config.destPath);
});

//gulp.task("build", ["clean", "sass-min", "js-min", "images", "fonts"]);
gulp.task("build", function (cb) {
    // Make sure 'clean' runs first, then all the others
    runSequence("clean", "sass-min", "js-min", "images", "fonts", cb);
});

function onError(error) {
    console.log(error.toString());
    this.emit("end");
}

gulp.task("default", ["watch"]);
