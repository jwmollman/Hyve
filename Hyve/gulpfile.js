const gulp = require("gulp");
const watch = require("gulp-watch");
const sass = require("gulp-sass");
const concat = require("gulp-concat");
const cleanCss = require("gulp-clean-css");
const uglify = require("gulp-uglify-es").default;
const imagemin = require("gulp-imagemin");
const cache = require("gulp-cache");
const rev = require("gulp-rev");
const autoprefixer = require("gulp-autoprefixer");
const del = require("del");
const runSequence = require("run-sequence");
const browserSync = require("browser-sync").create();

const config = {
    srcPath: "_src",
    destPath: "Content",
    concattedCssFilename: "main.css",
    concattedJsFilename: "main.js",
    concattedMinifiedCssFilename: "main.min.css",
    concattedMinifiedJsFilename: "main.min.js",
    watchPaths: {
        scss: "_src/scss/**/*.scss",
        js: "_src/js/**/*.js",
        html: "./**/*.html",
        cshtml: "Views/**/*.cshtml",
    },
    browserSyncSettings: {
        proxy: "http://localhost:12259/",
        host: "localhost",
    },
};

gulp.task("sass", () => {
    return gulp.src(config.srcPath + "/scss/main.scss")
        .pipe(sass())
        .on("error", onError)
        .pipe(autoprefixer())
        .pipe(concat(config.concattedCssFilename))
        .pipe(gulp.dest(config.destPath + "/css/"))
        .pipe(browserSync.reload({ stream: true }));
});

gulp.task("sass-min", () => {
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

gulp.task("js", () => {
    return gulp.src(config.srcPath + "/js/**/*.js")
        .pipe(concat(config.concattedJsFilename))
        .on("error", onError)
        .pipe(gulp.dest(config.destPath + "/js/"));
});

gulp.task("js-min", () => {
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

gulp.task("images", () => {
    return gulp.src(config.srcPath + "/img/**/*.+(png|jpg|jpeg|gif|svg)")
        .pipe(cache(imagemin()))
        .pipe(gulp.dest(config.destPath + "/img/"));
});

gulp.task("fonts", () => {
    return gulp.src(config.srcPath + "/fonts/**/*")
        .pipe(gulp.dest(config.destPath + "/fonts/"));
});

gulp.task("sync", () => {
    browserSync.init({
        proxy: config.browserSyncSettings.proxy,
        host: config.browserSyncSettings.host
    });
});

gulp.task("watch", ["sass", "js", "images", "fonts", "sync"], () => {
    //gulp.watch(config.watchPaths.scss, ["sass"]);
    //gulp.watch(config.watchPaths.js, browserSync.reload);
    //gulp.watch(config.watchPaths.cshtml, browserSync.reload);

    // Because gulp.watch() can't watch for new/deleted files, use gulp-watch instead
    watch(config.watchPaths.scss, () => {
        gulp.start("sass");
    });
    watch(config.watchPaths.js, () => {
        gulp.start("js");
        browserSync.reload();
    });
    watch(config.watchPaths.html, () => {
        browserSync.reload();
    });
    watch(config.watchPaths.cshtml, () => {
        browserSync.reload();
    });
});

gulp.task("clean", () => {
    return del.sync(config.destPath);
});

//gulp.task("build", ["clean", "sass-min", "js-min", "images", "fonts"]);
gulp.task("build", cb => {
    // Make sure 'clean' runs first, then all the others
    runSequence("clean", "sass-min", "js-min", "images", "fonts", cb);
});

function onError(error) {
    console.log(error.toString());
    this.emit("end");
}

gulp.task("default", ["watch"]);
